using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public class AccountModel
    {
        public class LoginModel
        {
            [Required]
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            [Required]
            public string ClientURI { get; set; } = string.Empty;
        }

        public class RegisterModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }=string.Empty;

            [Required]
            public string FirstName { get; set; } = string.Empty;

            [Required]
            public string MiddleName { get; set; } = string.Empty;

            [Required]
            public string LastName { get; set; } = string.Empty;

            [Required]
            [MaxLength(10)]
            [MinLength(6)]
            [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed")]
            public string NationalID { get; set; } = string.Empty;

            [Required]
            [MinLength(10)]
            [MaxLength(15)]
            public string PhoneNumber { get; set; } = string.Empty;

            [Required]
            public string ClientURI { get; set; } = string.Empty;

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public class ConfirmModel
        {
            public string UserId { get; set; }= string.Empty;
            public string Code { get; set; } = string.Empty;
            public string ClientURI { get; set; } = string.Empty;
            public string RoleName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        public class ResetPasswordModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = string.Empty;
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
        }

        public class ForgotPasswordModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
            [Required]
            public string ClientURI { get; set; } = string.Empty;
        }

        public class LogOutModel
        {
            public string Username { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;
        }

        public class UserBodyRequest
        {
            public string ApplicationNo { get; set; } = string.Empty;
            public string ClientURI { get; set; } = string.Empty;
        }
    }
}
