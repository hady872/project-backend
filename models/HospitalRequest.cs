// models/HospitalRequest.cs
using System.ComponentModel.DataAnnotations;

namespace BloodLink.Models
{
    public class HospitalRequest
    {
        [Key]
        public int RequestID { get; set; }

        // صاحب الطلب (حساب المستشفى)
        [Required]
        public int HospitalUserID { get; set; }

        [Required]
        [MaxLength(200)]
        public string HospitalName { get; set; }

        // اسم المريض
        [Required]
        [MaxLength(200)]
        public string PatientName { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        [MaxLength(100)]
        public string BloodType { get; set; }

        [Required]
        [MaxLength(50)]
        public string Urgency { get; set; }

        [Required]
        [MaxLength(200)]
        public string Contact { get; set; }

        [Required]
        [MaxLength(300)]
        public string Location { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}