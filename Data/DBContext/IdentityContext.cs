using Data.seed.Roles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Library.Models;
using Data.Entities;

namespace Data.DBContext;

public class IdContext : IdentityDbContext<ApplicationUser>
{
    public IdContext(DbContextOptions<IdContext> opts) : base(opts)
    {
    }
    public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }
    public virtual DbSet<ScheduledEmail> ScheduledEmails { get; set; }
}
