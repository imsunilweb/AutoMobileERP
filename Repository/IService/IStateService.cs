using AutoMobile.DTOs;

namespace AutoMobileERP.Repository.IService
{
    public interface IStateService
    {
        Task<ResponceDtos> AddState(StateDto stateDto);
        Task<ResponceDtos> GetAllStates();
        Task<ResponceDtos> GetState(int Id);
        Task<ResponceDtos> UpdateState(StateDto stateDto);
        Task<ResponceDtos> RemoveState(int Id);
    }
}
