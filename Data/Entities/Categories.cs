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
    public class Categories : BaseEntity
    {
        [Key]
        [Column(name: "brand_id")]
        public int category_id { get; set; }

        [Column(name: "category_name")]
        public string Category_name { get; set; } = string.Empty;
    }
}
