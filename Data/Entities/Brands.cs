using Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Brands: BaseEntity
    {
        [Key]
        [Column(name: "brand_id")]
        public int Brand_id { get; set; }

        [Column(name: "brand_name")]
        public string Brand_name { get; set; } = string.Empty;
    }
}
