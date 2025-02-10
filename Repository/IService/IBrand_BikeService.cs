using AutoMobile.DTOs;

namespace AutoMobileERP.Repository.IService
{
    public interface IBrand_BikeService
    {
        Task<ResponceDtos> AddBrand_Bike(Bike_BrandDto bike_BrandDto);
        Task<ResponceDtos> GetAllBrandBike ();
        Task<ResponceDtos> GetBrandBike(int id);
        Task<ResponceDtos> UpdateBrandBike(Bike_BrandDto bike_BrandDto);

    }
}
