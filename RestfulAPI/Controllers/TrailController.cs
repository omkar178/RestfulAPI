using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;
using RestfulAPI.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace RestfulAPI.Controllers
{
    [ApiController]
    [Route("api/Trails")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailController : Controller
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        private readonly ITrailsRepository _trailsRepository;
        private readonly IMapper _mapper;


        /// <summary>
        ///  Initialize Repository and mapper object
        /// </summary>
        /// <param name="trailsRepository"></param>
        /// <param name="mapper"></param>
        public TrailController(ITrailsRepository trailsRepository,IMapper mapper)
        {
            _trailsRepository = trailsRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get All trails
        /// </summary>
        /// <returns></returns>       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public IActionResult GetAllTrail()
        { 
            var objTrail = _trailsRepository.GetAllTrails();
            var objTrailDto = new List<TrailDtos>();
            if (objTrail != null && objTrail.Count() > 0)
            {
                foreach (Trail trail in objTrail)
                {
                    objTrailDto.Add(_mapper.Map<TrailDtos>(trail));
                }
                return Ok(objTrailDto);
            }
            return NotFound();
        }
        /// <summary>
        /// Get a Individual trail.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}",Name = "GetTrail")]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int Id)
        {
            var objTrail = _trailsRepository.GetTrail(Id);
            if (objTrail == null)
            {
                return NotFound();
            }

            var objTrailDto = _mapper.Map<TrailDtos>(objTrail);
            return Ok(objTrailDto);
            
        }
       
    }
}
