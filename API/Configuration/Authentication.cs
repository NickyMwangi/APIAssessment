using Data.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Configuration
{
    public static class Authentication
    {
        public static void AddSecurity(this IServiceCollection services, IAppSettings settings)
        {
            var key = Encoding.UTF8.GetBytes(settings.AuthenticationSettings.JWT_Secret.ToString());
            services.AddAuthentication(n =>
            {
                n.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                n.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                n.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(i =>
                {
                    i.RequireHttpsMetadata = false;
                    i.SaveToken = true;
                    i.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = settings.AuthenticationSettings.ValidIssuer,
                        ValidateAudience = true,
                        ValidAudience = settings.AuthenticationSettings.ValidAudience,
                        SaveSigninToken = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true, 
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero
                    };
                    i.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = AuthenticationFailed,
                        OnTokenValidated = AuthenticationSuccess,
                        OnMessageReceived = AuthenticationMessageReceived
                    };
                });
        }

        private static Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            return Task.FromResult(0);
        }

        private static Task AuthenticationMessageReceived(MessageReceivedContext context)
        {
            string authorizationHeader = context.Request.Headers["Authorization"];

            // If no authorization header found, nothing to process further
            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    context.Token = authorizationHeader["Bearer ".Length..].Trim();

                return Task.CompletedTask;
            }

            context.NoResult();
            return Task.CompletedTask;
        }

        private static Task AuthenticationSuccess(TokenValidatedContext arg)
        {
            return Task.FromResult(0);
        }
    }
}
