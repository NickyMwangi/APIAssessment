using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Account
{
    public static class ManageUserModel
    {
        public class ForgotPasswordModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            public string ClientURI { get; set; } = string.Empty;
        }
    }
}
