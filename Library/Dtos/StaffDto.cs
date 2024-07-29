using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos
{
    public class StaffDto : BaseDto<BaseLineDto>
    {
        public int Staff_id { get; set; }
        public string First_name { get; set; } = string.Empty;
        public string Last_name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Byte Active { get; set; }
        public int Store_id { get; set; }
        public int Manager_id { get; set; }

    }
}
