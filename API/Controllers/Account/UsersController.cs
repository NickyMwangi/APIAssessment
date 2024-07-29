using Business.IProcesses.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Library.Models.AccountModel;

namespace API.Controllers.Account
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IManageUsersProcess _manage;
        public UsersController(IManageUsersProcess manage)
        {
            _manage = manage;
        }

        [HttpPost]
        [Route("reset")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ResetVendor([FromBody] UserBodyRequest req)
        {
            var response = await _manage.ResetVendorPassword(req.ApplicationNo,req.ClientURI);
            return Ok(response);
        }
    }
}
