using Data.Interfaces;
using Data.Services;
using Library.Common.Setting;
using MapsterMapper;
using JobScheduler;
using JobScheduler.Models;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;
using Business.IProcesses.Account;
using Business.Processes.Account;
using Business.IProcesses;
using Business.Processes;
using Business.IProcesses.shared;
using Business.Processes.shared;

namespace API.Configuration
{
    internal static class DInjection
    {
        internal static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            // General
            services.AddSingleton<IAppSettings, AppSettings>();
            services.AddTransient<IMapper, Mapper>();
            services.AddTransient<IMapperService, MapperService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<INotificationService, NotificationService>();

            //DB Repo
            services.AddScoped<IRepoService, RepoService>();

            //Emails Scheduler
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<JobRunner>();
            services.AddHostedService<JobHostedService>();
            services.AddTransient<EmailSenderJob>();
            services.AddSingleton(new JobSchedule(jobType: typeof(EmailSenderJob),cronExpression: "0 0/1 * * * ?")); //every 10 seconds


            //Processes
            services.AddTransient<IAccountProcess, AccountProcess>();
            services.AddTransient<IManageUsersProcess, ManageUsersProcess>();
            services.AddTransient<IOrderProcess, OrderProcess>();
            services.AddTransient<IOptionsProcess, OptionsProcess>();
            


        }
    }
}
