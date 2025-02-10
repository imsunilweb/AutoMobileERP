using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AutoMobileERP.Extension
{
    public static class TokenConfiguration
    {
        public static WebApplicationBuilder AddTokenConfiguration(this WebApplicationBuilder app)
        {
            // Swagger setup to support JWT token authentication
            app.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as follows: 'Bearer <JWT-Token>'",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    }, new string[] { }
                }
            });
            });

            // Extracting settings from the configuration file
            var setting = app.Configuration.GetSection("ApiSettings");
            var secret = setting.GetValue<string>("JwtOptions:Secret");
            var issuer = setting.GetValue<string>("JwtOptions:Issuer");
            var audience = setting.GetValue<string>("JwtOptions:Audience");
            var key = Encoding.ASCII.GetBytes(secret);  // Converting secret to byte array

            // Adding JWT Authentication middleware
            app.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                };
            });

            return app;  // Returning the modified app
        }
    }

}
