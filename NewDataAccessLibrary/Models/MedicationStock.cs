using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class MedicationStock
    {
        public int Id { get; set; }
        [Required]
        public int PharmacyId { get; set; }
        [Required]
        public Medication Medication { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
