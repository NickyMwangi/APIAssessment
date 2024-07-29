using Library.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.IProcesses.Account
{
    public interface IManageUsersProcess
    {
        Task<AccountDto> ResetVendorPassword(string vendorNo, string clientUrl);
    }
}
