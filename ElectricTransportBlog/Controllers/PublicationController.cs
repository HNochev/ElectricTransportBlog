using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ElectricTransportBlog.Core.Constants;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Extensions;
using ElectricTransportBlog.Core.Models.Publication;
using ElectricTransportBlog.Core.Models.PublicationComments;

namespace ElectricTransportBlog.Controllers
{
    public class PublicationController : Controller
    {
        private readonly IPublicationService publication;
        private readonly IUserService users;
        private readonly IPublicationCommentsService comments;

        public PublicationController(IPublicationService news, IUserService users, IPublicationCommentsService comments)
        {
            this.publication = news;
            this.users = users;
            this.comments = comments;
        }

        public IActionResult All(int p = 1, int s = 10)
        {
            var latestPublication = this.publication.All(p, s);

            return View(latestPublication);
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var publication = this.publication.Details(id);

            if (publication == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("All");
            }

            return View(publication);
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public async Task<IActionResult> AddAsync(PublicationAddFormModel publication)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            byte[] fileArray = null;

            using (var memoryStream = new MemoryStream())
            {
                await publication.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                string fileExt = Path.GetExtension(publication.FileUpload.PhotoFile.FileName.ToLower());

                if (fileExt == ".jpeg" || fileExt == ".jpg")
                {
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        fileArray = memoryStream.ToArray();
                    }
                    else
                    {
                        TempData[MessageConstants.ErrorMessage] = "Размерът на снимката е твърде голям! Моля качете снимка до 2MB!";
                        return Redirect(Request.Path);
                    }
                }
                else
                {
                    TempData[MessageConstants.ErrorMessage] = "Само .jpeg/.jpg файлове са разрешени!";
                    return Redirect(Request.Path);
                }
            }

            var publicationId = this.publication.CreatePublication(
                publication.Title,
                publication.Description,
                DateTime.Now,
                userId,
                fileArray,
                false);

            TempData[MessageConstants.SuccessMessage] = "Новината беше успешно добавена.";
            return RedirectToAction("All");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var news = this.publication.Details(id);

            var newsForm = this.publication.EditViewData(news.Id);

            return View(newsForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public async Task<IActionResult> EditAsync(Guid id, PublicationEditFormModel publication)
        {
            byte[] fileArray = null;

            if (publication.FileUpload != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await publication.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                    string fileExt = Path.GetExtension(publication.FileUpload.PhotoFile.FileName.ToLower());

                    if (fileExt == ".jpeg" || fileExt == ".jpg")
                    {
                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            fileArray = memoryStream.ToArray();
                        }
                        else
                        {
                            TempData[MessageConstants.ErrorMessage] = "Размерът на снимката е твърде голям! Моля качете снимка до 2MB!";
                            return Redirect(Request.Path);
                        }
                    }
                    else
                    {
                        TempData[MessageConstants.ErrorMessage] = "Само .jpeg/.jpg файлове са разрешени!";
                        return Redirect(Request.Path);
                    }
                }
            }

            var edited = this.publication.Edit(
                id,
                publication.Title,
                publication.Description,
                fileArray);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Новината беше успешно редактирана.";
            return Redirect($"../../Publication/Details/{id}");
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var news = this.publication.Details(id);

            var newsForm = this.publication.DeleteViewData(news.Id);

            return View(newsForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Delete(Guid id, PublicationDeleteModel news)
        {

            var deleted = this.publication.Delete(id, true);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Новината беше успешно изтрита.";
            return RedirectToAction("All");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(Guid id, CommentAddFormModel comment)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var edited = this.comments.CreatePublicationComment(
                comment.Content,
                DateTime.Now,
                userId,
                id);

            TempData[MessageConstants.SuccessMessage] = "Успешно публикувахте коментар.";
            return Redirect(Request.Path);
        }
    }
}
