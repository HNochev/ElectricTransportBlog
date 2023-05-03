using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ElectricTransportBlog.Core.Constants;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Extensions;
using ElectricTransportBlog.Core.Models.Admin;

namespace ElectricTransportBlog.Controllers
{
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public async Task<IActionResult> ManageUsers()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole()
        {
            await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "121212"
            });

            return Ok();
        }
    }
}
