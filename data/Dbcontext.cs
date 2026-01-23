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
        public DbSet<HospitalRequest> HospitalRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<BloodBank>().ToTable("bloodbanks");
            modelBuilder.Entity<Donation>().ToTable("donations");
            modelBuilder.Entity<OTP>().ToTable("otps");
            modelBuilder.Entity<HospitalRequest>().ToTable("hospitalrequests");

            // ✅ جعل العلاقة مع طلب المستشفى اختيارية
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.HospitalRequest)
                .WithMany()
                .HasForeignKey(d => d.HospitalRequestID)
                .OnDelete(DeleteBehavior.SetNull);

            // ✅ الحل الجديد: جعل العلاقة مع بنك الدم اختيارية تماماً لمنع خطأ الـ NULL
            modelBuilder.Entity<Donation>()
                .HasOne(d => d.BloodBank)
                .WithMany()
                .HasForeignKey("BloodBankBankID") // نفس الاسم اللي ظهر في رسالة الخطأ عندك
                .IsRequired(false); 
        }
    }
}