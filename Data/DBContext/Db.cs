using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DBContext
{
    public partial class Db : DbContext
    {
        public Db() { }

        public Db(DbContextOptions<Db> options)
            : base(options)
        {
        }
        public virtual DbSet<Brands> brands { get; set; }
        public virtual DbSet<Categories> categories { get; set; }
        public virtual DbSet<Product> products { get; set; }
        public virtual DbSet<Stocks> stocks { get; set; }
        public virtual DbSet<Customers> customers { get; set; }
        public virtual DbSet<Order_items> order_items { get; set; }
        public virtual DbSet<Order> orders { get; set; }
        public virtual DbSet<Staffs> staffs { get; set; }
        public virtual DbSet<Stores> stores { get; set; }
        public virtual DbSet<GeneralSetting> GeneralSettings { get; set; }
        public virtual DbSet<ScheduledEmail> ScheduledEmails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
