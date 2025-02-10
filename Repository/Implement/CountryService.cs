using AutoMapper;
using AutoMobile.DTOs;
using AutoMobile.Models.Masters;
using AutoMobileERP.DataConnection;
using AutoMobileERP.Repository.IService;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileERP.Repository.Implement
{
    public class CountryService: ICountryService
    {
        private readonly AutoMobileDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponceDtos _responceDtos;

        public CountryService(AutoMobileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responceDtos = new ResponceDtos();
        }
        public async Task<ResponceDtos> AddCountry(CountryDto CountryDto)
        {
            try
            {
                var segment = _mapper.Map<Country>(CountryDto);
                await _context.Country.AddAsync(segment);
                await _context.SaveChangesAsync();
                _responceDtos.Massage = "Country Add successfuly";
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.InnerException?.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetAllCountries()
        {
            try
            {
                var Getall = await _context.Country.ToListAsync();
                _responceDtos.Result = _mapper.Map<List<CountryDto>>(Getall);
                _responceDtos.Massage = "Get All Countrys Successfuly";

            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetCountry(int Id)
        {
            try
            {
                var GetId = await _context.Country.FindAsync(Id);
                _responceDtos.Result = _mapper.Map<CountryDto>(GetId);
                _responceDtos.Massage = "Get Country Successfuly";

            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> RemoveCountry(int Id)
        {
            try
            {
                var existingCountry = await _context.Country.FindAsync(Id);
                if (existingCountry == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "Country with the given Id does not exist.";
                    return _responceDtos;
                }
                // Remove the entity from the context
                _context.Country.Remove(existingCountry);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Set success response
                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Country removed successfully.";
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return failure response
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = $"Error: {ex.Message}";
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> UpdateCountry(CountryDto CountryDto)
        {
            try
            {
                // Retrieve the existing Country entity from the database
                var existingCountry = await _context.Country.FindAsync(CountryDto.Id);
                if (existingCountry == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "Country not found.";
                    return _responceDtos;
                }

                // Map the new values from CountryDto to the existingCountry
                _mapper.Map(CountryDto, existingCountry);

                // Update the entity
                _context.Country.Update(existingCountry);
                await _context.SaveChangesAsync();

                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Updated Country successfully";
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
