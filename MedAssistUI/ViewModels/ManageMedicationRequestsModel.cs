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
    public class ManageMedicationRequestsModel : ViewModelBase, IManagemedicationRequestsViewModel
    {
        private IEventAggregator _eventAggregator;
        private MedAssistBLLibrary _medAssistLib;

        public ManageMedicationRequestsModel(IEventAggregator eventAggregator, MedAssistBLLibrary medAssistLib)
        {
            _eventAggregator = eventAggregator;
            _medAssistLib = medAssistLib;

            Close = new CloseCommand(this);
            RemoveMedicationRequest = new RemoveMedicationRequestCommand(this);

            _eventAggregator.GetEvent<ManageMedicationRequestsViewOpenedEvent>().Subscribe(OnManageMedicationRequestsViewOpened);
        }

        private void OnManageMedicationRequestsViewOpened(ObservableCollection<MedicationRequestModel> medicationRequests)
        {
            ObservableCollection<MedicationRequestBindingModel> medicationRequestBindingModels = new ObservableCollection<MedicationRequestBindingModel>();
            foreach(var item in medicationRequests)
            {
                medicationRequestBindingModels.Add(_medAssistLib.MedicationRequestMapper.MapToBindingModel(item));
            }
            MedicationRequests = medicationRequestBindingModels;
        }

        private ObservableCollection<MedicationRequestBindingModel> _medicationRequests;
        public ObservableCollection<MedicationRequestBindingModel> MedicationRequests
        {
            get { return _medicationRequests; }
            set
            {
                _medicationRequests = value;
                OnPropertyChanged();
            }
        }

        public CloseCommand Close { get; private set; }
        public RemoveMedicationRequestCommand RemoveMedicationRequest { get; private set; }

        public class CloseCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private ManageMedicationRequestsModel _parent;

            public CloseCommand(ManageMedicationRequestsModel parent)
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
                ObservableCollection<MedicationRequestModel> medicationRequests = new ObservableCollection<MedicationRequestModel>();
                foreach(var item in _parent.MedicationRequests)
                {
                    medicationRequests.Add(_parent._medAssistLib.MedicationRequestMapper.MapFromBindingModel(item));
                }

                _parent._eventAggregator.GetEvent<ManageMedicationRequestsViewClosedEvent>().Publish(medicationRequests);
            }
        }
        public class RemoveMedicationRequestCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private ManageMedicationRequestsModel _parent;

            public RemoveMedicationRequestCommand(ManageMedicationRequestsModel parent)
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
                _parent.MedicationRequests.Remove((MedicationRequestBindingModel)parameter);
            }
        }
    }
}
