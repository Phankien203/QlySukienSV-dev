using Microsoft.AspNetCore.Mvc;

namespace QlySukienSV.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Logout()
        {
            // Single logout source of truth: redirect to non-area AuthController.Logout
            return RedirectToAction("Logout", "Auth", new { area = string.Empty });
        }
    }
}

