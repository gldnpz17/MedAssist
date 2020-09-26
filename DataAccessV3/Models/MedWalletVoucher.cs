using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessV3.Models
{
    public class MedWalletVoucher
    {
        public int Id { get; set; }
        [Required]
        public Guid Code { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public bool Claimed { get; set; }
    }
}
