using AutoMobile.DTOs;
using AutoMobileERP.Repository.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoMobileERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Costumer")]
    public class CompanyRegistrationController(ICompanyRegistrationService companyRegistrationService) : ControllerBase
    {
        private readonly ICompanyRegistrationService _companyRegistrationService = companyRegistrationService;

        [HttpPost]
        public async Task<ResponceDtos> NewCompany(CompanyRegistrationDTOs companyRegistrationDTOs)
        {
           var result = await _companyRegistrationService.NewCompanyRegistration(companyRegistrationDTOs);
            return result;
           
        }

        [HttpGet]
        public async Task<ResponceDtos> GetAllCompanies()
        {
            var response = await _companyRegistrationService.GetAllCompanyRegistration();

            return response; // Return error response
        }
        [HttpGet("{id}")]
        public async Task<ResponceDtos> GetCompanyRegistration(int id)
        {
            var result= await _companyRegistrationService.GetCompanyRegistration(id);
            return result;
        }
        [HttpPut]

        public async Task<ResponceDtos> UpdateCompanyRegistration(CompanyRegistrationDTOs companyRegistrationDTOs)
        {
            var update = await _companyRegistrationService.UpdateCompanyRegistration(companyRegistrationDTOs);
            return update;
        }
        [HttpPatch]
        public async Task<ResponceDtos> PatchCompanyRegistration( CompanyRegistrationDTOs companyRegistrationDTOs)
        {
            return await _companyRegistrationService.PatchCompanyRegistration(companyRegistrationDTOs);
        }
        [HttpDelete]
        public async Task<ResponceDtos> RemoveCompanyRegistration(int Id)
        {
            return await _companyRegistrationService.RemoveCompanyRegistration(Id);
        }
    }
}
