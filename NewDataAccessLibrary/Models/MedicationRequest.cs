using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class MedicationRequest
    {
        public int Id { get; set; }
        [Required]
        public int PharmacyId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public int MedicationId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime RequestTime { get; set; }
    }
}
