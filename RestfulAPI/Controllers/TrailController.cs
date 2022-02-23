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
    [Route("api/v{version:apiVersion}/Trail")]
    //[Route("api/Trails")]
    //[ApiExplorerSettings(GroupName = "RestfulOpenApiSpecificationTrail")] // This is use for add multiple open api documentation.
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

        /// <summary>
        /// Create Trails
        /// </summary>
        /// <param name="trailCreateDtos"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreateTrail([FromBody] TrailCreateDtos trailCreateDtos)
        {
            if (trailCreateDtos == null)            
                return BadRequest(ModelState);
            
            if (_trailsRepository.IsTrailExist(trailCreateDtos.Name))
            {
                ModelState.AddModelError("",$"Trail {trailCreateDtos.Name} already exist");
                return StatusCode(StatusCodes.Status404NotFound, ModelState);
            }
            var objTrail = _mapper.Map<Trail>(trailCreateDtos);
            if (!_trailsRepository.CreateTrail(objTrail))
            {
                ModelState.AddModelError("", $"Somethind Wrong happned while adding {objTrail.Name} to the database");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }
            return CreatedAtRoute("GetTrail",new { Id = objTrail.Id},objTrail);

        }


        /// <summary>
        /// Update trails
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="trailUpdateDtos"></param>
        /// <returns></returns>

        [HttpPatch("{Id:int}",Name = "UpdateTrail")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateTrail(int Id,[FromBody] TrailUpdateDtos trailUpdateDtos)
        {
            if (Id != trailUpdateDtos.Id || trailUpdateDtos == null)           
                return BadRequest(ModelState);

            if (!_trailsRepository.IsTrailExist(Id) || _trailsRepository.IsTrailExist(trailUpdateDtos.Id))
            {
                ModelState.AddModelError("", $"");
                return StatusCode(StatusCodes.Status404NotFound,ModelState);
            }
            var objTrail = _mapper.Map<Trail>(trailUpdateDtos);
            if (!_trailsRepository.UpdateTrail(objTrail))
            {
                ModelState.AddModelError("", $"Something Wrong happned while updating {objTrail.Name} to the database");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Delete Trail by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}",Name = "DeleteTrailsById")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTrailsById(int Id)
        {
            if (!_trailsRepository.IsTrailExist(Id))
            {
                return NotFound();
            }

            var trail = _trailsRepository.GetTrail(Id);
            if (!_trailsRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something wrong happened while deleting {trail.Name} to database");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }


        /// <summary>
        /// Delete trail by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete(Name = "DeleteTrailsByName")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteTrailsByName([FromBody] string name)
        {
            if (!_trailsRepository.IsTrailExist(name))
            {
                return NotFound();
            }

            var trail = _trailsRepository.GetTrail(name);
            if (!_trailsRepository.DeleteTrail(trail))
            {
                ModelState.AddModelError("", $"Something wrong happened while deleting {trail.Name} to database");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

    }
}
