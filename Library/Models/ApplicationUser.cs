using Library.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{

    [Index(nameof(FirstName), Name ="First name", IsUnique = true)]
    [Index(nameof(MiddleName), Name = "Middle name", IsUnique = true)]
    [Index(nameof(LastName), Name = "Last name", IsUnique = true)]
    [Index(nameof(NationalID), Name = "National ID", IsUnique = true)]
    [Index(nameof(PhoneNumber), Name = "Phone number", IsUnique = true)]
    public partial class ApplicationUser:  IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string NationalID { get; set; } = string.Empty;
    }
}
