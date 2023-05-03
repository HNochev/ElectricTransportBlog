using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricTransportBlog.Infrastructure.Data.Models
{
    public class PublicationComment
    {
        public PublicationComment()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        public DateTime? LastEditedOn { get; set; }

        [Required]
        public Guid PublicationId { get; set; }
        
        [ForeignKey(nameof(PublicationId))]
        public Publication Publication { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public WebsiteUser User { get; set; }
    }
}
