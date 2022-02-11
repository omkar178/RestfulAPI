using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;
using RestfulAPI.Repository.IRepository;
using System;
using System.Collections.Generic;

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

             if (objList != null)
             {
                foreach (NationalPark np in objList)
                {
                   objDto.Add(_mapper.Map<NationalParkDto>(np));
                }
                return Ok(objDto);
             }
             return NotFound();
            
            
        }
    }
}
