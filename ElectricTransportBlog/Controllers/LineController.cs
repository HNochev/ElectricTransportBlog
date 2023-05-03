using ElectricTransportBlog.Core.Constants;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Extensions;
using ElectricTransportBlog.Core.Models.Line;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectricTransportBlog.Controllers
{
    public class LineController : Controller
    {
        private readonly ILineService lines;
        private readonly IUserService users;

        public LineController(ILineService lines, IUserService users)
        {
            this.lines = lines;
            this.users = users;
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Add()
        {
            return View(new LineAddFormModel
            {
                LineTypes = this.lines.AllLineTypes()
            });
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public async Task<IActionResult> AddAsync(Guid id, LineAddFormModel line)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var lineId = this.lines.CreateLine(
                line.Number,
                line.ShortLineDescription,
                line.LineDescription,
                line.WorkDayInterval,
                line.WeekendInterval,
                line.LineTypeId,
                id
                );

            TempData[MessageConstants.SuccessMessage] = "Успешно добавихте линията.";
            return Redirect($"../../TransportNetwork/Details/{id}");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var line = this.lines.Details(id);

            if (line == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("All");
            }

            return View(line);
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var lineForm = this.lines.EditViewData(id);

            lineForm.LineTypes = this.lines.AllLineTypes();

            return View(lineForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Edit(Guid id, LineEditFormModel line)
        {
            var edited = this.lines.Edit(
                id,
                line.Number,
                line.ShortLineDescription,
                line.LineDescription,
                line.WorkDayInterval,
                line.WeekendInterval,
                line.LineTypeId
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Информацията за линията беше успешно редактирана.";
            return Redirect($"../../TransportNetwork/All");
        }

        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var lineForm = this.lines.DeleteViewData(id);

            return View(lineForm);
        }

        [HttpPost]
        [Authorize(Roles = UserConstants.Administrator)]
        public IActionResult Delete(Guid id, LineDeleteModel line)
        {

            var deleted = this.lines.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Линията беше успешно изтрита.";
            return Redirect($"../../TransportNetwork/All");
        }
    }
}
