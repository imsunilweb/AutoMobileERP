using AutoMobile.DTOs;

namespace AutoMobileERP.Repository.IService
{
    public interface ICompanyRegistrationService
    {
        Task<ResponceDtos> NewCompanyRegistration(CompanyRegistrationDTOs companyRegistrationDTOs);
        Task<ResponceDtos> GetAllCompanyRegistration();
        Task<ResponceDtos> GetCompanyRegistration(int Id);
        Task<ResponceDtos> UpdateCompanyRegistration(CompanyRegistrationDTOs companyRegistrationDTOs);
        Task<ResponceDtos> PatchCompanyRegistration( CompanyRegistrationDTOs companyRegistrationDTOs);
        Task<ResponceDtos> RemoveCompanyRegistration(int Id);


    }
}
