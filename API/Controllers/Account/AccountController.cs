using Business.IProcesses.Account;
using Microsoft.AspNetCore.Mvc;
using static Library.Models.AccountModel;

namespace API.Controllers.Account
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountProcess _account;
        public AccountController(IAccountProcess account)
        {
            _account = account;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _account.Login(model);
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        public async Task<object> Register([FromBody] RegisterModel model)
        {
            var response = await _account.Register(model);
            return Ok(response);
        }


        [HttpPost]
        [Route("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmModel model)
        {
            var response = await _account.ConfirmEmail(model.Email, model.Code);
            return Ok(response);
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var response = await _account.ForgotPassword(model);
            return Ok(response);
        }


        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var response = await _account.ResetPassword(model);
            return Ok(response);
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> LogOut([FromBody] LogOutModel model)
        {
            var response = await _account.Logout(model);
            return Ok(response);
        }
    }
}
