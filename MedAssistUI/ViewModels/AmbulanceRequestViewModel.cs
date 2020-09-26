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
using System.Xaml;

namespace MedAssistUI.ViewModels
{
    public class AmbulanceRequestViewModel : ViewModelBase, IAmbulanceRequestViewModel
    {
        private MedAssistBLLibrary _medAssistBLLibrary;

        public AmbulanceRequestViewModel(MedAssistBLLibrary medAssistLibrary)
        {
            _medAssistBLLibrary = medAssistLibrary;

            AmbulanceRequestModels = _medAssistBLLibrary.Healthcare.GetAmbulanceRequestsList();

            MarkAsDone = new MarkAsDoneCommand(this);
            RefreshRequests = new RefreshRequestsCommand(this);
        }

        private ObservableCollection<AmbulanceRequestModel> _ambulanceRequestModels;
        public ObservableCollection<AmbulanceRequestModel> AmbulanceRequestModels 
        {
            get { return _ambulanceRequestModels; }
            set
            {
                _ambulanceRequestModels = value;
                OnPropertyChanged();
            }
        }

        public MarkAsDoneCommand MarkAsDone { get; private set; }
        public RefreshRequestsCommand RefreshRequests { get; private set; }

        public class MarkAsDoneCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private AmbulanceRequestViewModel _parent;

            public MarkAsDoneCommand(AmbulanceRequestViewModel parent)
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
                AmbulanceRequestModel arg = (AmbulanceRequestModel)parameter;

                _parent._medAssistBLLibrary.Healthcare.MarkAmbulanceRequestAsDone(arg.Id);
                _parent.AmbulanceRequestModels.Remove(arg);
            }
        }
        public class RefreshRequestsCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private AmbulanceRequestViewModel _parent;

            public RefreshRequestsCommand(AmbulanceRequestViewModel parent)
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
                _parent.AmbulanceRequestModels = _parent._medAssistBLLibrary.Healthcare.GetAmbulanceRequestsList();
            }
        }
    }
}
