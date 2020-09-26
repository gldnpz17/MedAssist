using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistUILibrary.Models
{
    public class DoctorScheduleModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DayOfTheWeek { get; set; }
        public int StartTimeInMinutesPastMidnight { get; set; }
        public int EndTimeInMinutesPastMidnight { get; set; }
    }
}
