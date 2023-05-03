using Microsoft.EntityFrameworkCore;
using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Models.Publication;
using ElectricTransportBlog.Core.Models.PublicationComments;
using ElectricTransportBlog.Infrastructure.Data;
using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ElectricTransportBlog.Core.Services
{
    public class PublicationService : IPublicationService
    {
        private readonly ApplicationDbContext data;

        public PublicationService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public PublicationPaginationModel All(int pageNo, int pageSize)
        {
            PublicationPaginationModel result = new PublicationPaginationModel()
            {
                PageNo = pageNo,
                PageSize = pageSize
            };

            result.TotalRecords = this.data.Publications.Count();
            result.Publications = this.data
                 .Publications
                 .Where(x => !x.IsDeleted)
                 .Select(x => new PublicationListingModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Description = Truncate(Regex.Replace(x.Description, "<.*?>|&[A-z0-9]*?\\;", String.Empty), 200),
                     ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                     Date = x.Date,
                     IsDeleted = x.IsDeleted,
                     AuthorId = x.AuthorId,
                     Author = x.Author,
                     CommentsCount = x.PublicationComments.Count(),
                 })
                 .OrderByDescending(x => x.Date)
                 .Skip(pageNo * pageSize - pageSize)
                 .Take(pageSize)
                 .ToList();

            return result;
        }

        public Guid CreatePublication(string title, string description, DateTime date, string authorId, byte[] photoFile, bool isDeleted)
        {
            var newPub = new Publication
            {
                Title = title,
                Description = description,
                Date = date,
                AuthorId = authorId,
                PhotoFile = photoFile,
                IsDeleted = isDeleted,
            };

            this.data.Publications.Add(newPub);
            this.data.SaveChanges();

            return newPub.Id;
        }

        public PublicationCommentsModel Details(Guid id)
        {
            return this.data.Publications
                .Include(x => x.Author)
                .Where(x => x.Id == id)
                .Select(x => new PublicationCommentsModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Date = x.Date,
                    AuthorId = x.AuthorId,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                    IsDeleted = x.IsDeleted,
                    Author = x.Author,
                    PublicationComments = x.PublicationComments
                    .Select(x => new CommentsListingModel
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Date = x.PublishedOn,
                        LastEditedOn = x.LastEditedOn,
                        PublicationId = x.PublicationId,
                        User = x.User,
                        UserId = x.UserId,
                    })
                    .OrderByDescending(x => x.Date)
                    .ToList(),
                })
                .First();
        }

        public async Task<Publication> GetPublicationByIdAsync(Guid id) => await this.data.Publications
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        private static string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length) + "...";
            }
            return source;
        }

        public bool Edit(Guid id, string title, string description, byte[] photoFile)
        {
            var publicationData = this.data.Publications.Find(id);

            if (publicationData == null)
            {
                return false;
            }

            publicationData.Title = title;
            publicationData.Description = description;
            if (photoFile != null)
            {
                publicationData.PhotoFile = photoFile;
            }

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id, bool isDeleted)
        {
            var publicationData = this.data.Publications.Find(id);

            if (publicationData == null)
            {
                return false;
            }

            publicationData.IsDeleted = isDeleted;

            this.data.SaveChanges();

            return true;
        }

        public Guid GetPublicationId(Guid publicationId)
        {
            return this.data.Publications
                .Where(x => x.Id == publicationId)
                .Select(x => x.Id)
                .FirstOrDefault();
        }

        public PublicationEditFormModel EditViewData(Guid id)
        {
            return this.data.Publications
                .Where(x => x.Id == id)
                .Select(x => new PublicationEditFormModel
                {
                    Title = x.Title,
                    Description = x.Description,
                })
                .First();
        }

        public PublicationDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Publications
                .Where(x => x.Id == id)
                .Select(x => new PublicationDeleteModel
                {
                    Title = x.Title,
                    Date = x.Date,
                    ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                })
                .First();
        }

        public List<PublicationListingModel> GetTopThreePublications()
        {
            return this.data
                 .Publications
                 .Where(x => !x.IsDeleted)
                 .Select(x => new PublicationListingModel
                 {
                     Id = x.Id,
                     Title = x.Title,
                     Description = Truncate(Regex.Replace(x.Description, "<.*?>|&[A-z0-9]*?\\;", String.Empty), 150),
                     ImgUrlFormDatabase = "data:image/jpg;base64," + Convert.ToBase64String(x.PhotoFile),
                     Date = x.Date,
                     IsDeleted = x.IsDeleted,
                     AuthorId = x.AuthorId,
                     Author = x.Author,
                     CommentsCount = x.PublicationComments.Count(),
                 })
                 .OrderByDescending(x => x.Date)
                 .Take(3)
                 .ToList();
        }
    }
}
