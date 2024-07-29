using Castle.Core.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos
{
    public class OrderDto:BaseDto<OrderLineDto>
    {
        public int Order_id { get; set; }
        public int Customer_id { get; set; }
        public int Store_id { get; set; }
        public int Staff_id { get; set; }
        public int Order_status { get; set; }
        public string  Delivery { get; set; }   = string.Empty;//Early,Late
        public DateTime Order_date { get; set; } = DateTime.Now;
        public DateTime Required_date { get; set; } = DateTime.Now;
        public DateTime Shipped_date { get; set; } = DateTime.Now;

        public CustomerDto? Customer { get; set; }

        public StoreDto? Store { get; set; }

        public StaffDto? Staff { get; set; }
    }
}
