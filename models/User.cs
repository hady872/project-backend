using System;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace BloodLink.Models
{
    public class User
    {
        public string AccountType { get; set; }  // "user" or "hospital"
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string BloodType { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        // العلاقات
        public ICollection<OTP> OTPs { get; set; }
        public ICollection<Donation> Donations { get; set; }
    }
}
