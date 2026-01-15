using BloodLink.Data;
using BloodLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BloodLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BloodLinkContext _context;

        public UsersController(BloodLinkContext context)
        {
            _context = context;
        }

        // ===================== DTOs =====================
        public class RegisterDto
        {
            [Required] public string FullName { get; set; }
            [Required] public string Email { get; set; }
            [Required] public string PasswordHash { get; set; }
            [Required] public string City { get; set; }
            [Required] public string Phone { get; set; }
            [Required] public string BloodType { get; set; }

            // ✅ الجديد
            [Required] public string AccountType { get; set; } // "user" or "hospital"
        }

        public class ForgotPasswordDto
        {
            [Required] public string Email { get; set; }
        }

        // ✅ DTO جديد لتعديل نوع الحساب
        public class UpdateAccountTypeDto
        {
            [Required] public string Email { get; set; }
            [Required] public string AccountType { get; set; } // "user" or "hospital"
        }
        // =================================================

        // ---------------- Register ----------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = dto.Email.Trim().ToLower();
            var accountType = dto.AccountType.Trim().ToLower();

            if (accountType != "user" && accountType != "hospital")
                return BadRequest(new { message = "AccountType must be 'user' or 'hospital'." });

            if (await _context.Users.AnyAsync(x => x.Email.ToLower() == email))
                return BadRequest(new { message = "Email already exists" });

            var user = new User
            {
                FullName = dto.FullName.Trim(),
                Email = email,
                PasswordHash = dto.PasswordHash,
                City = dto.City.Trim(),
                Phone = dto.Phone.Trim(),
                BloodType = dto.BloodType.Trim(),
                AccountType = accountType, // ✅ هنا
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully", user });
        }

        // ---------------- Login ----------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = login.Email.Trim().ToLower();

            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email.ToLower() == email &&
                    x.PasswordHash == login.PasswordHash
                );

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            // ✅ مهم: رجّع accountType للفرونت عشان يقرر يروح فين
            return Ok(new
            {
                message = "Login successful",
                user = new
                {
                    user.UserID,
                    user.FullName,
                    user.Email,
                    user.Phone,
                    user.BloodType,
                    user.AccountType
                }
            });
        }

        // ✅ ---------------- Update Account Type ----------------
        // الهدف: نصلّح اليوزرات القديمة اللي accountType عندها فاضي
        [HttpPut("account-type")]
        public async Task<IActionResult> UpdateAccountType([FromBody] UpdateAccountTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = dto.Email.Trim().ToLower();
            var accountType = dto.AccountType.Trim().ToLower();

            if (accountType != "user" && accountType != "hospital")
                return BadRequest(new { message = "AccountType must be 'user' or 'hospital'." });

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);
            if (user == null)
                return NotFound(new { message = "User not found" });

            user.AccountType = accountType;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "AccountType updated successfully",
                user = new
                {
                    user.UserID,
                    user.FullName,
                    user.Email,
                    user.AccountType
                }
            });
        }

        // ---------------- Forgot Password ----------------
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = dto.Email.Trim().ToLower();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email);
            if (user == null)
                return NotFound(new { message = "Email not found" });

            var otp = new OTP
            {
                UserID = user.UserID,
                OTPCode = new Random().Next(100000, 999999).ToString(),
                ExpirationDate = DateTime.Now.AddMinutes(5)
            };

            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();

            return Ok(new { message = "OTP sent", otp = otp.OTPCode });
        }

        // ---------------- Get Profile ----------------
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            return Ok(user);
        }

        // ---------------- Update Profile ----------------
        [HttpPut("profile/{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] User updated)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });

            user.FullName = updated.FullName;
            user.Phone = updated.Phone;
            user.BloodType = updated.BloodType;
            user.City = updated.City;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully", user });
        }
    }
}
// U09: Sync backend with frontend changes