using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Data;

namespace QlySukienSV.Controllers
{
    [Authorize]
    public class LichController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LichController(ApplicationDbContext context) { _context = context; }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var eventsByDay = await _context.SuKiens
                .Include(s => s.CLB)
                .OrderBy(s => s.NgayBatDau)
                .GroupBy(s => s.NgayBatDau.Date)
                .Select(g => new { Ngay = g.Key, Items = g.ToList() })
                .ToListAsync();
            return View(eventsByDay);
        }
    }
}
