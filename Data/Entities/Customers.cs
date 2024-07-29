using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;

namespace Data.Entities;

[Table("customers", Schema = "sales")]
public class Customers 
{
    [Key]
    [Column(name: "customer_id")]
    public int Customer_id { get; set; }

    [Column(name: "first_name")]
    public string First_name { get; set; } = string.Empty;

    [Column(name: "last_name")]
    public string Last_name { get; set; } = string.Empty;

    [Column(name: "phone")]
    public string? Phone { get; set; } 

    [Column(name: "email")]
    public string? Email { get; set; }

    [Column(name: "street")]
    public string Street { get; set; } = string.Empty;

    [Column(name: "city")]
    public string City { get; set; } = string.Empty;

    [Column(name: "state")]
    public string State { get; set; } = string.Empty;

    [Column(name: "zip_code")]
    public string Zip_code { get; set; } = string.Empty;
}
