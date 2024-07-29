using Business.IProcesses.Account;
using Business.Utility;
using Data.DBContext;
using Data.Extensions;
using Data.Interfaces;
using Data.Services;
using Library.Dtos.Account;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Business.Processes.Account
{
    public class ManageUsersProcess : IManageUsersProcess
    {
        private readonly UserManager<ApplicationUser> _userManager;
        protected readonly IRepoService _nav;
        private readonly INotificationService _notificationService;

        public ManageUsersProcess(UserManager<ApplicationUser> userManager, IRepoService nav, INotificationService notificationService)
        {
            _userManager = userManager;
            _nav = nav;
            _notificationService = notificationService;

        }
       

        public async Task<AccountDto> ResetVendorPassword(string email, string clientUrl)
        {
            AccountDto accountDto = new();
            var message = "Failed to reset password for the selected vendor";
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    accountDto.Message = "User Not Found!";
                    return accountDto;
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var param = new Dictionary<string, string>
                {
                    {"token", token },
                    {"email", email }
                };
                var callback = QueryHelpers.AddQueryString(clientUrl, param);
                await _notificationService.SendEmailResetPasswordAsync(user, callback);
                accountDto.IsSuccess = true;
                message = "Password reset successfull";
            }
            catch (Exception ex)
            {
                message = "Error resetting password." + ex.Message;
            }
            accountDto.Message = message;
            return accountDto;
        }




        public async Task<string> SendEmailConfirmationLink(ApplicationUser user, string clientURI, string password)
        {
            string msg = "Open your mail to confirm your account";
            try
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var param = new Dictionary<string, string>
            {
                {"token", code },
                {"email", user.Email }
            };
                var callbackUrl = QueryHelpers.AddQueryString(clientURI, param);
                await _notificationService.VendorEmailResetPasswordAsync(user, callbackUrl.ToString(), password);

                return msg;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return msg;
            }
        }
    }
}
