using System;

namespace BloodLink.Models
{
    public class OTP
    {
        public int OTPID { get; set; }
        public int UserID { get; set; }
        public string OTPCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsVerified { get; set; }

        // العلاقة مع المستخدم
        public User User { get; set; }
    }
}
