using AutoMapper;
using AutoMobile.DTOs;
using AutoMobile.Models.Masters;
using AutoMobile.Models.model;
using AutoMobileERP.DataConnection;
using AutoMobileERP.Repository.IService;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileERP.Repository.Implement
{
    public class BikeService(AutoMobileDbContext context, IMapper mapper) : IBikeService
    {
        private readonly AutoMobileDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private ResponceDtos _responceDtos = new ResponceDtos();

        public async Task<ResponceDtos> AddBike(BikeDto bikeDto)
        {
            try
            {
                var segment = _mapper.Map<Bike>(bikeDto);
                await _context.Bike.AddAsync(segment);
                await _context.SaveChangesAsync();
                _responceDtos.Massage = "Segment product Add successfuly";
            }
            catch(Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage= ex.InnerException?.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetAllBikes()
        {
            try
            {
                var Getall= await _context.Bike.ToListAsync();
                _responceDtos.Result = _mapper.Map <List<BikeDto>>(Getall);
                _responceDtos.Massage = "Get All Bikes Successfuly";

            }
            catch(Exception ex)
            {
                _responceDtos.IsSuccess=false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetBike(int Id)
        {
            try
            {
                var GetId = await _context.Bike.FindAsync(Id);
                _responceDtos.Result = _mapper.Map<BikeDto>(GetId);
                _responceDtos.Massage = "Get Bike Successfuly";

            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> RemoveBike(int Id)
        {
            try
            {
                var existingBike = await _context.Bike.FindAsync(Id);
                if (existingBike == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "Bike with the given Id does not exist.";
                    return _responceDtos;
                }
                // Remove the entity from the context
                _context.Bike.Remove(existingBike);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Set success response
                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Bike removed successfully.";
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return failure response
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = $"Error: {ex.Message}";
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> UpdateBike(BikeDto bikeDto)
        {
            try
            {
                // Retrieve the existing bike entity from the database
                var existingBike = await _context.Bike.FindAsync(bikeDto.Id);
                if (existingBike == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "Bike not found.";
                    return _responceDtos;
                }

                // Map the new values from bikeDto to the existingBike
                _mapper.Map(bikeDto, existingBike);

                // Update the entity
                _context.Bike.Update(existingBike);
                await _context.SaveChangesAsync();

                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Updated Bike successfully";
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.InnerException?.Message ?? ex.Message; // Handle inner exception if present
            }
            return _responceDtos;
        }
    }
}
