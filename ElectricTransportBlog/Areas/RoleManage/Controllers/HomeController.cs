using Microsoft.AspNetCore.Mvc;

namespace ElectricTransportBlog.Areas.RoleManage.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
