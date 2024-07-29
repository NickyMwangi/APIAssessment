using Data.Interfaces;
using Microsoft.OpenApi.Models;

namespace API.Configuration
{
    public static class SwaggerDefinitions
    {
        public static void AddSwaggerDoc(this IServiceCollection services, IAppSettings settings)
        {
            services.AddSwaggerGen(n =>
            {
                n.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Nickson Assessment API",
                    Description = "API Developed for implementation of assessment portal",
                    TermsOfService = new Uri("https://www.alibaba.com/"),
                    Version = "v1"
                });
                n.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Enter a valid token to proceed",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                });
                n.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }
    }
}
