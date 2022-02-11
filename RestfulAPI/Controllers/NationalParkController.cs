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

        [HttpGet("{Id:int}")]
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Obj = _mapper.Map<NationalPark>(nationalParkDto);
            if (!_npRepo.CreateNationalPark(Obj))
            {
                ModelState.AddModelError("", $"Somethind Wrong happned while adding {Obj.Name} to the database");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
