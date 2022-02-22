using AutoMapper;
using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;

namespace RestfulAPI.Mappings
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class TrailsMappings : Profile
    {
        public TrailsMappings()
        {
            CreateMap<Trail,TrailDtos>().ReverseMap();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
