using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricTransportBlog.Infrastructure.Data.Models
{
    public class Contact
    {
        public Contact()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? WebPage { get; set; }

        [Required]
        public Guid TransportNetworkId { get; set; }

        [ForeignKey(nameof(TransportNetworkId))]
        public TransportNetwork TransportNetwork { get; set; }
    }
}
