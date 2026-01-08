using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloodLink.Models
{
    public class BloodBank
    {
        [Key]
        public int BankID { get; set; }
        public string BankName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // العلاقة مع التبرعات
        public ICollection<Donation> Donations { get; set; }
    }
}
