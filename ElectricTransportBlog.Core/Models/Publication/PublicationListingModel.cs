using ElectricTransportBlog.Infrastructure.Data.Models;

namespace ElectricTransportBlog.Core.Models.Publication
{
    public class PublicationListingModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

        public string ImgUrlFormDatabase { get; set; }

        public string AuthorId { get; set; }

        public WebsiteUser Author { get; set; }

        public int CommentsCount { get; set; }
    }
}
