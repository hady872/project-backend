// Controllers/DonationsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodLink.Data;
using BloodLink.Models;

namespace BloodLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodBanksController : ControllerBase
    {
        private readonly BloodLinkContext _context;

        public BloodBanksController(BloodLinkContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllBloodBank")]
        public async Task<IActionResult> GetAll()
        {
            var banks = await _context.BloodBanks.ToListAsync();
            return Ok(banks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bank = await _context.BloodBanks.FindAsync(id);
            if (bank == null)
                return NotFound(new { message = "Blood bank not found" });

            return Ok(bank);
        }

        [HttpPost("CreateBloodBank")]
        public async Task<IActionResult> Create(BloodBank bank)
        {
            _context.BloodBanks.Add(bank);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Blood bank created", data = bank });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BloodBank bank)
        {
            if (id != bank.BankID)
                return BadRequest(new { message = "ID mismatch" });

            _context.Entry(bank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Blood bank updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var bank = await _context.BloodBanks.FindAsync(id);

            if (bank == null)
                return NotFound(new { message = "Blood bank not found" });

            _context.BloodBanks.Remove(bank);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Deleted successfully" });
        }
    }
}
