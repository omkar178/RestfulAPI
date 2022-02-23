using AutoMapper;
using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;

namespace RestfulAPI.Mappings
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class NationalParkMappings : Profile

    {
        public NationalParkMappings()
        {
            CreateMap<NationalPark,NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDtos>().ReverseMap();
            CreateMap<Trail, TrailCreateDtos>().ReverseMap();
            CreateMap<Trail, TrailUpdateDtos>().ReverseMap();
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
