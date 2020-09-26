using BusinessLogicV3.Models;
using MedAssistBusinessLogic;
using MedAssistUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedAssistUI.ViewModels
{
    public class MedWalletViewModel : ViewModelBase, IMedWalletViewModel
    {
        private MedAssistBLLibrary _medAssistLib;

        public MedWalletViewModel(MedAssistBLLibrary medAssistLib)
        {
            _medAssistLib = medAssistLib;

            Balance = _medAssistLib.MedWallet.GetMedWalletBalance();
            TransactionModels = _medAssistLib.MedWallet.GetMedWalletTransactions();

            RefreshBalance = new RefreshBalanceCommand(this);
            ClaimVoucher = new ClaimVoucherCommand(this);
            RefreshTransactions = new RefreshTransactionsCommand(this);
        }

        #region Properties
        private decimal _balance;
        public decimal Balance
        {
            get { return _balance; }
            set
            {
                _balance = value;
                OnPropertyChanged();
            }
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
        #endregion

        #region Commands
        public RefreshBalanceCommand RefreshBalance { get; private set; }
        public ClaimVoucherCommand ClaimVoucher { get; private set; }
        public RefreshTransactionsCommand RefreshTransactions { get; private set; }
        #endregion

        public class RefreshBalanceCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MedWalletViewModel _parent;

            public RefreshBalanceCommand(MedWalletViewModel parent)
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
                _parent.Balance = _parent._medAssistLib.MedWallet.GetMedWalletBalance();
            }
        }
        public class ClaimVoucherCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MedWalletViewModel _parent;

            public ClaimVoucherCommand(MedWalletViewModel parent)
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
                string code = (string)parameter;
                _parent._medAssistLib.MedWallet.ClaimVoucher(code);
            }
        }
        public class RefreshTransactionsCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MedWalletViewModel _parent;

            public RefreshTransactionsCommand(MedWalletViewModel parent)
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
