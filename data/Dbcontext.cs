using BloodLink.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace BloodLink.Data
{
    public class BloodLinkContext : DbContext
    {
        public BloodLinkContext(DbContextOptions<BloodLinkContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<BloodBank> BloodBanks { get; set; } 
        public DbSet<Donation> Donations { get; set; } 
        public DbSet<OTP> Otps { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // مثال لو اسم الجدول مختلف في MySQL
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<BloodBank>().ToTable("bloodbanks");
            modelBuilder.Entity<Donation>().ToTable("donations");
            modelBuilder.Entity<OTP>().ToTable("otps");
        }
    }
}
