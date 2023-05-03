using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.Line
{
    public class LineEditFormModel
    {
        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Номер на маршрута")]
        public string Number { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Кратко описание на маршрута")]
        public string ShortLineDescription { get; set; }

        [Display(Name = "Подробно описание")]
        public string? LineDescription { get; set; }

        [Display(Name = "Интервал на преминаване през работните дни")]
        public string? WorkDayInterval { get; set; }

        [Display(Name = "Интервал на преминаване през празнични дни")]
        public string? WeekendInterval { get; set; }

        [Required(ErrorMessage = "{0} е задължително поле")]
        [Display(Name = "Тип превозно средство")]
        public Guid LineTypeId { get; set; }

        public IEnumerable<LineTypeModel> LineTypes { get; set; }

        [Required]
        public Guid TransportNetworkId { get; set; }
    }
}
