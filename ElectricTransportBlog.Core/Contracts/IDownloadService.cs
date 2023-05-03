using ElectricTransportBlog.Core.Models.Download;
using ElectricTransportBlog.Infrastructure.Data.Models;

namespace ElectricTransportBlog.Core.Contracts
{
    public interface IDownloadService
    {
        public Guid CreateFile(string fileName, string? description, byte[] FilePdf, string userId);

        public List<DownloadsListingModel> All();

        public Task<Download> GetFile(Guid Id);

        public bool Edit(Guid id, string fileName, string? description);

        public bool Delete(Guid id);

        public DownloadEditFormModel EditViewData(Guid id);

        public DownloadDeleteModel DeleteViewData(Guid id);
    }
}
