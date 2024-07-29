using Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

public partial class GeneralSetting: BaseEntity
{

    [Key]
    [StringLength(128)]
    public virtual string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    [StringLength(128)]
    public string SettingType { get; set; } = string.Empty;
    public string SettingValue { get; set; }= string.Empty;
}
