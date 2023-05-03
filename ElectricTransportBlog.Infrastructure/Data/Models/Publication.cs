using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricTransportBlog.Infrastructure.Data.Models
{
    public class Publication
    {
        public Publication()
        {
            this.Id = Guid.NewGuid();
            this.PublicationComments = new HashSet<PublicationComment>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public byte[] PhotoFile { get; set; }

        [Required]
        public string AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public WebsiteUser Author { get; set; }

        public ICollection<PublicationComment> PublicationComments { get; set; }
    }
}
