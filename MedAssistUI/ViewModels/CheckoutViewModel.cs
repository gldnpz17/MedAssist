using BusinessLogicV3.DataBindingModel;
using BusinessLogicV3.Models;
using EventAggregatorLibrary;
using MedAssistBusinessLogic;
using MedAssistUI.Events;
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
    public class CheckoutViewModel : ViewModelBase, ICheckoutViewModel
    {
        private MedAssistBLLibrary _medAssistLib;
        private IEventAggregator _eventAggregator;
        private ObservableCollection<MedicationRequestModel> _medicationRequests;

        public CheckoutViewModel(MedAssistBLLibrary medAssistLib, IEventAggregator eventAggregator)
        {
            _medAssistLib = medAssistLib;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<CheckoutViewOpenedEvent>().Subscribe(OnCheckoutViewOpened);

            Cancel = new CancelCommand(this);
            Checkout = new CheckoutCommand(this);
        }

        private void OnCheckoutViewOpened(ObservableCollection<MedicationRequestModel> medicationRequests)
        {
            _medicationRequests = medicationRequests;
            ObservableCollection<MedicationRequestBindingModel> bindingModels = new ObservableCollection<MedicationRequestBindingModel>();
            foreach(var item in _medicationRequests)
            {
                bindingModels.Add(_medAssistLib.MedicationRequestMapper.MapToBindingModel(item));
            }
            MedicationRequests = bindingModels;
            
            foreach(var item in MedicationRequests)
            {
                CartTotal += item.Medication.Price * item.Amount;
            }
        }

        public ObservableCollection<MedicationRequestBindingModel> MedicationRequests { get; private set; }

        private decimal _cartTotal = 0;
        public decimal CartTotal
        {
            get { return _cartTotal; }
            private set
            {
                _cartTotal = value;

                OnPropertyChanged();
            }
        }

        public CancelCommand Cancel { get; private set; }
        public CheckoutCommand Checkout { get; private set; }

        public class CheckoutCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private CheckoutViewModel _parent;

            internal CheckoutCommand(CheckoutViewModel parent)
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
                try
                {
                    _parent._medAssistLib.Medication.Checkout(_parent._medicationRequests);

                    _parent._eventAggregator.GetEvent<CheckoutViewClosedEvent>().Publish();
                }
                catch(Exception ex)
                {
                    throw new Exception($"Error processing request : {ex.Message}");
                }
                _parent._eventAggregator.GetEvent<CheckoutSuccessfulEvent>().Publish();
            }
        }
        public class CancelCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private CheckoutViewModel _parent;

            internal CancelCommand(CheckoutViewModel parent)
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
                _parent._eventAggregator.GetEvent<CheckoutViewClosedEvent>().Publish();
            }
        }
    }
}
