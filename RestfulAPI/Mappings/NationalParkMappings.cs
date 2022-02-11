using AutoMapper;
using RestfulAPI.Model;
using RestfulAPI.Model.Dtos;

namespace RestfulAPI.Mappings
{
    public class NationalParkMappings : Profile
    {
        public NationalParkMappings()
        {
            CreateMap<NationalPark,NationalParkDto>().ReverseMap();
        }
    }
}
