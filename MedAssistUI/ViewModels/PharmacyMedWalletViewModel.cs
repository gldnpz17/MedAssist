using BusinessLogicV3.Models;
using MedAssistBusinessLogic;
using MedAssistUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedAssistUI.ViewModels
{
    public class PharmacyMedWalletViewModel : ViewModelBase, IPharmacyMedWalletViewModel
    {
        private MedAssistBLLibrary _medAssistLib;

        public PharmacyMedWalletViewModel(MedAssistBLLibrary medAssistLib)
        {
            _medAssistLib = medAssistLib;

            TransactionModels = _medAssistLib.MedWallet.GetMedWalletTransactions();

            RefreshTransactions = new RefreshTransactionsCommand(this);
        }

        private ObservableCollection<MedWalletTransactionModel> _transactionModels;
        public ObservableCollection<MedWalletTransactionModel> TransactionModels
        {
            get { return _transactionModels; }
            set
            {
                _transactionModels = value;
                OnPropertyChanged();
            }
        }

        public RefreshTransactionsCommand RefreshTransactions { get; private set; }

        public class RefreshTransactionsCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private PharmacyMedWalletViewModel _parent;

            public RefreshTransactionsCommand(PharmacyMedWalletViewModel parent)
            {
                _parent = parent;
                parent.PropertyChanged +=
                    (sender, e) =>
                    {
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    };
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _parent.TransactionModels = _parent._medAssistLib.MedWallet.GetMedWalletTransactions();
            }
        }
    }
}
