using Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

[Table("products", Schema = "production")]
public class Product 
{
    [Key]
    [Column(name: "product_id")]
    public int Product_id { get; set; }

    [Column(name: "product_name")]
    public string Product_name { get; set; } = string.Empty;

    [Column(name: "brand_id")]
    public int Brand_id { get; set; }

    [Column(name: "category_id")]
    public int Category_id { get; set; }

    [Column(name: "model_year")]
    public Int16 Model_year { get; set; }

    [Column(name: "list_price", TypeName = "decimal(10,2)")]
    public decimal List_price { get; set; }

    //[InverseProperty(nameof(Order_items.Product))]
    //public virtual ICollection<Order_items>? OrderItemsList { get; set; }
}
