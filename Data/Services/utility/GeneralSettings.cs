using Data.Interfaces;
using Data.Entities;
using Library.Helpers;
using Library.Models;
using Library.Models.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.utility;

public static class GeneralSettings
{
    public static SmtpSettingsModel SmtpSettings { get; set; }

    public static SmtpSettingsModel GetSmtpSettings(IRepoService repo)
    {
        if (SmtpSettings == null)
        {
            SmtpSettings = new SmtpSettingsModel();
            var smtpConfig = repo.Where<GeneralSetting>(m => m.SettingType.ToLower() == "smtp");
            if (smtpConfig.Any())
                SmtpSettings = JsonConvert.DeserializeObject<SmtpSettingsModel>(smtpConfig.First().SettingValue);

        }
        return SmtpSettings;
    }

    
}
