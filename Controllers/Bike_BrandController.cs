using AutoMobile.DTOs;
using AutoMobileERP.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AutoMobileERP.Controllers
{
   // [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    
    public class Bike_BrandController(IBrand_BikeService brand_BikeService) : ControllerBase
    {
        private readonly IBrand_BikeService _brand_BikeService = brand_BikeService;

        [HttpPost]
        public async Task<ResponceDtos> NewProductRegistration(Bike_BrandDto bike_BrandDto)
        {
            var result =
            await _brand_BikeService.AddBrand_Bike(bike_BrandDto);
            return result;
        }

        [HttpGet]

        public async Task<ResponceDtos> GetAllProducts()
        {
            return await _brand_BikeService.GetAllBrandBike();
        }
        [HttpGet("{Id}")]

        public async Task<ResponceDtos> GetProductRegistration(int Id)
        {
            return await _brand_BikeService.GetBrandBike(Id);
        }
        [HttpPut]
        public async Task<ResponceDtos> UpdateProduct (Bike_BrandDto bike_BrandDto )
        {
            return await _brand_BikeService.UpdateBrandBike(bike_BrandDto);
        }
    }
}
