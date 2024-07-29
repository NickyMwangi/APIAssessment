using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos.Account
{
    public class AccountDto
    {
        public string Message { get; set; } = "failed. Try again.";
        public bool IsSuccess { get; set; } = false;
        public ApplicationUser User { get; set; } = new ApplicationUser();
        public string Token { get; set; } = string.Empty;
        public string NationalID { get; set; } = string.Empty;
    }
}
