using AutoMapper;
using AutoMobile.DTOs;
using AutoMobile.Models.Masters;
using AutoMobileERP.DataConnection;
using AutoMobileERP.Repository.IService;
using Microsoft.EntityFrameworkCore;

namespace AutoMobileERP.Repository.Implement
{
    public class Brand_BikeService : IBrand_BikeService
    {
        private readonly AutoMobileDbContext _dbContext;
        private readonly IMapper _mapper;
        private ResponceDtos _responceDtos;

        public Brand_BikeService(AutoMobileDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _responceDtos = new ResponceDtos();
        }

        public async Task<ResponceDtos> GetAllBrandBike()
        {
            try
            {
                var GetAll = await _dbContext.Bike_Brand.ToListAsync();
                _responceDtos.Result = _mapper.Map<List<Bike_BrandDto>>(GetAll);
                _responceDtos.Massage = "Fetched all product successfully.";
            }
            catch(Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> AddBrand_Bike(Bike_BrandDto bike_BrandDto)
        {
            try
            {
                var result= _mapper.Map<Bike_Brand>(bike_BrandDto);
                await _dbContext.Bike_Brand.AddAsync(result);
                await _dbContext.SaveChangesAsync();

                _responceDtos.Massage = "Product Add successfully.";
                //_responceDtos.Result = product;
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> GetBrandBike(int id)
        {
            try
            {
                //  var result = await _dbContext.Bike_Brand.
                var result = await _dbContext.Bike_Brand.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    _responceDtos.IsSuccess = false;
                    _responceDtos.Massage = "Product Id does not exist";
                    return _responceDtos;
                }
                _responceDtos.Result = _mapper.Map<Bike_BrandDto>(result);
                _responceDtos.Massage = "Fetched product successfully.";
            }
            catch (Exception ex)
            {
                _responceDtos.IsSuccess = false;
                _responceDtos.Massage = ex.Message;
            }
            return _responceDtos;
        }

        public async Task<ResponceDtos> UpdateBrandBike(Bike_BrandDto productDto)
        {
            try
            {
                var result = await _dbContext.Bike_Brand.FirstOrDefaultAsync(x =>x.Id == productDto.Id);
                if (result == null)
                {
                    _responceDtos.IsSuccess=false;
                    _responceDtos.Massage = "Id does not exist";
                }

                _mapper.Map(productDto, result);
                _dbContext.Bike_Brand.Update(result);
                await _dbContext.SaveChangesAsync();
                _responceDtos.Massage = "Product update successfuly";

            }
            catch(Exception ex)
            {
                _responceDtos.IsSuccess=!false;
                _responceDtos.Massage=ex.Message;

            }
            return _responceDtos;
        }
    }
}
