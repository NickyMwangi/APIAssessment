using Library.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities;

public partial class ScheduledEmail : BaseEntity
{
    [Key]
    [StringLength(128)]
    public virtual string Id { get; set; } = Guid.NewGuid().ToString();

    [StringLength(50)]
    public string Module { get; set; } = string.Empty;
    [StringLength(128)]
    public string ReferenceId { get; set; } = string.Empty;
    [StringLength(150)]
    public string ReferenceDescription { get; set; } = string.Empty;
    [Required]
    public string EmailTo { get; set; } = string.Empty;
    [Column("EmailCC")]
    public string EmailCC { get; set; } = string.Empty;
    [Required]
    [StringLength(80)]
    public string Subject { get; set; } = string.Empty;
    [Required]
    public string HtmlBody { get; set; } = string.Empty;
    public bool? IsHtml { get; set; }
    public bool? Sent { get; set; }
    public int? RetryCount { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}

