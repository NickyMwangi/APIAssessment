using Data.DBContext;
using Data.Interfaces;
using Data.seed.Roles;
using Microsoft.AspNetCore.Identity;

namespace API.Configuration
{
    public static class DbSeeder
    {
        public static void SeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var appSettings = scope.ServiceProvider.GetRequiredService<IAppSettings>();
                var _dbContext = scope.ServiceProvider.GetRequiredService<IdContext>();
                var _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (appSettings.AuthenticationSettings.AllowSeeding)
                {
                    // seed roles
                    RolesSeed _roleSeed = new(_dbContext);
                    _roleSeed.BeginSeeding();
                }
            }
        }
    }
}
