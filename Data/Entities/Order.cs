using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Castle.Core.Resource;

namespace Data.Entities;

[Table("orders", Schema = "sales")]
public class Order
{
    [Key]
    [Column(name: "order_id")]
    public int Order_id { get; set; }

    [Column(name: "customer_id")]
    public int Customer_id { get; set; }

    [Column(name: "store_id")]
    public int Store_id { get; set; }

    [Column(name: "staff_id")]
    public int Staff_id { get; set; }

    [Column(name: "order_status", TypeName = "tinyint)")]
    public byte Order_status { get; set; }

    [Column(name: "order_date")]
    public DateTime Order_date { get; set; } = DateTime.Now;

    [Column(name: "required_date")]
    public DateTime Required_date { get; set; } = DateTime.Now;

    [Column(name: "shipped_date")]
    public DateTime? Shipped_date { get; set; } = DateTime.Now;

    [ForeignKey(nameof(Customer_id))]
    public virtual Customers? Customer { get; set; }

    [ForeignKey(nameof(Store_id))]
    public virtual Stores? Store { get; set; }

    [ForeignKey(nameof(Staff_id))]
    public virtual Staffs? Staff { get; set; }
}