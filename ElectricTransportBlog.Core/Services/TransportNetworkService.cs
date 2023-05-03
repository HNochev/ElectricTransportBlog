using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Models.TransportNetwork;
using ElectricTransportBlog.Infrastructure.Data;
using ElectricTransportBlog.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricTransportBlog.Core.Services
{
    public class TransportNetworkService : ITransportNetworkService
    {
        private readonly ApplicationDbContext data;

        public TransportNetworkService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public TransportNetworkPaginationModel All(int pageNo, int pageSize)
        {
            TransportNetworkPaginationModel result = new TransportNetworkPaginationModel()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = this.data.TransportNetworks.Count();
            result.TransportNetworks = this.data
                 .TransportNetworks
                 .Where(x => !x.IsDeleted)
                 .Select(x => new TransportNetworkListingModel
                 {
                     Id = x.Id,
                     Town = x.Town,
                     Description = Truncate(x.Description, 50),
                     ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                     IsDeleted = x.IsDeleted,
                     LinesCount = x.Lines.Count(),
                 })
                 .OrderBy(x => x.Town)
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
                 .ToList();

            return result;
        }

        public Guid Create(string town, string description, string createdBy, byte[] photoFile, bool isDeleted)
        {
            var newNetwork = new TransportNetwork
            {
                Town = town,
                Description = description,
                PublishedById = createdBy,
                PhotoFile = photoFile,
                IsDeleted = isDeleted,
            };

            this.data.TransportNetworks.Add(newNetwork);
            this.data.SaveChanges();

            return newNetwork.Id;
        }

        public bool Edit(Guid id, string town, string description, byte[] photoFile)
        {
            var transportNetworkData = this.data.TransportNetworks.Find(id);

            if (transportNetworkData == null)
            {
                return false;
            }

            transportNetworkData.Town = town;
            transportNetworkData.Description = description;
            if (photoFile != null)
            {
                transportNetworkData.PhotoFile = photoFile;
            }

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var transportNetworkData = this.data.TransportNetworks.Find(id);

            if (transportNetworkData == null)
            {
                return false;
            }

            transportNetworkData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public Guid GetPublicationId(Guid transportNetworkId)
        {
            return this.data.TransportNetworks
                .Where(x => x.Id == transportNetworkId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public TransportNetworkEditFormModel EditViewData(Guid id)
        {
            return this.data.TransportNetworks
                .Where(x => x.Id == id)
                .Select(x => new TransportNetworkEditFormModel
                {
                    Town = x.Town,
                    Description = x.Description,
                })
                .First();
        }

        public TransportNetworkDeleteModel DeleteViewData(Guid id)
        {
            return this.data.TransportNetworks
                .Where(x => x.Id == id)
                .Select(x => new TransportNetworkDeleteModel
                {
                    Town = x.Town,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                })
                .First();
        }

        private static string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length) + "...";
            }
            return source;
        }

        public TransportNetworkContactLinesModel Details(Guid id)
        {
            return this.data.TransportNetworks
                .Include(x => x.Contact)
                .Include(x => x.Lines)
                .Where(x => x.Id == id)
                .Select(x => new TransportNetworkContactLinesModel
                {
                    Id = x.Id,
                    Town = x.Town,
                    Description = x.Description,
                    PublishedById = x.PublishedById,
                    PublishedBy = x.PublishedBy,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                    IsDeleted = x.IsDeleted,
                    Contact = new ContactListingModel
                    {
                        Email = x.Contact.Email,
                        PhoneNumber = x.Contact.PhoneNumber,
                        WebPage = x.Contact.WebPage,
                    },
                    Lines = x.Lines
                    .Select(x => new LineListingModel
                    {
                        Id = x.Id,
                        Number = x.Number,
                        ShortLineDescription = x.ShortLineDescription,
                        LineTypeId = x.LineTypeId,
                        LineType = x.LineType,
                    })
                    .OrderBy(x => Convert.ToInt32(x.Number))
                    .ToList(),
                })
                .First();
        }

        public TransportNetworkContactEditModel EditContactViewData(Guid id)
        {
            return this.data.TransportNetworks
            .Where(x => x.Id == id)
            .Select(x => new TransportNetworkContactEditModel
            {
                Email = x.Contact.Email ?? "",
                PhoneNumber = x.Contact.PhoneNumber ?? "",
                WebPage = x.Contact.WebPage ?? "",
            })
            .First();
        }

        public bool EditContact(Guid id, string email, string phone, string webPage)
        {
            var transportNetworkData = this.data.TransportNetworks.Find(id);

            if (transportNetworkData == null)
            {
                return false;
            }
            if (!this.data.Contacts.Any(x => x.TransportNetworkId == id))
            {
                var newContact = new Contact
                {
                    Email = email,
                    PhoneNumber = phone,
                    WebPage = webPage,
                    TransportNetworkId = id,
                    TransportNetwork = transportNetworkData,
                };


                this.data.Contacts.Add(newContact);
                this.data.SaveChanges();

                return true;
            }
            else
            {
                var contactData = this.data.Contacts.Where(x => x.TransportNetworkId == id).First();

                contactData.Email = email;
                contactData.PhoneNumber = phone;
                contactData.WebPage = webPage;

                this.data.SaveChanges();

                return true;
            }
        }
    }
}
