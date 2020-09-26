using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int HealthcareUnitId { get; set; }

        [MaxLength(128)]
        public string FullName { get; set; }
        public byte[] RawProfileImage { get; set; }
        [MaxLength(256)]
        public string ShortBio { get; set; }
        [MaxLength(64)]
        public string Specialization { get; set; }

        /// <summary>
        /// 0=Available,1=Busy,2=OffHours
        /// </summary>
        [Required]
        public int Status { get; set; }
        public List<DoctorSchedule> Schedules { get; set; }
    }
}
