using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ElectricTransportBlog.Core.Models.Publication
{
    public class PublicationEditFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(50, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Заглавие")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Съдържание")]
        public string Description { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDbNotRequired? FileUpload { get; set; }
    }
}
