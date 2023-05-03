using System.ComponentModel.DataAnnotations;

namespace ElectricTransportBlog.Core.Models.Download
{
    public class DownloadEditFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(50, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Заглавие на качваният файл")]
        public string FileName { get; set; }

        [StringLength(100, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Описание на файлът")]
        public string? Description { get; set; }
    }
}
