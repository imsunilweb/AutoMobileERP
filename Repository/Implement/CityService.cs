using AutoMapper;
using AutoMobile.DTOs;
using AutoMobile.Models.Masters;
using AutoMobileERP.DataConnection;
using AutoMobileERP.Repository.IService;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileERP.Repository.Implement
{
    public class CityService : ICityService
    {
        private readonly AutoMobileDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponceDtos _responceDtos;

        public CityService(AutoMobileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responceDtos = new ResponceDtos();
        }
        public async Task<ResponceDtos> AddCity(CityDto cityDto)
        {
            try
            {
                var existingCity = await _context.City
                   .FirstOrDefaultAsync(x => x.Name == cityDto.Name);

                if (existingCity != null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "City Already Exists";
                    return _responceDtos;
                }


                var segment = _mapper.Map<City>(cityDto);
                await _context.City.AddAsync(segment);
                await _context.SaveChangesAsync();
                _responceDtos.Massage = "City Add successfuly";
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.InnerException?.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetAllCities()
        {
            try
            {
                var Getall = await _context.City.ToListAsync();
                _responceDtos.Result = _mapper.Map<List<CityDto>>(Getall);
                _responceDtos.Massage = "Get All Cities Successfuly";

            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetCity(int Id)
        {
            try
            {
                var GetId = await _context.City.FindAsync(Id);
                _responceDtos.Result = _mapper.Map<CityDto>(GetId);
                _responceDtos.Massage = "Get City Successfuly";

            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> RemoveCity(int Id)
        {
            try
            {
                var existingCity = await _context.City.FindAsync(Id);
                if (existingCity == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "City with the given Id does not exist.";
                    return _responceDtos;
                }
                // Remove the entity from the context
                _context.City.Remove(existingCity);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Set success response
                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "City removed successfully.";
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return failure response
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = $"Error: {ex.Message}";
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> UpdateCity(CityDto cityDto)
        {
            try
            {
                // Retrieve the existing City entity from the database
                var existingCity = await _context.City.FindAsync(cityDto.Id);
                if (existingCity == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "City not found.";
                    return _responceDtos;
                }

                // Map the new values from CityDto to the existingCity
                _mapper.Map(cityDto, existingCity);

                // Update the entity
                _context.City.Update(existingCity);
                await _context.SaveChangesAsync();

                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Updated City successfully";
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
