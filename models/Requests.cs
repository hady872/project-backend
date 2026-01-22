// models/Requests.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodLink.Models
{
    public class Request
    {
        [Key]
        public int RequestID { get; set; }

        // صاحب الطلب (المستشفى = User)
        [Required]
        public int HospitalUserID { get; set; }

        // اسم المستشفى (Snapshot وقت إنشاء الطلب)
        [Required]
        [MaxLength(200)]
        public string HospitalName { get; set; } = string.Empty;

        // اسم المريض
        [Required]
        [MaxLength(200)]
        public string PatientName { get; set; } = string.Empty;

        // عدد الوحدات
        [Required]
        public int Amount { get; set; }

        // وسيلة تواصل (Email أو Phone)
        [Required]
        [MaxLength(200)]
        public string Contact { get; set; } = string.Empty;

        // مكان المستشفى/الطلب
        [Required]
        [MaxLength(300)]
        public string Location { get; set; } = string.Empty;

        // فصيلة الدم
        [Required]
        [MaxLength(5)]
        public string BloodType { get; set; } = string.Empty;

        // urgency: high/medium/low
        [Required]
        [MaxLength(20)]
        public string Urgency { get; set; } = string.Empty;

        // وقت الإنشاء الحقيقي في الداتابيز
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // علاقة اختيارية مع جدول Users (لو عندك Navigation في User)
        [ForeignKey(nameof(HospitalUserID))]
        public User? HospitalUser { get; set; }
    }
}