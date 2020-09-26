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
    public class ManageVouchersViewModel : ViewModelBase, IManageVouchersViewModel
    {
        private MedAssistBLLibrary _medAssistLib;

        public ManageVouchersViewModel(MedAssistBLLibrary medAssistLib)
        {
            _medAssistLib = medAssistLib;

            GenerateVouchers = new GenerateVouchersCommand(this);
        }

        private decimal _voucherValue;
        public decimal VoucherValue
        {
            get { return _voucherValue; }
            set
            {
                _voucherValue = value;
                OnPropertyChanged();
            }
        }

        private int _voucherCount;
        public int VoucherCount
        {
            get { return _voucherCount; }
            set
            {
                _voucherCount = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Guid> _generatedCodes;
        public ObservableCollection<Guid> GeneratedCodes 
        {
            get { return _generatedCodes; }
            set
            {
                _generatedCodes = value;
                OnPropertyChanged();
            }
        }

        public GenerateVouchersCommand GenerateVouchers { get; private set; }

        public class GenerateVouchersCommand : ICommand
        {
            private ManageVouchersViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public GenerateVouchersCommand(ManageVouchersViewModel parent)
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
                _parent.GeneratedCodes = _parent._medAssistLib.MedWallet.GenerateVoucherCode(_parent.VoucherCount, _parent.VoucherValue);
            }
        }
    }
}
