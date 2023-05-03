using ElectricTransportBlog.Core.Models.Line;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricTransportBlog.Core.Contracts
{
    public interface ILineService
    {
        public IEnumerable<LineTypeModel> AllLineTypes();

        public Guid CreateLine(string number, string shortLineDescription, string lineDescription, string workDayInterval, string weekendInterval, Guid lineTypeId, Guid transportNetworkId);

        public LineDetailsModel Details(Guid id);

        public bool Edit(Guid id, string number, string shortLineDescription, string lineDescription, string workDayInterval, string weekendInterval, Guid lineTypeId);

        public bool Delete(Guid id);

        public LineEditFormModel EditViewData(Guid id);

        public LineDeleteModel DeleteViewData(Guid id);
    }
}
