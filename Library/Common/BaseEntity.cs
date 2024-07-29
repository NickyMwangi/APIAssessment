using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public class BaseEntity
    {
        //[Key]
        //[StringLength(128)]
        //public virtual string Id { get; set; }= Guid.NewGuid().ToString();
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }= DateTime.Now;
        [Column("Created_by")]
        [StringLength(128)]
        public string CreatedBy { get; set; }= string.Empty;
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }=DateTime.Now;
        [StringLength(128)]
        public string ModifiedBy { get; set; }=string.Empty;
    }
}
