using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Dtos
{
    public class ProductDto : BaseDto<BaseLineDto>
    {
        public int Product_id { get; set; }
        public string Product_name { get; set; } = string.Empty;
        public int Brand_id { get; set; }
        public int Category_id { get; set; }
        public Int16 Model_year { get; set; }
        public decimal List_price { get; set; }
    }
}
