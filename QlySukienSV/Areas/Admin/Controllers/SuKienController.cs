using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;
using QlySukienSV.Models;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SystemRoles.QuanTri + "," + SystemRoles.BanChapHanh)]
    public class SuKienController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SuKienController(ApplicationDbContext ctx) { _context = ctx; }
        public async Task<IActionResult> Index()
        {
            var list = await _context.SuKiens.Include(s => s.CLB).OrderByDescending(s => s.NgayBatDau).ToListAsync();

            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.CLBs = new SelectList(await _context.CLBs.OrderBy(c => c.TenCLB).ToListAsync(), "Id", "TenCLB");

            return View(new SuKien { NgayBatDau = DateTime.Now, NgayKetThuc = DateTime.Now.AddHours(2) });
        }
        [HttpPost]
        public async Task<IActionResult> Create(SuKien model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CLBs = new SelectList(await _context.CLBs.OrderBy(c => c.TenCLB).ToListAsync(), "Id", "TenCLB");
                return View(model);
            }
            _context.SuKiens.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã tạo sự kiện";


            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var x = await _context.SuKiens.FindAsync(id);
            if (x == null)
                return NotFound();
            ViewBag.CLBs = new SelectList(await _context.CLBs.OrderBy(c => c.TenCLB).ToListAsync(), "Id", "TenCLB");


            return View(x);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, SuKien model)
        {
            if (id != model.Id) return BadRequest();
            if (!ModelState.IsValid)
            {
                ViewBag.CLBs = new SelectList(await _context.CLBs.OrderBy(c => c.TenCLB).ToListAsync(), "Id", "TenCLB");
                return View(model);
            }
            _context.Update(model); await _context.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật"; return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var x = await _context.SuKiens.FindAsync(id);
            if (x == null)
                return NotFound();
            _context.SuKiens.Remove(x);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa";

            return RedirectToAction(nameof(Index));
        }
    }
}
