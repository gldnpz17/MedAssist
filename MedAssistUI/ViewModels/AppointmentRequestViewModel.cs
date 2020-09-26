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
    public class AppointmentRequestViewModel : ViewModelBase, IAppointmentRequestViewModel
    {
        private MedAssistBLLibrary _medAssistLib;

        public AppointmentRequestViewModel(MedAssistBLLibrary medAssistLib)
        {
            _medAssistLib = medAssistLib;

            AppointmentRequestModels = _medAssistLib.Healthcare.GetAppointmentRequestsList();

            MarkAsDone = new MarkAsDoneCommand(this);
            RefreshRequests = new RefreshRequestsCommand(this);
        }

        private ObservableCollection<AppointmentRequestModel> _appointmentRequestModels;
        public ObservableCollection<AppointmentRequestModel> AppointmentRequestModels 
        { 
            get { return _appointmentRequestModels; }
            set
            {
                _appointmentRequestModels = value;
                OnPropertyChanged();
            } 
        }

        public MarkAsDoneCommand MarkAsDone { get; private set; }
        public RefreshRequestsCommand RefreshRequests { get; private set; }

        public class MarkAsDoneCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private AppointmentRequestViewModel _parent;

            public MarkAsDoneCommand(AppointmentRequestViewModel parent)
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
                AppointmentRequestModel arg = (AppointmentRequestModel)parameter;

                _parent._medAssistLib.Healthcare.MarkAppointmentRequestAsDone(arg.Id);
                _parent.AppointmentRequestModels.Remove(arg);
            }
        }
        public class RefreshRequestsCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private AppointmentRequestViewModel _parent;

            public RefreshRequestsCommand(AppointmentRequestViewModel parent)
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
                _parent.AppointmentRequestModels = _parent._medAssistLib.Healthcare.GetAppointmentRequestsList();
            }
        }
    }
}
