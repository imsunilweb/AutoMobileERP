using AutoMobile.DTOs;
using AutoMobileERP.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoMobileERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  //  [Authorize(Roles = "Costumer")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost]

        public async Task<ResponceDtos> AddCity (CityDto cityDto)
        {
            var result = await _cityService.AddCity(cityDto);
            return result;
        }
        [HttpGet("GetAll")]
        public async Task<ResponceDtos> GetAllCitys ()
        {
            return await _cityService.GetAllCities();
        }
        [HttpGet]
        public async Task<ResponceDtos> GetCity(int Id)
        {
            return await _cityService.GetCity(Id);
        }
        [HttpPut]
        public async Task<ResponceDtos> UpdateCity (CityDto cityDto)
        {
            return await _cityService.UpdateCity(cityDto);
        }
        [HttpDelete]
        public async Task<ResponceDtos> RemoveCity(int Id)
        {
            return await _cityService.RemoveCity(Id);
        }
    }
}
