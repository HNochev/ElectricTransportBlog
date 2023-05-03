using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElectricTransportBlog.Core.Constants;

namespace ElectricTransportBlog.Areas.RoleManage.Controllers
{
    [Authorize(Roles = UserConstants.Administrator)]
    [Area("RoleManage")]
    public class BaseController : Controller
    {

    }
}
