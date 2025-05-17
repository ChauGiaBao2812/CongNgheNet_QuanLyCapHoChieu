using Microsoft.AspNetCore.Mvc;

namespace QuanLiHoChieu.Controllers
{
    public class ChungController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
