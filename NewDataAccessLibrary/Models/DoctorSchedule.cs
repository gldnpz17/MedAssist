using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class DoctorSchedule
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public int DayOfTheWeek { get; set; }
        [Required]
        public int StartTimeInMinutesPastMidnight { get; set; }
        [Required]
        public int EndTimeInMinutesPastMidinight { get; set; }
    }
}
