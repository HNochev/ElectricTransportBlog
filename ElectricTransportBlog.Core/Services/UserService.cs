using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Models.Users;
using ElectricTransportBlog.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;

        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string IdByUser(string userId)
        {
            return this.data.WebsiteUsers
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .First();
        }

        public UserDetailsModel UserDetails(string id)
        {
            return this.data.WebsiteUsers
                .Where(x => x.Id == id)
                .Select(x => new UserDetailsModel
                {
                    Id = id,
                    Email = x.Email,
                    Username = x.UserName,
                    Role = this.data.Roles.First(x => x.Id == this.data.UserRoles.First(x => x.UserId == id).RoleId).Name,
                    Publications = x.Publications,
                    PublicationComments = x.PublicationComments,
                })
                .First();
        }

        //public int UserPendingPhotosCount(string id)
        //{
        //    return this.data.Photos
        //        .Where(x => x.PhotoStatus.ClassColor == "table-warning" && x.UserId == id)
        //        .Count();
        //}
    }
}
