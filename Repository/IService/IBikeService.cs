using AutoMobile.DTOs;

namespace AutoMobileERP.Repository.IService
{
    public interface IBikeService
    {
        Task<ResponceDtos> AddBike(BikeDto bikeDto);
        Task<ResponceDtos> GetAllBikes();
        Task<ResponceDtos> GetBike(int Id);
        Task<ResponceDtos> UpdateBike(BikeDto bikeDto);
        Task<ResponceDtos> RemoveBike(int Id);

    }
}
