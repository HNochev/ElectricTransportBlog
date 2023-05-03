using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElectricTransportBlog.Core.Constants;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Extensions;

namespace ElectricTransportBlog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService users;

        public UserController(IUserService users)
        {
            this.users = users;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            return View();
        }

        public IActionResult UserProfile(string id)
        {

            var detailsForm = this.users.UserDetails(id);

            return View(detailsForm);
        }
    }
}
