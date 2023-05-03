using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.Publication
{
    public class PublicationPaginationModel
    {
        public int PageNo { get; set; }

        public int TotalRecords { get; set; }

        public int PageSize { get; set; }

        public List<PublicationListingModel> Publications { get; set; }
    }
}
