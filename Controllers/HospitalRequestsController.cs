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

        // GET: api/HospitalRequests/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            // استخدام AsNoTracking يضمن الحصول على أحدث بيانات من الداتابيز مباشرة
            var list = await _context.HospitalRequests
                .AsNoTracking()
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/HospitalRequests/GetByHospital/5
        [HttpGet("GetByHospital/{hospitalUserId:int}")]
        public async Task<IActionResult> GetByHospital(int hospitalUserId)
        {
            var list = await _context.HospitalRequests
                .AsNoTracking()
                .Where(r => r.HospitalUserID == hospitalUserId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(list);
        }

        // GET: api/HospitalRequests/10
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var req = await _context.HospitalRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RequestID == id);

            if (req == null)
                return NotFound(new { message = "Request not found" });

            return Ok(req);
        }

        // POST: api/HospitalRequests/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] HospitalRequest model)
        {
            if (model == null)
                return BadRequest(new { message = "Invalid body" });

            // التحقق من صحة البيانات (Validation)
            if (model.HospitalUserID <= 0 || string.IsNullOrWhiteSpace(model.HospitalName) || 
                string.IsNullOrWhiteSpace(model.PatientName) || model.Amount <= 0)
            {
                return BadRequest(new { message = "Please fill all required fields correctly." });
            }

            // استخدام UtcNow لتوحيد التوقيت ومنع مشاكل الظهور
            model.CreatedAt = DateTime.UtcNow;

            _context.HospitalRequests.Add(model);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Request created successfully",
                data = model
            });
        }

        // PUT: api/HospitalRequests/Update/10
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] HospitalRequest model)
        {
            var existing = await _context.HospitalRequests
                .FirstOrDefaultAsync(r => r.RequestID == id);

            if (existing == null)
                return NotFound(new { message = "Request not found" });

            // تحديث الحقول
            existing.PatientName = model.PatientName ?? existing.PatientName;
            existing.Amount = model.Amount > 0 ? model.Amount : existing.Amount;
            existing.BloodType = model.BloodType ?? existing.BloodType;
            existing.Urgency = model.Urgency ?? existing.Urgency;
            existing.Contact = model.Contact ?? existing.Contact;
            existing.Location = model.Location ?? existing.Location;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Request updated",
                data = existing
            });
        }

        // DELETE: api/HospitalRequests/Delete/10
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var req = await _context.HospitalRequests
                .FirstOrDefaultAsync(r => r.RequestID == id);

            if (req == null)
                return NotFound(new { message = "Request not found" });

            _context.HospitalRequests.Remove(req);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Request deleted" });
        }

        // GET: api/HospitalRequests/GetAllForUser
        [HttpGet("GetAllForUser")]
        public async Task<IActionResult> GetAllForUser()
        {
            // تم التأكد من جلب كل البيانات مرتبة من الأحدث للأقدم
            var list = await _context.HospitalRequests
                .AsNoTracking()
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(list);
        }
    }
}