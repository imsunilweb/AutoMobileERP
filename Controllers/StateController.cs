using AutoMobile.DTOs;
using AutoMobileERP.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoMobileERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StateController : ControllerBase
    {
        private readonly IStateService _StateService;
        public StateController(IStateService StateService)
        {
            _StateService = StateService;
        }

        [HttpPost]

        public async Task<ResponceDtos> AddState (StateDto StateDto)
        {
            var result = await _StateService.AddState(StateDto);
            return result;
        }
        [HttpGet("GetAll")]
        public async Task<ResponceDtos> GetAllStates ()
        {
            return await _StateService.GetAllStates();
        }
        [HttpGet]
        public async Task<ResponceDtos> GetState(int Id)
        {
            return await _StateService.GetState(Id);
        }
        [HttpPut]
        public async Task<ResponceDtos> UpdateState (StateDto StateDto)
        {
            return await _StateService.UpdateState(StateDto);
        }
        [HttpDelete]
        public async Task<ResponceDtos> RemoveState(int Id)
        {
            return await _StateService.RemoveState(Id);
        }
    }
}
