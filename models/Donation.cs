using System;

namespace BloodLink.Models
{
    public class Donation
    {
        public int DonationID { get; set; }
        public int UserID { get; set; }
        public int BankID { get; set; }
        public DateTime DonationDate { get; set; } = DateTime.Now;
        public string BloodType { get; set; }
        public string Status { get; set; } = "Pending";

        // العلاقات
        public User User { get; set; }
        public BloodBank BloodBank { get; set; }
    }
}
