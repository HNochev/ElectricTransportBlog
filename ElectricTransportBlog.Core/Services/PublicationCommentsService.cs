using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Models.PublicationComments;
using ElectricTransportBlog.Infrastructure.Data;
using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Services
{
    public class PublicationCommentsService : IPublicationCommentsService
    {
        private readonly ApplicationDbContext data;

        public PublicationCommentsService(ApplicationDbContext data)
        {
            this.data = data;
        }
        public Guid CreatePublicationComment(string content, DateTime publishedOn, string userId, Guid publicationId)
        {
            var newComment = new PublicationComment
            {
                Content = content,
                PublishedOn = publishedOn,
                UserId = userId,
                PublicationId = publicationId
            };

            this.data.PublicationComments.Add(newComment);
            this.data.SaveChanges();

            return newComment.Id;
        }

        public bool Edit(Guid id, string content, DateTime lastEditedOn)
        {
            var commentData = this.data.PublicationComments.Find(id);

            if (commentData == null)
            {
                return false;
            }

            commentData.Content = content;
            commentData.LastEditedOn = lastEditedOn;

            this.data.SaveChanges();

            return true;
        }

        public bool IsByUser(Guid commentId, string userId)
            => this.data
                .PublicationComments
                .Any(c => c.Id == commentId && c.UserId == userId);

        public Guid IdOfPublication(Guid commentId)
            => this.data
                .PublicationComments
                .Where(x => x.Id == commentId)
                .Select(x => x.PublicationId)
                .FirstOrDefault();

        public bool Delete(Guid id)
        {
            var comment = this.data.PublicationComments.Find(id);

            if (comment == null)
            {
                return false;
            }

            this.data.Remove(comment);

            this.data.SaveChanges();

            return true;
        }

        public CommentsEditFormModel EditViewData(Guid id)
        {
            return this.data.PublicationComments
                    .Where(x => x.Id == id)
                    .Select(x => new CommentsEditFormModel
                    {
                        Content = x.Content
                    })
                    .First();
        }
    }
}
