using BusinessLogicV3.Models;
using DataAccessV3;
using DataAccessV3.Models;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistBusinessLogic.MedWallet
{
    public class MedWalletManager : ManagerBase
    {
        public MedWalletManager(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }

        public decimal GetMedWalletBalance()
        {
            using (var context = new MedAssistContext())
            {
                return context.Accounts.FirstOrDefault(a => a.Id == _loggedInUser.Id).MedWalletBalance;
            }
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
                
                //mark voucher as claimed
                voucher.Claimed = true;
                //log transaction
                context.MedWalletTransactions.Add(new MedWalletTransaction()
                {
                    AccountId = _loggedInUser.Id,
                    ChangeInBalance = voucher.Value,
                    Description = $"{voucher.Value} added using a voucher",
                });
                //add user balance
                decimal balance = context.Accounts.FirstOrDefault(a => a.Id == _loggedInUser.Id).MedWalletBalance;
                balance += voucher.Value;

                context.SaveChanges();
            }
        }

        public ObservableCollection<MedWalletTransactionModel> GetMedWalletTransactions()
        {
            ObservableCollection<MedWalletTransactionModel> outputs = new ObservableCollection<MedWalletTransactionModel>();
            using (var context = new MedAssistContext())
            {
                var transactions = from transaction in context.MedWalletTransactions
                                   where transaction.AccountId == _loggedInUser.Id
                                   select transaction;

                //map model
                foreach(var transaction in transactions)
                {
                    outputs.Add(new MedWalletTransactionModel()
                    {
                        Id = transaction.Id,
                        AccountId = transaction.AccountId,
                        ChangeInBalance = transaction.ChangeInBalance,
                        Description = transaction.Description
                    });
                }

                return outputs;
            }
        }

        public ObservableCollection<Guid> GenerateVoucherCode(int count, decimal value)
        {
            ObservableCollection<Guid> output = new ObservableCollection<Guid>();
            using (var context = new MedAssistContext())
            {
                for(int x = 0; x < count; x++)
                {
                    //get all voucher GUIDs
                    var oldVouchers = context.MedWalletVouchers.ToList();
                    List<Guid> oldVoucherGuids = new List<Guid>();
                    foreach (var voucher in oldVouchers)
                    {
                        oldVoucherGuids.Add(voucher.Code);
                    }

                    //generate new voucher
                    var newVoucher = new MedWalletVoucher()
                    {
                        Code = GenerateUniqueGuid(oldVoucherGuids),
                        Value = value,
                        Claimed = false
                    };

                    output.Add(newVoucher.Code);
                    context.MedWalletVouchers.Add(newVoucher);
                }
            }
            return output;
        }

        private Guid GenerateUniqueGuid(List<Guid> previouslyGeneratedGuids)
        {
            bool IsUnique = true;
            Guid currentGuid;
            {
                IsUnique = true;
                currentGuid = Guid.NewGuid();

                foreach(var guid in previouslyGeneratedGuids)
                {
                    if(currentGuid.ToString() == guid.ToString())
                    {
                        IsUnique = false;
                    }
                }
            } while (!IsUnique);

            return currentGuid;
        }
    }
}
