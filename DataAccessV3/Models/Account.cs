using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessV3.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)][Key]
        public Guid Id { get; set; }
        [MaxLength(64)] [Required]
        public string Username { get; set; }
        [MaxLength(64)] [Required]
        public string Password { get; set; }
        [Required]
        public bool IsHospital { get; set; }
        [Required]
        public bool IsPharmacy { get; set; }
        [Required]
        public decimal MedWalletBalance { get; set; }
    }
}
