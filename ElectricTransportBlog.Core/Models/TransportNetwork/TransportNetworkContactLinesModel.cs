using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.TransportNetwork
{
    public class TransportNetworkContactLinesModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Town { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public bool IsDeleted { get; set; }

        public string PublishedById { get; set; }

        public WebsiteUser PublishedBy { get; set; }

        public Guid ContactId { get; set; }

        public ContactListingModel? Contact { get; set; }

        public ICollection<LineListingModel> Lines { get; set; }
    }
}
