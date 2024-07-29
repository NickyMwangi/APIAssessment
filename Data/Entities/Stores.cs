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
    [Table("stores", Schema = "sales")]
    public class Stores 
    {
        [Key]
        [Column(name: "store_id")]
        public int Store_id { get; set; }

        [Column(name: "store_name")]
        public string Store_name { get; set; } = string.Empty;


        [Column(name: "phone")]
        public string Phone { get; set; } = string.Empty;

        [Column(name: "email")]
        public string Email { get; set; } = string.Empty;

        [Column(name: "street")]
        public string Street { get; set; } = string.Empty;

        [Column(name: "city")]
        public string City { get; set; } = string.Empty;

        [Column(name: "state")]
        public string State { get; set; } = string.Empty;

        [Column(name: "zip_code")]
        public string Zip_code { get; set; } = string.Empty;
    }
}
