// data/Dbcontext.cs
using BloodLink.Models;
using Microsoft.EntityFrameworkCore;

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

        // ✅ NEW: Hospital Requests (طلبات المستشفيات)
        public DbSet<HospitalRequest> HospitalRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<BloodBank>().ToTable("bloodbanks");
            modelBuilder.Entity<Donation>().ToTable("donations");
            modelBuilder.Entity<OTP>().ToTable("otps");

            // ✅ NEW
            modelBuilder.Entity<HospitalRequest>().ToTable("hospitalrequests");
        }
    }
}