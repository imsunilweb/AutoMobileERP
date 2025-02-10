using AutoMapper;
using AutoMobile.DTOs;
using AutoMobile.Models.Masters;
using AutoMobile.Models.model;

namespace AutoMobileERP.Mapping
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<CompanyRegistration, CompanyRegistrationDTOs>().ReverseMap();
            CreateMap<Bike_Brand, Bike_BrandDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Bike, BikeDto>().ReverseMap();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<City, CityDto>().ReverseMap();
        }
    }
}
