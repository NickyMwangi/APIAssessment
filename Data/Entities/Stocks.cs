using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;

namespace Data.Entities
{
    public class Stocks : BaseEntity
    {
        [Key]
        [Column(name: "store_id")]
        public int Store_id { get; set; }

        [Column(name: "product_id")]
        public int Product_id { get; set; }

        [Column(name: "quantity")]
        public int Quantity { get; set; }

    }
}
