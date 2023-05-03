using ElectricTransportBlog.Core.Models.PublicationComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Contracts
{
    public interface IPublicationCommentsService
    {
        Guid CreatePublicationComment(
            string content,
            DateTime date,
            string userId,
            Guid newsId);

        public bool Edit(
            Guid id,
            string content,
            DateTime lastEditedOn
            );

        public bool IsByUser(Guid commentId, string userId);

        public Guid IdOfPublication(Guid commentId);

        public bool Delete(Guid id);

        public CommentsEditFormModel EditViewData(Guid id);
    }
}
