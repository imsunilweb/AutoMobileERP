using AutoMobile.DTOs;

namespace AutoMobileERP.Repository.IService
{
    public interface ICountryService
    {
        Task<ResponceDtos> AddCountry(CountryDto countryDto);
        Task<ResponceDtos> GetAllCountries();
        Task<ResponceDtos> GetCountry(int Id);
        Task<ResponceDtos> UpdateCountry(CountryDto countryDto);
        Task<ResponceDtos> RemoveCountry(int Id);
    }
}
