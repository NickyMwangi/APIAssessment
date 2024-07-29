using Library.Dtos.Account;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Library.Models.AccountModel;

namespace Business.IProcesses.Account
{
    public interface IAccountProcess
    {
        #region account
        Task<AccountDto> Login(LoginModel model);
        Task<AccountDto> Register(RegisterModel model);
        Task<AccountDto> ConfirmEmail(string email, string code);
        Task<AccountDto> ForgotPassword(ForgotPasswordModel code);
        Task<AccountDto> ResetPassword(ResetPasswordModel code);
        Task<string> SendEmailConfirmationLink(ApplicationUser user, string clientURI);
        Task<AccountDto> Logout(LogOutModel model);
        #endregion
    }
}
