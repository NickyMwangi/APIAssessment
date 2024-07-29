using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos
{
    public class OrderLineDto:BaseLineDto
    {
        public int Order_id { get; set; }
        public int Item_id { get; set; }
        public int Product_id { get; set; }
        public int Quantity { get; set; }
        public decimal List_price { get; set; }
        public decimal Discount { get; set; }
        public OrderDto? Header { get; set; }
        public ProductDto? Product { get; set; }
        public CustomerDto? customer { get; set; }
    }
}
