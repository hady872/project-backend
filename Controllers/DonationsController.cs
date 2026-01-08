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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var donations = await _context.Donations
                .Include(d => d.User)
                .Include(d => d.BloodBank)
                .ToListAsync();

            return Ok(donations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var donation = await _context.Donations
                .Include(d => d.User)
                .Include(d => d.BloodBank)
                .FirstOrDefaultAsync(d => d.DonationID == id);

            if (donation == null)
                return NotFound(new { message = "Donation not found" });

            return Ok(donation);
        }

        [HttpPost("CreateDonation")]
        public async Task<IActionResult> Create(Donation donation)
        {
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Donation created", data = donation });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Donation donation)
        {
            if (id != donation.DonationID)
                return BadRequest(new { message = "ID mismatch" });

            _context.Entry(donation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Donation updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
                return NotFound(new { message = "Donation not found" });

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Donation deleted" });
        }
    }
}
