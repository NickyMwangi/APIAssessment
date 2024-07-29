using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Library.Models.Service
{
    public class SmtpSettingsModel
    {
        public SmtpSettingsModel()
        {
            SmtpServer = "localhost";
            SmtpPort = 25;
            EnableSsl = false;
            SmtpFromDisplayName = "Scholarship";
        }
        [DisplayName("SMTP Server Name")]
        [Required]
        public string SmtpServer { get; set; } = string.Empty;

        [DisplayName("SMTP Port")]
        public int? SmtpPort { get; set; } = 80;

        [DisplayName("Enable SSL")]
        public bool? EnableSsl { get; set; } = false;

        [DisplayName("SMTP User Name")]
        public string SmtpUserName { get; set; } = string.Empty;

        [DisplayName("SMTP Password")]
        [DataType(DataType.Password)]
        public string SmtpPassword { get; set; } = string.Empty;

        [DisplayName("SMTP From Address")]
        [Required]
        [EmailAddress]
        public string SmtpFromAddress { get; set; } = string.Empty;

        [DisplayName("SMTP From Display Name")]
        [Required]
        public string SmtpFromDisplayName { get; set; } = string.Empty;

        [DisplayName("Max. Retry Count")]
        [Required]
        public int MaxRetryCount { get; set; } = 0;
    }
}
