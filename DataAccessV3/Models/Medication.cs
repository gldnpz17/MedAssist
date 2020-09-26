using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessV3.Models
{
    public class Medication
    {
        public int Id { get; set; }
        [MaxLength(64)][Required]
        public string Name { get; set; }
        [MaxLength(128)][Required]
        public string Description { get; set; }
        [Required]
        public byte[] RawImage { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
