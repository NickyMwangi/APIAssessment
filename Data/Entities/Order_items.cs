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
    [Table("order_items", Schema = "sales")]
    public class Order_items 
    {

        [Column(name: "order_id")]
        public int? Order_id { get; set; }
        [Key]
        [Column(name: "item_id")]
        public int? Item_id { get; set; }

        [Column(name: "product_id")]
        public int? Product_id { get; set; }

        [Column(name: "quantity")]
        public int? Quantity { get; set; }

        [Column(name: "list_price", TypeName = "decimal(10,2)")]
        public decimal List_price { get; set; }

        [Column(name: "discount", TypeName = "decimal(4,2)")]
        public decimal Discount { get; set; }

        [ForeignKey(nameof(Order_id))]
        public virtual Order? Header { get; set; }

        [ForeignKey(nameof(Product_id))]
        public virtual Product? Product { get; set; }
    }
}
