using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Models.Line
{
    public class LineTypeModel
    {
        public Guid Id { get; set; }

        public string TypeOfVehicle { get; set; }

        public string ClassColor { get; set; }

        public int Counter { get; set; }
    }
}
