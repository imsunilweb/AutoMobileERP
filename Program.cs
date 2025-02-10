//using AutoMobileERP.DataConnection;
//using AutoMobileERP.Extension;
//using AutoMobileERP.Mapping;
//using AutoMobileERP.Repository.Implement;
//using AutoMobileERP.Repository.IService;
//using Microsoft.EntityFrameworkCore;

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);

//        // Add services to the container.
//        builder.Services.AddControllers();
//        builder.Services.AddScoped<ICompanyRegistrationService, CompanyRegistrationService>();
//        builder.Services.AddScoped<IBrand_BikeService, Brand_BikeService>();
//        builder.Services.AddScoped<IBikeService, BikeService>();
//        builder.Services.AddScoped<ICountryService, CountryService>();
//        builder.Services.AddScoped<IStateService, StateService>();
//        builder.Services.AddScoped<ICityService, CityService>();
//        builder.Services.AddAutoMapper(typeof(MapperProfiles));
//        builder.Services.AddDbContext<AutoMobileDbContext>(options => options.UseSqlServer(
//            builder.Configuration.GetConnectionString("AutoMobileConnection")).LogTo(Console.WriteLine));

//        // Add Swagger support
//        builder.Services.AddEndpointsApiExplorer();
//        builder.Services.AddSwaggerGen();

//        //  Call AddTokenConfiguration for JWT setup
//        builder.AddTokenConfiguration();

//        var app = builder.Build();

//        // Configure the HTTP request pipeline.
//        if (app.Environment.IsDevelopment())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI();
//        }

//        app.UseHttpsRedirection();


//        app.UseAuthentication();
//        app.UseAuthorization();

//        app.MapControllers();

//        app.Run();
//    }
//}


using AutoMobileERP.DataConnection;
using AutoMobileERP.Extension;
using AutoMobileERP.Mapping;
using AutoMobileERP.Repository.Implement;
using AutoMobileERP.Repository.IService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddScoped<ICompanyRegistrationService, CompanyRegistrationService>();
        builder.Services.AddScoped<IBrand_BikeService, Brand_BikeService>();
        builder.Services.AddScoped<IBikeService, BikeService>();
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddScoped<IStateService, StateService>();
        builder.Services.AddScoped<ICityService, CityService>();
        builder.Services.AddAutoMapper(typeof(MapperProfiles));

        // Database Context (Entity Framework)
        builder.Services.AddDbContext<AutoMobileDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("AutoMobileConnection"))
                   .LogTo(Console.WriteLine));

        // JWT Authentication
        builder.AddTokenConfiguration();

        // Add CORS policy
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        // Add Swagger support
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AutoMobile ERP API",
                Version = "v1",
                Description = "API for AutoMobile ERP system"
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAllOrigins");

        app.MapControllers();

        app.Run();
    }
}
