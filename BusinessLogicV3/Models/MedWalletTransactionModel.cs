using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicV3.Models
{
    public class MedWalletTransactionModel
    {
        public int Id { get; set; }
        public Guid AccountId { get; set; }
        public decimal ChangeInBalance { get; set; }
        public string Description { get; set; }
    }
}
