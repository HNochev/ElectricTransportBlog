using Microsoft.AspNetCore.Identity;

namespace ElectricTransportBlog.Infrastructure.Data.Models
{
    public class WebsiteUser : IdentityUser
    {
        public WebsiteUser()
        {
            this.Publications = new HashSet<Publication>();
            this.PublicationComments = new HashSet<PublicationComment>();
        }
        public DateTime RegisteredOn { get; set; }

        public ICollection<Publication> Publications { get; set; }

        public ICollection<PublicationComment> PublicationComments { get; set; }
    }
}
