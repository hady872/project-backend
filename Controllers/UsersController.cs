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

        // ===================== DTOs (داخل نفس الملف عادي) =====================
        public class RegisterDto
        {
            [Required] public string FullName { get; set; }
            [Required] public string Email { get; set; }
            [Required] public string PasswordHash { get; set; }
            [Required] public string City { get; set; }
            [Required] public string Phone { get; set; }
            [Required] public string BloodType { get; set; }
        }

        public class ForgotPasswordDto
        {
            [Required] public string Email { get; set; }
        }
        // =====================================================================

        // ---------------- Register ----------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(x => x.Email == dto.Email))
                return BadRequest(new { message = "Email already exists" });

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                City = dto.City,
                Phone = dto.Phone,
                BloodType = dto.BloodType,
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

    var user = await _context.Users
        .FirstOrDefaultAsync(x =>
            x.Email == login.Email &&
            x.PasswordHash == login.PasswordHash
        );

    if (user == null)
        return Unauthorized(new { message = "Invalid email or password" });

    return Ok(new
    {
        message = "Login successful",
        user = new
        {
            user.UserID,
            user.FullName,
            user.Email,
            user.Phone,
            user.BloodType
        }
    });
}

        // ---------------- Forgot Password ----------------
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
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

