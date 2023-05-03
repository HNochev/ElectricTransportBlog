using ElectricTransportBlog.Infrastructure.Data.Models;

namespace ElectricTransportBlog.Core.Models.Download
{
    public class DownloadsListingModel
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string? Description { get; set; }

        public string UserId { get; set; }

        public WebsiteUser User { get; set; }

        public string DownloadUrlFromDatabase { get; set; }
    }
}
