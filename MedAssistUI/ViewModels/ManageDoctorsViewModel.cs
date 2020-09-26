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
using System.Windows.Interop;

namespace MedAssistUI.ViewModels
{
    public class ManageDoctorsViewModel : ViewModelBase, IManageDoctorsViewModel
    {
        private MedAssistBLLibrary _medAssistLib;

        public ManageDoctorsViewModel(MedAssistBLLibrary medAssistLib)
        {
            _medAssistLib = medAssistLib;

            DoctorModels = _medAssistLib.Healthcare.GetDoctorsList();

            SetStatus = new SetStatusCommand(this);
        }

        private ObservableCollection<DoctorModel> _doctorModels;
        public ObservableCollection<DoctorModel> DoctorModels 
        {
            get { return _doctorModels; }
            set
            {
                _doctorModels = value;
                OnPropertyChanged();
            }
        }

        private DoctorModel _selectedDoctor;
        public DoctorModel SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                _selectedDoctor = value;
                OnPropertyChanged();
            }
        }

        private string _currentStatus;
        public string CurrentStatus
        {
            get { return _currentStatus; }
            set
            {
                _currentStatus = value;
                OnPropertyChanged();
            }
        }

        public SetStatusCommand SetStatus { get; private set; }

        public class SetStatusCommand : ICommand
        {
            private ManageDoctorsViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public SetStatusCommand(ManageDoctorsViewModel parent)
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
                _parent._medAssistLib.Healthcare.SetDoctorStatus(_parent.SelectedDoctor.Id, _parent.CurrentStatus);
                _parent.DoctorModels = _parent._medAssistLib.Healthcare.GetDoctorsList();
            }
        }
    }
}
