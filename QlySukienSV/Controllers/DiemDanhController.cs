using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Data;
using QlySukienSV.Models;
using System.Security.Claims;

namespace QlySukienSV.Controllers
{
    [Authorize]
    public class DiemDanhController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DiemDanhController(ApplicationDbContext context) { _context = context; }

        [HttpPost]
        public async Task<IActionResult> ThamGia(int suKienId)
        {
            var userId = int.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)!);

            // Lấy thông tin sự kiện
            var suKien = await _context.SuKiens.FindAsync(suKienId);
            if (suKien == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sự kiện!";
                return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
            }

            // Lấy giờ Việt Nam (UTC+7)
            var vietnamTime = DateTime.UtcNow.AddHours(7);

            // Kiểm tra thời gian điểm danh: trong khoảng sự kiện hoặc trước giờ bắt đầu tối đa 2 tiếng
            var thoiGianChoPhepBatDau = suKien.NgayBatDau.AddHours(-2);
            var thoiGianKetThuc = suKien.NgayKetThuc;

            if (vietnamTime < thoiGianChoPhepBatDau || vietnamTime > thoiGianKetThuc)
            {
                TempData["ErrorMessage"] = "Chưa đến thời gian điểm danh hoặc sự kiện đã kết thúc!";
                return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
            }

            // Kiểm tra đăng ký tham gia
            var dangKy = await _context.DangKySuKiens
                .FirstOrDefaultAsync(dk => dk.SuKienId == suKienId && dk.NguoiDungId == userId);

            if (dangKy == null)
            {
                TempData["ErrorMessage"] = "Bạn chưa đăng ký tham gia sự kiện này!";
                return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
            }

            if (dangKy.TrangThai != TrangThaiDangKy.DaDuyet)
            {
                TempData["ErrorMessage"] = "Đăng ký của bạn chưa được duyệt!";
                return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
            }

            // Kiểm tra xem đã điểm danh chưa
            var exists = await _context.DiemDanhSuKiens.AnyAsync(x => x.SuKienId == suKienId && x.NguoiDungId == userId);
            if (!exists)
            {
                _context.DiemDanhSuKiens.Add(new DiemDanhSuKien
                {
                    SuKienId = suKienId,
                    NguoiDungId = userId,
                    ThoiGianDiemDanh = vietnamTime,
                    HinhThuc = "DiemDanhThuong"
                });

                // Cập nhật trạng thái đăng ký
                dangKy.TrangThai = TrangThaiDangKy.DaThamGia;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Điểm danh thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn đã điểm danh trước đó!";
            }
            return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
        }

        // Action này sẽ được gọi khi quét QR code
        [HttpGet]
        public async Task<IActionResult> QuetQR(int suKienId)
        {
            if (!User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Login", "Auth", new { returnUrl = Url.Action("QuetQR", "DiemDanh", new { suKienId }) });
            }

            var userId = int.Parse(User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier)!);

            // Lấy thông tin sự kiện
            var suKien = await _context.SuKiens.FindAsync(suKienId);
            if (suKien == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sự kiện!";
                return RedirectToAction("Index", "Home");
            }

            // Lấy giờ Việt Nam (UTC+7)
            var vietnamTime = DateTime.UtcNow.AddHours(7);

            // Kiểm tra thời gian điểm danh
            var thoiGianChoPhepBatDau = suKien.NgayBatDau.AddHours(-2);
            var thoiGianKetThuc = suKien.NgayKetThuc;

            if (vietnamTime < thoiGianChoPhepBatDau || vietnamTime > thoiGianKetThuc)
            {
                TempData["ErrorMessage"] = "Chưa đến thời gian điểm danh hoặc sự kiện đã kết thúc!";
                return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
            }

            // Kiểm tra đăng ký tham gia
            var dangKy = await _context.DangKySuKiens
                .FirstOrDefaultAsync(dk => dk.SuKienId == suKienId && dk.NguoiDungId == userId);

            if (dangKy == null)
            {
                TempData["ErrorMessage"] = "Bạn chưa đăng ký tham gia sự kiện này!";
                return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
            }

            if (dangKy.TrangThai != TrangThaiDangKy.DaDuyet)
            {
                TempData["ErrorMessage"] = "Đăng ký của bạn chưa được duyệt!";
                return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
            }

            // Kiểm tra xem đã điểm danh chưa
            var exists = await _context.DiemDanhSuKiens.AnyAsync(x => x.SuKienId == suKienId && x.NguoiDungId == userId);
            if (!exists)
            {
                _context.DiemDanhSuKiens.Add(new DiemDanhSuKien
                {
                    SuKienId = suKienId,
                    NguoiDungId = userId,
                    ThoiGianDiemDanh = vietnamTime,
                    HinhThuc = "QR"
                });

                // Cập nhật trạng thái đăng ký
                dangKy.TrangThai = TrangThaiDangKy.DaThamGia;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Điểm danh bằng QR code thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Bạn đã điểm danh trước đó!";
            }
            return RedirectToAction("ChiTietSuKien", "Home", new { id = suKienId });
        }
    }
}
