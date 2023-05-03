using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectricTransportBlog.Infrastructure.Data.Models
{
    public class Line
    {
        public Line()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string ShortLineDescription { get; set; }

        public string? LineDescription { get; set; }

        public string? WorkDayInterval { get; set; }

        public string? WeekendInterval { get; set; }

        [Required]
        public Guid LineTypeId { get; set; }

        [ForeignKey(nameof(LineTypeId))]
        public LineType LineType { get; set; }

        [Required]
        public Guid TransportNetworkId { get; set; }

        [ForeignKey(nameof(TransportNetworkId))]
        public TransportNetwork TransportNetwork { get; set; }
    }
}
