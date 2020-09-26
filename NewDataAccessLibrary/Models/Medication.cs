using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class Medication
    {
        public int Id { get; set; }
        [Required]
        public byte[] RawImage { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
