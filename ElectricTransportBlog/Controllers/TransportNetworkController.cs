using ElectricTransportBlog.Core.Constants;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Extensions;
using ElectricTransportBlog.Core.Models.TransportNetwork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectricTransportBlog.Controllers
{
    public class TransportNetworkController : Controller
    {
        private readonly ITransportNetworkService transportNetwork;
        private readonly IUserService users;

        public TransportNetworkController(ITransportNetworkService transportNetwork, IUserService users)
        {
            this.transportNetwork = transportNetwork;
            this.users = users;
        }

        public IActionResult All(int p = 1, int s = 10)
        {
            var latestPublication = this.transportNetwork.All(p, s);

            return View(latestPublication);
        }

        [Authorize(Roles = $"{UserConstants.Administrator},{UserConstants.Moderator}")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator}")]
        public async Task<IActionResult> AddAsync(TransportNetworkAddFormModel transportNetwork)
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
                await transportNetwork.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                string fileExt = Path.GetExtension(transportNetwork.FileUpload.PhotoFile.FileName.ToLower());

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

            var transportNetworkId = this.transportNetwork.Create(
                transportNetwork.Town,
                transportNetwork.Description,
                userId,
                fileArray,
                false);

            TempData[MessageConstants.SuccessMessage] = "Мрежата беше успешно добавена.";
            return RedirectToAction("All");
        }

        [Authorize(Roles = $"{UserConstants.Administrator}")]
        public IActionResult Edit(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var transportNetwork = this.transportNetwork.Details(id);

            var transportNetworkForm = this.transportNetwork.EditViewData(transportNetwork.Id);

            return View(transportNetworkForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator}")]
        public async Task<IActionResult> EditAsync(Guid id, TransportNetworkEditFormModel transportNetwork)
        {
            byte[] fileArray = null;

            if (transportNetwork.FileUpload != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await transportNetwork.FileUpload.PhotoFile.CopyToAsync(memoryStream);

                    string fileExt = Path.GetExtension(transportNetwork.FileUpload.PhotoFile.FileName.ToLower());

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

            var edited = this.transportNetwork.Edit(
                id,
                transportNetwork.Town,
                transportNetwork.Description,
                fileArray);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Мрежата беше успешно редактирана.";
            return Redirect($"../../TransportNetwork/Details/{id}");
        }

        [Authorize(Roles = $"{UserConstants.Administrator}")]
        public IActionResult Delete(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var transportNetwork = this.transportNetwork.Details(id);

            var transportNetworkForm = this.transportNetwork.DeleteViewData(transportNetwork.Id);

            return View(transportNetworkForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator}")]
        public IActionResult Delete(Guid id, TransportNetworkDeleteModel transportNetwork)
        {

            var deleted = this.transportNetwork.Delete(id, true);

            if (!deleted)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Мрежата беше успешно изтрита.";
            return RedirectToAction("All");
        }

        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var transportNetwork = this.transportNetwork.Details(id);

            if (transportNetwork == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction("All");
            }

            return View(transportNetwork);
        }

        public IActionResult AddContact(Guid id)
        {
            var userId = this.users.IdByUser(this.User.Id());

            if (userId == null)
            {
                TempData[MessageConstants.ErrorMessage] = "Възникна грешка!";
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            var transportNetwork = this.transportNetwork.Details(id);

            var transportNetworkForm = this.transportNetwork.EditContactViewData(transportNetwork.Id);

            return View(transportNetworkForm);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserConstants.Administrator}")]
        public async Task<IActionResult> AddContactAsync(Guid id, TransportNetworkContactEditModel transportNetwork)
        {
            var edited = this.transportNetwork.EditContact(
                id,
                transportNetwork.Email,
                transportNetwork.PhoneNumber,
                transportNetwork.WebPage);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[MessageConstants.SuccessMessage] = "Контактът беше успешно редактиран.";
            return Redirect($"../../TransportNetwork/Details/{id}");
        }
    }
}
