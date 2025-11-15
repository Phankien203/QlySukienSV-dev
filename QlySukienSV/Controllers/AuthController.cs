using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QlySukienSV.Data;
using QlySukienSV.ViewModels;
using System.Security.Claims;

namespace QlySukienSV.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var normalized = model.Identifier.Trim().ToUpperInvariant();
            var user = await _context.NguoiDungs
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => (u.MaSoSV != null && u.MaSoSV.ToUpper() == normalized) || u.Email.ToUpper() == normalized);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.MatKhau))
            {
                ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không hợp lệ.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.HoTen),
                new Claim(ClaimTypes.Role, user.Role!.TenRole),
                new Claim("MaSoSV", user.MaSoSV ?? string.Empty)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = model.RememberMe,
                    AllowRefresh = true
                });

            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserRole", user.Role!.TenRole);
            HttpContext.Session.SetString("UserName", user.HoTen);
            HttpContext.Session.SetString("MaSoSV", user.MaSoSV ?? string.Empty);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            if (user.Role!.TenRole == "QuanTri" || user.Role.TenRole == "BanChapHanh")
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }


            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra email đã tồn tại
            var existingUser = await _context.NguoiDungs
                .FirstOrDefaultAsync(u => u.Email.ToUpper() == model.Email.Trim().ToUpperInvariant());

            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng.");
                return View(model);
            }

            // Tạo người dùng mới
            var newUser = new Models.NguoiDung
            {
                MaSoSV = model.MaSoSV.Trim(),
                HoTen = model.HoTen.Trim(),
                Lop = model.Lop.Trim(),
                Email = model.Email.Trim(),
                MatKhau = BCrypt.Net.BCrypt.HashPassword(model.Password),
                SoDienThoai = string.IsNullOrWhiteSpace(model.SoDienThoai) ? null : model.SoDienThoai.Trim(),
                RoleId = 3 // SinhVien
            };

            _context.NguoiDungs.Add(newUser);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}
