using ElectricTransportBlog.Core.Models.TransportNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Contracts
{
    public interface ITransportNetworkService
    {
        public TransportNetworkPaginationModel All(int pageNo, int pageSize);

        public Guid Create(string town, string description, string createdBy, byte[] photoFile, bool isDeleted);

        public bool Edit(Guid id, string town, string description, byte[] photoFile);

        public bool Delete(Guid id, bool isDeleted);

        public Guid GetPublicationId(Guid transportNetworkId);

        public TransportNetworkEditFormModel EditViewData(Guid id);

        public TransportNetworkDeleteModel DeleteViewData(Guid id);

        public TransportNetworkContactLinesModel Details(Guid id);

        public TransportNetworkContactEditModel EditContactViewData(Guid id);

        public bool EditContact(Guid id, string email, string phone, string webPage);
    }
}
