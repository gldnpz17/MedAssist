using EventAggregatorLibrary;
using MedAssistUILibrary.Models;
using NewDataAccessLibrary;
using NewDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistUILibrary.MedWallet
{
    public class MedWalletManager : ManagerBase
    {
        public MedWalletManager(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }

        public void ClaimVoucher(string voucherCode)
        {
            using (var context = new MedAssistContext())
            {
                var voucher = context.MedWalletVouchers.FirstOrDefault(v => v.Code.ToString() == voucherCode);
                //check validity
                if(voucher == null)
                {
                    throw new Exception("Voucher not found.");
                }
                else if(voucher.Claimed == true)
                {
                    throw new Exception("Voucher has been claimed previously.");
                }

                //perform transaction
                //mark voucher as claimed
                var endUser = context.EndUsers.FirstOrDefault(e => (e.UserId == _loggedInUser.Id));
                voucher.Claimed = true;
                //log transaction
                context.MedWalletTransactions.Add(new MedWalletTransaction()
                {
                    EndUserId = endUser.Id,
                    ChangeInAmount = voucher.Value,
                    Description = $"{voucher.Value} added using a voucher",
                });
                //add user balance
                endUser.MedWalletBalance += voucher.Value;
                context.SaveChanges();
            }
        }

        public ObservableCollection<MedWalletTransactionModel> GetMedWalletTransactions()
        {
            ObservableCollection<MedWalletTransactionModel> outputs = new ObservableCollection<MedWalletTransactionModel>();
            using (var context = new MedAssistContext())
            {
                var endUser = context.EndUsers.FirstOrDefault(e => e.UserId == _loggedInUser.Id);
                var transactions = from transaction in context.MedWalletTransactions
                                   where transaction.EndUserId == endUser.Id
                                   select transaction;

                foreach(var item in transactions)
                {
                    outputs.Add(new MedWalletTransactionModel()
                    {
                        Id = item.Id,
                        ChangeInAmount = item.ChangeInAmount,
                        Description = item.Description,
                        TransactionTime = item.TransactionTime
                    });
                }

                return outputs;
            }
        }
    }
}
