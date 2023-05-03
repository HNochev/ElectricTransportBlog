using ElectricTransportBlog.Core.Models.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.Home
{
    public class HomePageModel
    {
        public IEnumerable<PublicationListingModel> Publications { get; set; }
    }
}
