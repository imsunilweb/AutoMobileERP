using AutoMapper;
using AutoMobile.DTOs;
using AutoMobile.Models.Masters;
using AutoMobileERP.DataConnection;
using AutoMobileERP.Repository.IService;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileERP.Repository.Implement
{
    public class StateService : IStateService
    {
        private readonly AutoMobileDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponceDtos _responceDtos;

        public StateService(AutoMobileDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _responceDtos = new ResponceDtos();
        }
        public async Task<ResponceDtos> AddState(StateDto stateDto)
        {
            try
            {
                var existingState = await _context.State
                   .FirstOrDefaultAsync(x => x.Name == stateDto.Name);

                if (existingState != null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "State Already Exists";
                    return _responceDtos;
                }


                var segment = _mapper.Map<State>(stateDto);
                await _context.State.AddAsync(segment);
                await _context.SaveChangesAsync();
                _responceDtos.Massage = "State Add successfuly";
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.InnerException?.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetAllStates()
        {
            try
            {
                var Getall = await _context.State.ToListAsync();
                _responceDtos.Result = _mapper.Map<List<StateDto>>(Getall);
                _responceDtos.Massage = "Get All States Successfuly";

            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetState(int Id)
        {
            try
            {
                var GetId = await _context.State.FindAsync(Id);
                _responceDtos.Result = _mapper.Map<StateDto>(GetId);
                _responceDtos.Massage = "Get State Successfuly";

            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> RemoveState(int Id)
        {
            try
            {
                var existingState = await _context.State.FindAsync(Id);
                if (existingState == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "State with the given Id does not exist.";
                    return _responceDtos;
                }
                // Remove the entity from the context
                _context.State.Remove(existingState);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Set success response
                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "State removed successfully.";
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return failure response
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = $"Error: {ex.Message}";
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> UpdateState(StateDto stateDto)
        {
            try
            {
                // Retrieve the existing State entity from the database
                var existingState = await _context.State.FindAsync(stateDto.Id);
                if (existingState == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "State not found.";
                    return _responceDtos;
                }

                // Map the new values from StateDto to the existingState
                _mapper.Map(stateDto, existingState);

                // Update the entity
                _context.State.Update(existingState);
                await _context.SaveChangesAsync();

                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Updated State successfully";
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
