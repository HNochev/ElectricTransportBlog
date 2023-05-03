using System.ComponentModel.DataAnnotations;

namespace ElectricTransportBlog.Infrastructure.Data.Models
{
    public class LineType
    {
        public LineType()
        {
            this.Id = Guid.NewGuid();
            this.Lines = new HashSet<Line>();
        }

        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string TypeOfVehicle { get; set; }

        [Required]
        public string ClassColor { get; set; }

        [Required]
        public int Counter { get; set; }

        public ICollection<Line> Lines { get; set; }
    }
}
