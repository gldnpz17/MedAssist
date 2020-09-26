using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicV3.Models
{
    public class AppointmentRequestModel
    {
        public int Id { get; set; }
        public Guid AccountId { get; set; }
        public int DoctorId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsDone { get; set; }
    }
}
