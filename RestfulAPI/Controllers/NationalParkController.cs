using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;
using RestfulAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestfulAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParkController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initialize Repository and mapper object
        /// </summary>
        /// <param name="npRepo"></param>
        /// <param name="mapper"></param>
        public NationalParkController(INationalParkRepository npRepo, IMapper mapper)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }
        /// <summary>
        /// Get All National park
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllNationalPark()
        {
            var objList = _npRepo.GetAllNationalParks();
            var objDto = new List<NationalParkDto>();

            if (objList != null && objList.Count() > 0 )
            {
                foreach (NationalPark np in objList)
                {
                    objDto.Add(_mapper.Map<NationalParkDto>(np));
                }
                return Ok(objDto);
            }
            return NotFound();
        }

        /// <summary>
        /// Get Specific National Park
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}",Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int Id)
        {
            var nationalPark = _npRepo.GetNationalPark(Id);
            if (nationalPark == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<NationalParkDto>(nationalPark);
            return Ok(objDto);
            
        }

        /// <summary>
        /// Create new national park
        /// </summary>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null)
            { 
                return BadRequest(ModelState);
            }
            if (_npRepo.IsExist(nationalParkDto.Name))
            {
                ModelState.AddModelError("",$"NationalPark {nationalParkDto.Name} already exist");
                return StatusCode(404,ModelState);
            }

            /* if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/

            var Obj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.CreateNationalPark(Obj))
            {
                ModelState.AddModelError("", $"Somethind Wrong happned while adding {Obj.Name} to the database");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetNationalPark",new { Id = Obj.Id },Obj);
        }

        /// <summary>
        /// Update National Park
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="nationalParkDto"></param>
        /// <returns></returns>
        [HttpPatch("{Id:int}", Name = "UpdatedNationalPark")]
        public IActionResult UpdatedNationalPark(int Id ,[FromBody] NationalParkDto nationalParkDto)
        {
            if (Id != nationalParkDto.Id || nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_npRepo.IsExist(Id) || !_npRepo.IsExist(nationalParkDto.Name))
            {
                ModelState.AddModelError("", $"NationalPark {nationalParkDto.Name} not exits");
                return StatusCode(404, ModelState);
            }

            var nationalPark = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.UpdateNationalPark(nationalPark))
            {
                ModelState.AddModelError("",$"Something Wrong happned while updating {nationalPark.Name} to the database");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }

        /// <summary>
        /// Delete national park by it's Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}", Name = "DeleteNationalParkById")]
        public IActionResult DeleteNationalParkById(int Id)
        {
            if (!_npRepo.IsExist(Id))
            {
                return NotFound();
            }

            var nationalPark = _npRepo.GetNationalPark(Id);
            if(!_npRepo.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something wrong happened while deleting {nationalPark.Name} to database");
                return StatusCode(500, ModelState);
            }

            return NoContent();
            
        }

        /// <summary>
        /// Delete national park by it's name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete(Name = "DeleteNationalParkByName")]
        public IActionResult DeleteNationalParkByName([FromBody] string name)
        {
            if (string.IsNullOrEmpty(name))
            { 
                return BadRequest(ModelState);
            }

            if (!_npRepo.IsExist(name))
            {
                return NotFound();
            }

            var nationalPark = _npRepo.GetNationalPark(name);
            if (!_npRepo.DeleteNationalPark(nationalPark))
            {
                ModelState.AddModelError("", $"Something wrong happened while deleting {nationalPark.Name} to database");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
