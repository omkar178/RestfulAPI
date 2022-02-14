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
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;
        public NationalParkController(INationalParkRepository npRepo, IMapper mapper)
        {
            _npRepo = npRepo;
            _mapper = mapper;
        }

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

        [HttpPatch("{Id:int}", Name = "UpdatedNationalPark")]
        public IActionResult UpdatedNationalPark(int Id ,[FromBody] NationalParkDto nationalParkDto)
        {
            if (Id == nationalParkDto.Id || nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_npRepo.IsExist(Id))
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
        [HttpDelete("{name:alpha}", Name = "DeleteNationalParkByName")]
        public IActionResult DeleteNationalParkByName(string name)
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
