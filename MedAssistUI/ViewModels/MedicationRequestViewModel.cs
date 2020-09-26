using BusinessLogicV3.Models;
using MedAssistBusinessLogic;
using MedAssistUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedAssistUI.ViewModels
{
    public class MedicationRequestViewModel : ViewModelBase, IMedicationRequestViewModel
    {
        private MedAssistBLLibrary _medAssistBLLibrary;

        public MedicationRequestViewModel(MedAssistBLLibrary medAssistLibrary)
        {
            _medAssistBLLibrary = medAssistLibrary;

            MedicationRequestModels = _medAssistBLLibrary.Medication.GetMedicationRequestsList();

            MarkAsDone = new MarkAsDoneCommand(this);
            RefreshRequests = new RefreshRequestsCommand(this);
        }

        private ObservableCollection<MedicationRequestModel> _medicationRequestModels;
        public ObservableCollection<MedicationRequestModel> MedicationRequestModels
        {
            get { return _medicationRequestModels; }
            set
            {
                _medicationRequestModels = value;
                OnPropertyChanged();
            }
        }

        public MarkAsDoneCommand MarkAsDone { get; private set; }
        public RefreshRequestsCommand RefreshRequests { get; private set; }

        public class MarkAsDoneCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MedicationRequestViewModel _parent;

            public MarkAsDoneCommand(MedicationRequestViewModel parent)
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
                MedicationRequestModel arg = (MedicationRequestModel)parameter;

                _parent._medAssistBLLibrary.Medication.MarkMedicationRequestAsDone(arg.Id);
                _parent.MedicationRequestModels.Remove(arg);
            }
        }
        public class RefreshRequestsCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MedicationRequestViewModel _parent;

            public RefreshRequestsCommand(MedicationRequestViewModel parent)
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
                _parent.MedicationRequestModels = _parent._medAssistBLLibrary.Medication.GetMedicationRequestsList();
            }
        }
    }
}
