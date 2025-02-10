using AutoMapper;
using AutoMobile.DTOs;
using AutoMobile.Models.model;
using AutoMobileERP.DataConnection;
using AutoMobileERP.Repository.IService;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace AutoMobileERP.Repository.Implement
{
    public class CompanyRegistrationService(AutoMobileDbContext context, IMapper mapper) : ICompanyRegistrationService
    {
        private readonly AutoMobileDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private ResponceDtos _responceDtos = new ResponceDtos();

        public async Task<ResponceDtos> GetAllCompanyRegistration()
        {
           
            try
            {
                // Fetch all company registrations from the database
                var companies = await _context.CompanyRegistration.ToListAsync();

                // Map the list of entities to a list of DTOs
                _responceDtos.Result = _mapper.Map<List<CompanyRegistrationDTOs>>(companies);

                // Set success response
                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Fetched all company registrations successfully.";
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetCompanyRegistration(int id)
        {
            try
            {
                var result = await _context.CompanyRegistration.FirstOrDefaultAsync(x => x.Id == id);
                if(result == null)
                {
                    _responceDtos.IsSuccess=false;
                    _responceDtos.Massage = "Company Id does not exist";
                    return _responceDtos;
                }
                //Map the entity to dtos

                _responceDtos.Result =_mapper.Map<CompanyRegistrationDTOs>(result);
                _responceDtos.Massage = "Company retrieved successfully.";
            }
            catch(Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> NewCompanyRegistration(CompanyRegistrationDTOs companyRegistrationDTOs)
        {
            try
            {
                var existingCompany = await _context.CompanyRegistration
                    .FirstOrDefaultAsync(x => x.CompanyName == companyRegistrationDTOs.CompanyName);

                if (existingCompany != null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "Company Already Exists";
                    return _responceDtos;
                }

                // Create a new CompanyRegistration entity
                var newCompany = _mapper.Map<CompanyRegistration>(companyRegistrationDTOs);
                await _context.CompanyRegistration.AddAsync(newCompany);
                await _context.SaveChangesAsync();

                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Registration done successfully";
                _responceDtos.Result = newCompany; // Optionally return the newly created company
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return _responceDtos;
        }


        public async Task<ResponceDtos> UpdateCompanyRegistration(CompanyRegistrationDTOs companyRegistrationDTOs)
        {
            try
            {
                var result = await _context.CompanyRegistration.FirstOrDefaultAsync(x => x.Id == companyRegistrationDTOs.Id);
                if(result == null )
                {
                    _responceDtos.IsSuccess=false;
                    _responceDtos.Massage = "Id does not exist";
                    return _responceDtos;
                }
                _mapper.Map(companyRegistrationDTOs, result);
                _context.CompanyRegistration.Update(result);
                await _context.SaveChangesAsync();
                _responceDtos.Massage = "Company update successfully";
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage= ex.Message;   
            }
            return _responceDtos;
        }
        public async Task<ResponceDtos> PatchCompanyRegistration(CompanyRegistrationDTOs companyRegistrationDTOs)
        {
            var responceDtos = new ResponceDtos(); // Initialize response object

            try
            {
                // Find the existing company by Id from the DTO
                var existingCompany = await _context.CompanyRegistration.FirstOrDefaultAsync(x => x.Id == companyRegistrationDTOs.Id);
                if (existingCompany == null)
                {
                    responceDtos.IsSuccess = false;
                    responceDtos.Massage = "Id does not exist";
                    return responceDtos;
                }

                // Use AutoMapper to map only the non-null properties
                _mapper.Map(companyRegistrationDTOs, existingCompany);

                // Update metadata fields
                existingCompany.ModifiedDate = DateTime.UtcNow; // Update modified date
                existingCompany.ModifiedBy = "CurrentUser"; // Replace with actual user context

                await _context.SaveChangesAsync();
                responceDtos.IsSuccess = true;
                responceDtos.Massage = "Company updated successfully";
            }
            catch (Exception ex)
            {
                responceDtos.IsSuccess = false;
                responceDtos.Massage = ex.Message;
            }
            return responceDtos;
        }

        public async Task<ResponceDtos> RemoveCompanyRegistration(int id)
        {
            try
            {
                // Find the company registration entity by Id
                var companyRegistration = await _context.CompanyRegistration.FindAsync(id);

                // If the company registration doesn't exist, return an error
                if (companyRegistration == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "Company with the given Id does not exist.";
                    return _responceDtos;
                }

                // Remove the entity from the context
                _context.CompanyRegistration.Remove(companyRegistration);

                // Save the changes to the database
                await _context.SaveChangesAsync();

                // Set success response
                _responceDtos.IsSuccess = true;
                _responceDtos.Massage = "Company removed successfully.";
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return failure response
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = $"Error: {ex.Message}";
            }

            return _responceDtos;
        }

    }
}
