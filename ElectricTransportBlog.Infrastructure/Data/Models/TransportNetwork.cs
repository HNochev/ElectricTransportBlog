using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricTransportBlog.Infrastructure.Data.Models
{
    public class TransportNetwork
    {
        public TransportNetwork()
        {
            this.Id = Guid.NewGuid();
            this.Contact = new Contact();
            this.Lines = new HashSet<Line>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public byte[] PhotoFile { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public string PublishedById { get; set; }

        [ForeignKey(nameof(PublishedById))]
        public WebsiteUser PublishedBy { get; set; }

        public Guid? ContactId { get; set; }

        [ForeignKey(nameof(ContactId))]
        public Contact? Contact { get; set; }

        public ICollection<Line> Lines { get; set; }
    }
}
