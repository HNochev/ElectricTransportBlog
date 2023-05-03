using System.ComponentModel.DataAnnotations;

namespace ElectricTransportBlog.Core.Models.TransportNetwork
{
    public class TransportNetworkContactEditModel
    {
        [EmailAddress(ErrorMessage = "Нужен е валиден имейл!")]
        [Display(Name = "Имейл")]
        public string? Email { get; set; }

        [RegularExpression(@"[0-9]{10}$", ErrorMessage = "Нужен е валиден телефон!")]
        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Уеб Страница")]
        public string? WebPage { get; set; }
    }
}
