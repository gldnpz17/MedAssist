using EventAggregatorLibrary;
using MedAssistUI.Events;
using MedAssistUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using MedAssistBusinessLogic;
using BusinessLogicV3.Models;
using BusinessLogicV3.DataBindingModel;
using System.Runtime.InteropServices;

namespace MedAssistUI.ViewModels
{
    public class AddMedicationRequestViewModel : ViewModelBase, IAddMedicationRequestViewModel
    {
        private IEventAggregator _eventAggregator;

        private MedAssistBLLibrary _medAssistLib;

        public AddMedicationRequestViewModel(IEventAggregator eventAggregator, MedAssistBLLibrary medAssistLibrary)
        {
            _eventAggregator = eventAggregator;
            _medAssistLib = medAssistLibrary;

            _eventAggregator.GetEvent<AddMedicationRequestViewOpenedEvent>().Subscribe(OnAddMedicationRequestViewOpenedEvent);

            AddMedicationRequest = new AddMedicationRequestCommand(this);
            Cancel = new CancelCommand(this);
        }

        private MedicationRequestBindingModel _medicationRequest = new MedicationRequestBindingModel();
        public MedicationRequestBindingModel MedicationRequest 
        {
            get { return _medicationRequest; }
            set 
            {
                _medicationRequest = value;
                OnPropertyChanged();
            }
        }

        private void OnAddMedicationRequestViewOpenedEvent(MedicationRequestModel medicationRequest)
        {
            MedicationRequest = _medAssistLib.MedicationRequestMapper.MapToBindingModel(medicationRequest);
        }

        public AddMedicationRequestCommand AddMedicationRequest { get; private set; }
        public CancelCommand Cancel { get; private set; }

        public class CancelCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private AddMedicationRequestViewModel _parent;

            internal CancelCommand(AddMedicationRequestViewModel parent)
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
                _parent._eventAggregator.GetEvent<AddMedicationRequestViewClosedEvent>().Publish();
            }
        }
        public class AddMedicationRequestCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private AddMedicationRequestViewModel _parent;

            internal AddMedicationRequestCommand(AddMedicationRequestViewModel parent)
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
                    _parent._eventAggregator.GetEvent<MedicationRequestAddedEvent>().Publish(_parent._medAssistLib.MedicationRequestMapper.MapFromBindingModel(_parent.MedicationRequest));
                }
                catch
                {

                }
            }
        }
    }
}
