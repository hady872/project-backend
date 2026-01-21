using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloodLink.Data;
using BloodLink.Models;

namespace BloodLink.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalRequestsController : ControllerBase
    {
        private readonly BloodLinkContext _context;

        public HospitalRequestsController(BloodLinkContext context)
        {
            _context = context;
        }

        // ✅ GET: api/HospitalRequests/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Set<HospitalRequest>()
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(list);
        }

        // ✅ GET: api/HospitalRequests/GetByHospital/5
        [HttpGet("GetByHospital/{hospitalUserId:int}")]
        public async Task<IActionResult> GetByHospital(int hospitalUserId)
        {
            var list = await _context.Set<HospitalRequest>()
                .Where(r => r.HospitalUserID == hospitalUserId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(list);
        }

        // ✅ GET: api/HospitalRequests/10
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var req = await _context.Set<HospitalRequest>()
                .FirstOrDefaultAsync(r => r.RequestID == id);

            if (req == null)
                return NotFound(new { message = "Request not found" });

            return Ok(req);
        }

        // ✅ POST: api/HospitalRequests/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] HospitalRequest model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid body" });

            if (model.HospitalUserID <= 0)
                return BadRequest(new { message = "HospitalUserID is required" });

            if (string.IsNullOrWhiteSpace(model.HospitalName))
                return BadRequest(new { message = "HospitalName is required" });

            if (string.IsNullOrWhiteSpace(model.PatientName))
                return BadRequest(new { message = "PatientName is required" });

            if (model.Amount <= 0)
                return BadRequest(new { message = "Amount must be > 0" });

            if (string.IsNullOrWhiteSpace(model.BloodType))
                return BadRequest(new { message = "BloodType is required" });

            if (string.IsNullOrWhiteSpace(model.Urgency))
                return BadRequest(new { message = "Urgency is required" });

            if (string.IsNullOrWhiteSpace(model.Contact))
                return BadRequest(new { message = "Contact is required" });

            if (string.IsNullOrWhiteSpace(model.Location))
                return BadRequest(new { message = "Location is required" });

            model.CreatedAt = DateTime.Now;

            _context.Set<HospitalRequest>().Add(model);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Request created",
                data = model
            });
        }

        // ✅ PUT: api/HospitalRequests/Update/10
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] HospitalRequest model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid body" });

            var existing = await _context.Set<HospitalRequest>()
                .FirstOrDefaultAsync(r => r.RequestID == id);

            if (existing == null)
                return NotFound(new { message = "Request not found" });

            // ✅ تحديث الحقول المسموح بتعديلها
            if (!string.IsNullOrWhiteSpace(model.PatientName))
                existing.PatientName = model.PatientName.Trim();

            if (model.Amount > 0)
                existing.Amount = model.Amount;

            if (!string.IsNullOrWhiteSpace(model.BloodType))
                existing.BloodType = model.BloodType.Trim();

            if (!string.IsNullOrWhiteSpace(model.Urgency))
                existing.Urgency = model.Urgency.Trim().ToLower();

            if (!string.IsNullOrWhiteSpace(model.Contact))
                existing.Contact = model.Contact.Trim();

            if (!string.IsNullOrWhiteSpace(model.Location))
                existing.Location = model.Location.Trim();

            // ✅ Validation بعد التعديل
            if (existing.Amount <= 0)
                return BadRequest(new { message = "Amount must be > 0" });

            if (string.IsNullOrWhiteSpace(existing.PatientName))
                return BadRequest(new { message = "PatientName is required" });

            if (string.IsNullOrWhiteSpace(existing.BloodType))
                return BadRequest(new { message = "BloodType is required" });

            if (string.IsNullOrWhiteSpace(existing.Urgency))
                return BadRequest(new { message = "Urgency is required" });

            if (string.IsNullOrWhiteSpace(existing.Contact))
                return BadRequest(new { message = "Contact is required" });

            if (string.IsNullOrWhiteSpace(existing.Location))
                return BadRequest(new { message = "Location is required" });

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Request updated",
                data = existing
            });
        }

        // ✅ DELETE: api/HospitalRequests/Delete/10
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var req = await _context.Set<HospitalRequest>()
                .FirstOrDefaultAsync(r => r.RequestID == id);

            if (req == null)
                return NotFound(new { message = "Request not found" });

            _context.Set<HospitalRequest>().Remove(req);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Request deleted" });
        }
    }
}