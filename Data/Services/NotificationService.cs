using Data.Interfaces;
using Data.Entities;
using Data.Services.utility;
using Library.Models;
using Library.Models.Service;

namespace Data.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepoService repo;
        private readonly IEmailService emailService;
        private readonly SmtpSettingsModel smtpSetting;
        public NotificationService(IRepoService _repo, IEmailService _emailService)
        {
            repo = _repo;
            emailService = _emailService;
            smtpSetting = GeneralSettings.SmtpSettings ?? GeneralSettings.GetSmtpSettings(repo);
        }


        public async Task<int> ScheduleEmailNotificationAsync(string emailTo, string subject, string htmlBody, string module,
            string refId = null, string refText = null, string emailCC = null)
        {
            if (!string.IsNullOrWhiteSpace(emailTo))
            {
                var footer = "<b/><b/>This Email is a system auto generated. Please do not reply.";
                var msgHtml = $"{htmlBody}{footer}";
                var uid = Guid.NewGuid().ToString();
                var schEmail = new ScheduledEmail()
                {
                    Id = uid,
                    Module = module,
                    ReferenceId = refId ?? uid,
                    ReferenceDescription = $"{refText} at {DateTime.Today}",
                    EmailTo = emailTo,
                    EmailCC = emailCC,
                    Subject = $"RE: KoTDA Recruitment Portal - {subject}",
                    HtmlBody = msgHtml,
                    IsHtml = true,
                    Sent = false,
                    RetryCount = 0,
                    CreatedBy = emailTo,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };
                repo.Insert(schEmail);
                await repo.SaveAsync();
                return 1;
            }
            return 0;
        }



        public async Task SendScheduledMailAsync()
        {
            var retryCnt = smtpSetting.MaxRetryCount < 1 ? 3 : smtpSetting.MaxRetryCount;
            var scheduledEmailsList = repo.Where<ScheduledEmail>(m => m.Sent == false && m.RetryCount < retryCnt);
            if (scheduledEmailsList.Any())
            {
                foreach (var smail in scheduledEmailsList)
                {
                    var _sent = await emailService.SendEmailAsync(smail.EmailTo, smail.Subject, smail.HtmlBody, smail.EmailCC);
                    smail.Sent = (bool)_sent[0];
                    smail.RetryCount += 1;
                    smail.ErrorMessage = _sent[1].ToString();
                    repo.Update(smail);
                }
                await repo.SaveAsync();
            }
        }


    }
}
