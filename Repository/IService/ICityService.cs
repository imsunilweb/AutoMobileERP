using AutoMobile.DTOs;

namespace AutoMobileERP.Repository.IService
{
    public interface ICityService
    {
        Task<ResponceDtos> AddCity(CityDto cityDto);
        Task<ResponceDtos> GetAllCities();
        Task<ResponceDtos> GetCity(int Id);
        Task<ResponceDtos> UpdateCity(CityDto cityDto);
        Task<ResponceDtos> RemoveCity(int Id);
    }
}
