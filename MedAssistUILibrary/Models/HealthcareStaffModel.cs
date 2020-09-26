using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistUILibrary.Models
{
    public class HealthcareStaffModel
    {
        public int Id { get; set; }
        public int HealthcareUnitId { get; set; }
        public Guid UserId { get; set; }
    }
}
