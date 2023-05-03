using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.TransportNetwork
{
    public class LineListingModel
    {
        public Guid Id { get; set; }

        public string Number { get; set; }

        public string ShortLineDescription { get; set; }

        public Guid LineTypeId { get; set; }

        public LineType LineType { get; set; }

        public int NumberInted { get; set; }
    }
}
