using AutoMobile.DTOs;
using AutoMobileERP.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoMobileERP.Controllers
{

   // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        public BikeController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpPost]

        public async Task<ResponceDtos> AddBike (BikeDto bikeDto)
        {
            var result = await _bikeService.AddBike(bikeDto);
            return result;
        }
        [HttpGet("GetAll")]
        public async Task<ResponceDtos> GetAllBikes ()
        {
            return await _bikeService.GetAllBikes();
        }
        [HttpGet]
        public async Task<ResponceDtos> GetBike(int Id)
        {
            return await _bikeService.GetBike(Id);
        }
        [HttpPut]
        public async Task<ResponceDtos> UpdateBike (BikeDto bikeDto)
        {
            return await _bikeService.UpdateBike(bikeDto);
        }
        [HttpDelete]
        public async Task<ResponceDtos> RemoveBike(int Id)
        {
            return await _bikeService.RemoveBike(Id);
        }
    }
}
