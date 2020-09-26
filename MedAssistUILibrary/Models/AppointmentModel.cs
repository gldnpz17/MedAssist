using System;
using System.Collections.Generic;
using System.Text;

namespace UILibrary.Models
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Specialization { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
    }
}
