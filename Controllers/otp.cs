using BloodLink.Data;
using BloodLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace BloodLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly BloodLinkContext _context;

        public OTPController(BloodLinkContext context)
        {
            _context = context;
        }

        [HttpGet("GetOTP")]
        public async Task<ActionResult<IEnumerable<OTP>>> GetOtps()
        {
            return await _context.Otps.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OTP>> GetOtp(int id)
        {
            var otp = await _context.Otps.FindAsync(id);

            if (otp == null)
                return NotFound();

            return otp;
        }

        [HttpPost("CreateOTP")]
        public async Task<ActionResult<OTP>> CreateOtp(OTP otp)
        {
            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOtp), new { id = otp.OTPID }, otp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOtp(int id, OTP otp)
        {
            if (id != otp.OTPID)
                return BadRequest();

            _context.Entry(otp).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOtp(int id)
        {
            var otp = await _context.Otps.FindAsync(id);
            if (otp == null)
                return NotFound();

            _context.Otps.Remove(otp);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
