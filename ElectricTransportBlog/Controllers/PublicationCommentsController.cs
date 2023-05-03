using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ElectricTransportBlog.Core.Constants;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Extensions;
using ElectricTransportBlog.Core.Models.PublicationComments;
using ElectricTransportBlog.Infrastructure.Data;

namespace ElectricTransportBlog.Controllers
{
    public class PublicationCommentsController : Controller
    {
        private readonly IPublicationCommentsService comments;
        private readonly IPublicationService news;
        private readonly IUserService users;

        public PublicationCommentsController(IPublicationCommentsService comments, IPublicationService news, IUserService users)
        {
            this.comments = comments;
            this.news = news;
            this.users = users;
        }

        [Authorize]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            if (!this.comments.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.Moderator))
            {
                return BadRequest();
            }

            var commentForm = this.comments.EditViewData(id);

            return View(commentForm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(Guid id, CommentsEditFormModel comment)
        {
            var userId = this.users.IdByUser(this.User.Id());
            var newsId = this.comments.IdOfPublication(id);

            if (!this.comments.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.Moderator))
            {
                return BadRequest();
            }

            var edited = this.comments.Edit(
                id,
                comment.Content.Trim(),
                lastEditedOn: DateTime.Now
                );

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Коментарът беше успешно редактиран.";

            return Redirect($"../../Publication/Details/{newsId}");
        }

        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());
            var newsId = this.comments.IdOfPublication(id);

            if (!this.comments.IsByUser(id, userId) && !User.IsInRole(UserConstants.Administrator) && !User.IsInRole(UserConstants.Moderator))
            {
                return BadRequest();
            }

            var deleted = this.comments.Delete(id);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Коментарът беше успешно изтрит.";
            return Redirect($"../../Publication/Details/{newsId}");
        }
    }
}
