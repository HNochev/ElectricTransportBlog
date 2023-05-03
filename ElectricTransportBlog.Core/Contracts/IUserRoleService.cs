using ElectricTransportBlog.Core.Models.Users;
using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Contracts
{
    public interface IUserRoleService
    {
        public Task<IEnumerable<UserListingModel>> GetUsers();

        public Task<UserEditModel> GetUserForEdit(string id);

        public Task<bool> UpdateUser(UserEditModel model);

        public Task<WebsiteUser> GetUserById(string id);
    }
}
