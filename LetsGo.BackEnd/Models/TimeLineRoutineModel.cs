using LetsGo.DataLayer.TableEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class TimeLineRoutineResponse
    {
        public DateTime Date { get; set; }
        public string Day { get; set; }

        public IEnumerable<TimeLineRoutineDayResponse> RoutineDays;
    }

    public class TimeLineRoutineDayResponse
    {

        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public string RoutineName { get; set; }
        public string RoutineAltName { get; set; }
        public Guid RoutineId { get; set; }
        public Routine Routine { get; set; }
    }
}
