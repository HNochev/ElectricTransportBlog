using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Models.Admin;
using ElectricTransportBlog.Infrastructure.Data;
using ElectricTransportBlog.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext data;

        public AdminService(ApplicationDbContext data)
        {
            this.data = data;
        }
    }
}
