using AutoMapper;
using CityFinder.Models;
using CityFinder.Dtos;

namespace CityFinder.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryDto>()
                .ForMember(dest => dest.Alpha2Code, opt => opt.MapFrom(src => src.Cca2))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Offical));
        }
    }
}