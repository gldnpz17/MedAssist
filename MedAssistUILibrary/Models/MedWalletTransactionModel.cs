using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistUILibrary.Models
{
    public class MedWalletTransactionModel
    {
        public int Id { get; set; }
        public decimal ChangeInAmount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionTime { get; set; }
    }
}
