using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.Line
{
    public class LineDetailsModel
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public string ShortLineDescription { get; set; }

        public string? LineDescription { get; set; }

        public string? WorkDayInterval { get; set; }

        public string? WeekendInterval { get; set; }

        public Guid LineTypeId { get; set; }

        public LineTypeModel LineType { get; set; }

        public Guid TransportNetworkId { get; set; }

        public string TransportNetworkTown { get; set; }
    }
}
