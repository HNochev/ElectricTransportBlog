using ElectricTransportBlog.Core.Models.Publication;
using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Contracts
{
    public interface IPublicationService
    {
        Guid CreatePublication(
            string title,
            string description,
            DateTime date,
            string authorId,
            byte[] photoFile,
            bool isDeleted);

        Task<Publication> GetPublicationByIdAsync(Guid id);

        public PublicationCommentsModel Details(Guid id);

        public PublicationPaginationModel All(int pageNo, int pageSize);

        public bool Edit(
           Guid id,
           string title,
           string description,
           byte[] photoFile);

        public bool Delete(Guid id, bool isDeleted);

        public Guid GetPublicationId(Guid publicationId);

        public PublicationEditFormModel EditViewData(Guid id);

        public PublicationDeleteModel DeleteViewData(Guid id);

        public List<PublicationListingModel> GetTopThreePublications();
    }
}
