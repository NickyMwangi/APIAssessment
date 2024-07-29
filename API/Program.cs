using API.Configuration;
using Data.DBContext;
using Library.Common.Setting;
using Library.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 1. Application settings
builder.Configuration.AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddHttpClient();

builder.Services.AddCors(options =>
{
    string[] methods = { "GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS" };
    options.AddPolicy("AssessmentPolicy", n => n.WithOrigins("*").AllowAnyHeader().WithMethods(methods));
});
// Serilog Configuration
var logDirectory = "ErrorLog";

if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
     .WriteTo.File($"{logDirectory}/{DateTime.Now:yyyy-MM-dd}/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Services.AddHttpClient();

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
.AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    var enumConverter = new JsonStringEnumConverter();
    opts.JsonSerializerOptions.Converters.Add(enumConverter);
});


builder.Services.AddDbContext<IdContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Store")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options => options.SignIn.RequireConfirmedAccount = true))
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<IdContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
});
builder.Services.ConfigureDependencyInjection();

// 2.6. Security Configuration
builder.Services.AddSecurity(new AppSettings(builder.Configuration));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDoc(new AppSettings(builder.Configuration));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

// 3.4 Handle no endpoint call
app.MapGet("/", () => Results.Ok());
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AssessmentPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.SeedData();
app.Run();
