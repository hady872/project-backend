using BloodLink.Data;
using BloodLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // ---------------- Register ----------------
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _context.Users.AnyAsync(x => x.Email == user.Email))
                return BadRequest(new { message = "Email already exists" });

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User created successfully", user });
        }

        // ---------------- Login ----------------
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == login.Email && x.PasswordHash == login.PasswordHash);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(new { message = "Login successful", user });
        }

        // ---------------- Forgot Password ----------------
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] dynamic data)
        {
            string email = data.email;

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
                return NotFound(new { message = "Email not found" });

            // Generate OTP
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

            await _context.SaveChangesAsync();

            return Ok(new { message = "Profile updated successfully", user });
        }
    }
}
