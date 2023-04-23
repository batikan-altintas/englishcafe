using BatikanSon.DAL;
using Microsoft.AspNetCore.Mvc;

namespace BatikanSon.Controllers
{
    public class LangController : Controller
    {
        LangDB _db;
        public LangController(LangDB db)
        {
            _db = db;
            _db.Database.EnsureCreated();
        }
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
