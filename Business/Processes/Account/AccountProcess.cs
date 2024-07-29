using API.Models.Account;
using Business.IProcesses.Account;
using Data.Interfaces;
using Data.Extensions;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Dtos.Account;
using Microsoft.AspNetCore.WebUtilities;
using static Library.Models.AccountModel;

namespace Business.Processes.Account
{
    public class AccountProcess : IAccountProcess
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService;
        private readonly IAppSettings _appSettings;
        public AccountProcess(UserManager<ApplicationUser> userManager, INotificationService notificationService, IAppSettings appSettings)
        {
            _userManager = userManager;
            _notificationService = notificationService;
            _appSettings = appSettings;
        }

        public async Task<AccountDto> ConfirmEmail(string email, string code)
        {
            AccountDto accountDto = new();
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
                    accountDto.Message = "Invalid Request ";
                var user = await _userManager.FindByNameAsync(email);

                if (user == null)
                    accountDto.Message = "Invalid Email Confirmation Request";
                else
                {
                    code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                    var result = await _userManager.ConfirmEmailAsync(user, code);
                    if (!result.Succeeded)
                    {
                        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                        if (result.Errors == null)
                            errors = "Invalid Email Confirmation Request";
                        accountDto.Message = errors;
                    }
                    else
                    {
                        accountDto.Message = "Email confirmation was successfull";
                        accountDto.IsSuccess = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                accountDto.Message = ex.Message;
            }
            return accountDto;
        }

        public async Task<AccountDto> ForgotPassword(ForgotPasswordModel model)
        {
            AccountDto accountDto = new();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                accountDto.Message ="Invalid Request. An account with this email address doesnt exist";
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", model.Email }
            };
            var callback = QueryHelpers.AddQueryString(model.ClientURI, param);
            await _notificationService.SendEmailResetPasswordAsync(user, callback);

            accountDto.Message = "Reset link sent successfully. Check your Email to reset";
            accountDto.IsSuccess = true;
            return accountDto;
        }

        public async Task<AccountDto> Login(AccountModel.LoginModel model)
        {
            AccountDto accountDto = new();
            try
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                    accountDto.Message = "Invalid Email address or username";
                else
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        await SendEmailConfirmationLink(user, model.ClientURI);
                        accountDto.Message = "Open your mail to confirm your account";
                    }
                    else if (await _userManager.CheckPasswordAsync(user, model.Password))
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(_appSettings.AuthenticationSettings.JWT_Secret);
                        var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.GroupSid, user.NationalID),
                            new Claim(JwtRegisteredClaimNames.Jti ,Guid.NewGuid().ToString()),
                        };
                        // add all the roles for this user
                        foreach (var role in roles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, role));
                        }
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Audience = _appSettings.AuthenticationSettings.ValidAudience,
                            Issuer = _appSettings.AuthenticationSettings.ValidIssuer,
                            Subject = new ClaimsIdentity(authClaims),
                            Expires = DateTime.UtcNow.AddHours(3),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                        var token = tokenHandler.WriteToken(securityToken);
                        accountDto.Message = "Logged in successfully.";
                        accountDto.Token = token;
                        accountDto.IsSuccess = true;
                        accountDto.User = user;
                    }
                    else
                    {
                        accountDto.Message = "Invalid password ";
                    }
                }
            }
            catch (Exception ex)
            {
                accountDto.Message = ex.Message;
            }
            return accountDto;
        }

        public async Task<AccountDto> Register(RegisterModel model)
        {
            AccountDto accountDto = new();
            try
            {
                var user = new ApplicationUser()
                {
                    UserName = model.NationalID,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    NationalID = model.NationalID,
                    PhoneNumber = model.PhoneNumber,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        await SendEmailConfirmationLink(user, model.ClientURI);
                    }
                    await _userManager.AddToRoleAsync(user, "Applicant");
                    accountDto.Message = "Registration Successful. Check your email to confirm";
                    accountDto.NationalID = model.NationalID;
                    accountDto.User = user;
                    accountDto.IsSuccess = true;
                }
                else
                {
                    List<string> errorMgs = new();
                    foreach (var error in result.Errors)
                    {
                        errorMgs.Add(error.Description);
                    }
                    string message = string.Join(",", errorMgs.ToArray());
                    accountDto.Message = message;
                }
            }
            catch (Exception ex)
            {
                accountDto.IsSuccess = false;
                if (ex.InnerException != null)
                    accountDto.Message = ex.InnerException.Message.Replace("Cannot insert duplicate key row in object 'dbo.AspNetUsers' with unique index","Duplicate");
                else
                    accountDto.Message = ex.Message;
            }
            return accountDto;
        }

        public async Task<AccountDto> ResetPassword(ResetPasswordModel model)
        {
            AccountDto accountDto = new();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                accountDto.Message = "Invalid Request. An account with this email address doesnt exist";
            else
            {
                var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (!resetPassResult.Succeeded)
                {
                    var errors = resetPassResult.Errors.Select(e => e.Description);
                    accountDto.Message = errors.First();
                }
                else
                {
                    accountDto.Message = "Password reset successfully. Proceed to login";
                    accountDto.IsSuccess = true;
                }
            }
            return accountDto;  
        }

        public async Task<string> SendEmailConfirmationLink(ApplicationUser user, string clientURI)
        {
            string msg = "Open your mail to confirm your account";
            try
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var param = new Dictionary<string, string>
            {
                {"token", code },
                {"email", user.NationalID }
            };
                var callbackUrl = QueryHelpers.AddQueryString(clientURI, param);
                await _notificationService.SendEmailConfirmationAsync(user, callbackUrl.ToString());

                return msg;
            }
            catch (Exception ex) { 
                msg = ex.Message;   
                return msg; 
            }
        }

        public async Task<AccountDto> Logout(LogOutModel model)
        {
            AccountDto accountDto = new();
            return accountDto;
        }
    }
}

