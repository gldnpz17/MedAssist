using BusinessLogicV3.Models;
using EventAggregatorLibrary;
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
    public class HealthcareViewModel : ViewModelBase, IHealthcareViewModel
    {
        private IEventAggregator _eventAggregator;
        private MedAssistBLLibrary _medAssistLib;

        public HealthcareViewModel(IEventAggregator eventAggregator, MedAssistBLLibrary medAssistLib)
        {
            _eventAggregator = eventAggregator;
            _medAssistLib = medAssistLib;

            DoctorModels = _medAssistLib.Healthcare.GetDoctorsList();

            Refresh = new RefreshCommand(this);
            RequestAppointment = new RequestAppointmentCommand(this);
            RequestAmbulance = new RequestAmbulanceCommand(this);
        }

        #region Binding Properties
        public ObservableCollection<DoctorModel> DoctorModels { get; private set; }
        #endregion

        #region Commands
        public RequestAppointmentCommand RequestAppointment { get; private set; }
        public RefreshCommand Refresh { get; private set; }
        public RequestAmbulanceCommand RequestAmbulance { get; private set; }
        #endregion

        public class RequestAppointmentCommand : ICommand
        {
            private HealthcareViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public RequestAppointmentCommand(HealthcareViewModel parent)
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
                DoctorModel selectedModel = (DoctorModel)parameter;
                _parent._medAssistLib.Healthcare.ArrangeAppointment(selectedModel.Id);
            }
        }
        public class RefreshCommand : ICommand
        {
            private HealthcareViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public RefreshCommand(HealthcareViewModel parent)
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
                _parent.DoctorModels = _parent._medAssistLib.Healthcare.GetDoctorsList();
            }
        }
        public class RequestAmbulanceCommand : ICommand
        {
            private HealthcareViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public RequestAmbulanceCommand(HealthcareViewModel parent)
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
                _parent._medAssistLib.Healthcare.RequestAmbulance();
            }
        }
    }
}
