using System;
using System.Text.Json.Serialization; // تم إضافة هذا السطر

namespace BloodLink.Models
{
    public class Donation
    {
        public int DonationID { get; set; }
        public int UserID { get; set; }
        public DateTime DonationDate { get; set; } = DateTime.Now;
        public string BloodType { get; set; }
        public string Status { get; set; } = "Pending";

        // ✅ حقول جديدة لاستقبال البيانات الحقيقية من صفحة الحجز
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public double? Weight { get; set; }
        public string? Medications { get; set; }
        public string? RecentSurgery { get; set; }
        public string? DonatedBefore { get; set; }
        public string? RecentInfection { get; set; }
        public string? CenterName { get; set; }

        public int? HospitalRequestID { get; set; }

        // تم إضافة JsonIgnore لمنع تكرار البيانات في الـ JSON (حل مشكلة الـ Cycle)
        [JsonIgnore]
        public HospitalRequest? HospitalRequest { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public BloodBank? BloodBank { get; set; }
    }
}