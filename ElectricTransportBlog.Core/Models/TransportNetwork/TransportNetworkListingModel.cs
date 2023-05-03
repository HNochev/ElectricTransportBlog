using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.TransportNetwork
{
    public class TransportNetworkListingModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Town { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public bool IsDeleted { get; set; }

        public int LinesCount { get; set; }
    }
}
