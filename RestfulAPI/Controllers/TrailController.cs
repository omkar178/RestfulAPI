using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI.Repository.IRepository;

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
       
    }
}
