using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodLink.Data;
using BloodLink.Models;

namespace BloodLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : ControllerBase
    {
        private readonly BloodLinkContext _context;

        public DonationsController(BloodLinkContext context)
        {
            _context = context;
        }

        // ✅ تم تعديل هذه الميثود لحل مشكلة الـ Error 500
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try 
            {
                // سحب التبرعات الأساسية فقط لضمان استقرار الطلب
                var donations = await _context.Donations
                    .AsNoTracking()
                    .OrderByDescending(d => d.DonationDate)
                    .ToListAsync();

                return Ok(donations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching donations", details = ex.Message });
            }
        }

        [HttpPost("CreateDonation")]
        public async Task<IActionResult> Create([FromBody] Donation donation)
        {
            ModelState.Clear();

            try 
            {
                // تم إزالة الأسطر التي كانت تسبب تصفير العلاقات لضمان ربط الـ HospitalRequestID
                if (string.IsNullOrEmpty(donation.Status))
                    donation.Status = "Pending";

                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Donation created successfully", id = donation.DonationID });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message = $"Database Error: {innerMessage}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null) return NotFound();

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Deleted Successfully" });
        }
    }
}