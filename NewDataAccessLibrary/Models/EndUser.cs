using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary.Models
{
    public class EndUser
    {
        public int Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public decimal MedWalletBalance { get; set; }

        public List<AmbulanceRequest> AmbulanceRequests { get; set; }
        public List<MedWalletTransaction> MedWalletTransactions { get; set; }
    }
}
