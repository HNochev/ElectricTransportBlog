using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.TransportNetwork
{
    public class TransportNetworkAddFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(500, ErrorMessage = "{0} трябва да бъде по-късo от {1} символа")]
        [Display(Name = "Описание на линията")]
        public string Description { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [StringLength(60, ErrorMessage = "{0} трябва да бъде по-къс от {1} символа")]
        [Display(Name = "Име на град")]
        public string Town { get; set; }

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
    }
}
