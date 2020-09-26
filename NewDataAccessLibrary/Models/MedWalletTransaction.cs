using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class MedWalletTransaction
    {
        public int Id { get; set; }
        [Required]
        public int EndUserId { get; set; }
        [Required]
        public decimal ChangeInAmount { get; set; }
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime TransactionTime { get; set; }
    }
}
