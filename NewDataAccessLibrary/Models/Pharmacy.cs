using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class Pharmacy
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public List<MedicationStock> MedicationStocks { get; set; }
        public List<Pharmacist> Pharmacists { get; set; }
    }
}
