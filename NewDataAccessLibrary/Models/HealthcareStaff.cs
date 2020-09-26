using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class HealthcareStaff
    {
        public int Id { get; set; }
        [Required]
        public int HealthcareUnitId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
