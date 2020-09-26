using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class HealthcareUnit
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Name { get; set; }

        public List<Doctor> Doctors { get; set; }
        public List<HealthcareStaff> HealthcareStaffs { get; set; }
    }
}
