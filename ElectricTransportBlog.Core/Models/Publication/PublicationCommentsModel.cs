using ElectricTransportBlog.Core.Models.PublicationComments;
using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.Publication
{
    public class PublicationCommentsModel : CommentAddFormModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public string AuthorId { get; set; }

        public WebsiteUser Author { get; set; }

        public ICollection<CommentsListingModel> PublicationComments { get; set; }
    }
}
