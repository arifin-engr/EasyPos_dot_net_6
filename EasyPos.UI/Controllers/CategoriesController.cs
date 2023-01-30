using Microsoft.AspNetCore.Mvc;

namespace EasyPos.UI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
