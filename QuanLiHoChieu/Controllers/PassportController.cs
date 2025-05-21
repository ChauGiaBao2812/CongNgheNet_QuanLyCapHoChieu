using Microsoft.AspNetCore.Mvc;
using QuanLiHoChieu.Data;
using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Controllers
{
    public class PassportController : Controller
    {
        private PassportDbContext _context;
        private ILogger<PassportController> _logger;

        public PassportController(PassportDbContext context, ILogger<PassportController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(PassportFormVM model, IFormFile? avatar)
        {
            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;
                    foreach (var error in errors)
                    {
                        _logger.LogWarning("Validation error in field '{Field}': {ErrorMessage}", key, error.ErrorMessage);
                    }
                }

                return View(model);
            }

            // Proceed with async operations later
            return Ok(); // or RedirectToAction(...);
        }
    }
}
