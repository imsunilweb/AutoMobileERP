using AutoMobile.DTOs;
using AutoMobileERP.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoMobileERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _CountryService;
        public CountryController(ICountryService CountryService)
        {
            _CountryService = CountryService;
        }

        [HttpPost]

        public async Task<ResponceDtos> AddCountry (CountryDto CountryDto)
        {
            var result = await _CountryService.AddCountry(CountryDto);
            return result;
        }
        [HttpGet("GetAll")]
        public async Task<ResponceDtos> GetAllCountrys ()
        {
            return await _CountryService.GetAllCountries();
        }
        [HttpGet]
        public async Task<ResponceDtos> GetCountry(int Id)
        {
            return await _CountryService.GetCountry(Id);
        }
        [HttpPut]
        public async Task<ResponceDtos> UpdateCountry (CountryDto CountryDto)
        {
            return await _CountryService.UpdateCountry(CountryDto);
        }
        [HttpDelete]
        public async Task<ResponceDtos> RemoveCountry(int Id)
        {
            return await _CountryService.RemoveCountry(Id);
        }
    }
}
