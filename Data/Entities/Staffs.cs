using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;

namespace Data.Entities;    

[Table("staffs", Schema = "sales")]
public class Staffs 
{
    [Key]
    [Column(name: "staff_id")]
    public int Staff_id { get; set; }

    [Column(name: "first_name")]
    public string First_name { get; set; } = string.Empty;

    [Column(name: "last_name")]
    public string Last_name { get; set; } = string.Empty;

    [Column(name: "phone")]
    public string Phone { get; set; } = string.Empty;

    [Column(name: "email")]
    public string Email { get; set; } = string.Empty;

    [Column(name: "active")]
    public Byte Active { get; set; }

    [Column(name: "store_id")]
    public int Store_id { get; set; }

    [Column(name: "Manager_id")]
    public int Manager_id { get; set; }


}
