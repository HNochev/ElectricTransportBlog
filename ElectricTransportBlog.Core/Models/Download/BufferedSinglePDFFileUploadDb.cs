using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ElectricTransportBlog.Core.Models.Download
{
    public class BufferedSinglePDFFileUploadDb
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Качете PDF файл")]
        public IFormFile FilePDF { get; set; }
    }
}
