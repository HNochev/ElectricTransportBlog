using ElectricTransportBlog.Core.Contracts;
using ElectricTransportBlog.Core.Models.Line;
using ElectricTransportBlog.Infrastructure.Data;
using ElectricTransportBlog.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ElectricTransportBlog.Core.Services
{
    public class LineService : ILineService
    {
        private readonly ApplicationDbContext data;

        public LineService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<LineTypeModel> AllLineTypes()
        {
            return this.data
                .LineTypes
                .Select(x => new LineTypeModel
                {
                    Id = x.Id,
                    TypeOfVehicle = x.TypeOfVehicle,
                    ClassColor = x.ClassColor,
                    Counter = x.Counter,
                })
                .OrderBy(x => x.Counter)
                .ToList();
        }

        public Guid CreateLine(string number, string shortLineDescription, string lineDescription, string workDayInterval, string weekendInterval, Guid lineTypeId, Guid transportNetworkId)
        {
            var newLine = new Line
            {
                Number = number,
                ShortLineDescription = shortLineDescription,
                LineDescription = lineDescription,
                WorkDayInterval = workDayInterval,
                WeekendInterval = weekendInterval,
                LineTypeId = lineTypeId,
                TransportNetworkId = transportNetworkId,
            };

            this.data.Lines.Add(newLine);
            this.data.SaveChanges();

            return newLine.Id;
        }

        public LineDetailsModel Details(Guid id)
        {
            return this.data.Lines
                .Include(x => x.TransportNetwork)
                .Include(x => x.LineType)
                .Where(x => x.Id == id)
                .Select(x => new LineDetailsModel
                {
                    Id = x.Id,
                    Number = x.Number,
                    ShortLineDescription = x.ShortLineDescription,
                    LineDescription = x.LineDescription,
                    TransportNetworkId = x.TransportNetworkId,
                    WeekendInterval = x.WeekendInterval,
                    WorkDayInterval = x.WorkDayInterval,
                    TransportNetworkTown = x.TransportNetwork.Town,
                    LineType = new LineTypeModel
                    {
                        Id = x.LineTypeId,
                        ClassColor = x.LineType.ClassColor,
                        TypeOfVehicle = x.LineType.TypeOfVehicle,
                        Counter = x.LineType.Counter,
                    },
                })
                .First();
        }

        public bool Edit(Guid id, string number, string shortLineDescription, string lineDescription, string workDayInterval, string weekendInterval, Guid lineTypeId)
        {
            var lineData = this.data.Lines.Find(id);

            if (lineData == null)
            {
                return false;
            }

            lineData.Number = number;
            lineData.ShortLineDescription = shortLineDescription;
            lineData.LineDescription = lineDescription;
            lineData.WorkDayInterval = workDayInterval;
            lineData.WeekendInterval = weekendInterval;
            lineData.LineTypeId = lineTypeId;
            lineData.LineType = this.data.LineTypes.Find(lineTypeId);

            this.data.SaveChanges();

            return true;
        }

        public bool Delete(Guid id)
        {
            var line = this.data.Lines.Find(id);

            if (line == null)
            {
                return false;
            }

            this.data.Remove(line);

            this.data.SaveChanges();

            return true;
        }

        public LineEditFormModel EditViewData(Guid id)
        {
            return this.data.Lines
                .Where(x => x.Id == id)
                .Select(x => new LineEditFormModel
                {
                    Number = x.Number,
                    ShortLineDescription = x.ShortLineDescription,
                    LineDescription = x.LineDescription,
                    WeekendInterval = x.WeekendInterval,
                    WorkDayInterval = x.WorkDayInterval,
                    LineTypeId = x.LineTypeId,
                })
                .First();
        }

        public LineDeleteModel DeleteViewData(Guid id)
        {
            return this.data.Lines
                .Include(x => x.TransportNetwork)
                .Where(x => x.Id == id)
                .Select(x => new LineDeleteModel
                {
                    Number = x.Number,
                    ShortDescription = x.ShortLineDescription,
                    TownName = x.TransportNetwork.Town,
                })
                .First();
        }
    }
}
