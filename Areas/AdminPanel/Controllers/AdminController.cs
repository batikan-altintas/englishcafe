using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BatikanSon.Areas.AdminPanel.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("AdminPanel")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
