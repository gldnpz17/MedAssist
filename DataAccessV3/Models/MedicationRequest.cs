using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessV3.Models
{
    public class MedicationRequest
    {
        public int Id { get; set; }
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public int MedicationId { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public bool IsDone { get; set; }
    }
}
