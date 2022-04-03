using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    [Route("api/v{version:apiVersion}/nationalpark")]
    [ApiVersion("2.0")]
    //[Route("api/[controller]")]
    //[ApiExplorerSettings(GroupName = "RestfulOpenApiSpecificationNP")] // This is use for add multiple open api documentation.
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParkV2Controller : ControllerBase
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly INationalParkRepository _npRepo;
        private readonly IMapper _mapper;

        
        /// <summary>
        /// Initialize Repository and mapper object
        /// </summary>
        /// <param name="npRepo"></param>
        /// <param name="mapper"></param>
        public NationalParkV2Controller(INationalParkRepository npRepo, IMapper mapper)
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
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<NationalPark>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(NationalPark))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
    }
}
