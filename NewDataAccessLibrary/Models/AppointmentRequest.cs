using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class AppointmentRequest
    {
        public int Id { get; set; }
        [Required]
        public int EndUserId { get; set; }
        [Required]
        public int DoctorId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RequestTime { get; set; }
    }
}
