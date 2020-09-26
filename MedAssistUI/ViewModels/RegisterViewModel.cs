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
using System.Windows.Controls;
using System.Windows.Input;

namespace MedAssistUI.ViewModels
{
    public class RegisterViewModel : ViewModelBase, IRegisterViewModel
    {
        private MedAssistBLLibrary _medAssistLib;
        private IEventAggregator _eventAggregator;

        public RegisterViewModel(MedAssistBLLibrary medAssistLib, IEventAggregator eventAggregator)
        {
            _registerCommand = new RegisterCommand(this);
            _cancelCommand = new CancelCommand(this);

            _medAssistLib = medAssistLib;
            _eventAggregator = eventAggregator;
        }

        private string _username;
        public string Username 
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        private RegisterCommand _registerCommand;
        public RegisterCommand MedAssistRegisterCommand
        {
            get { return _registerCommand; }
        }

        private CancelCommand _cancelCommand;
        public CancelCommand MedAssistCancelRegisterCommand { get { return _cancelCommand; } }


        public class RegisterCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private RegisterViewModel _parent;

            public RegisterCommand(RegisterViewModel parent)
            {
                _parent = parent;

                _parent.PropertyChanged +=
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
                PasswordBox passwordBox = (PasswordBox)parameter;
                _parent._medAssistLib.Authorization.Register(_parent._username, passwordBox.Password);
                _parent._cancelCommand.Execute(this);
            }
        }

        public class CancelCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private RegisterViewModel _parent;

            public CancelCommand(RegisterViewModel parent)
            {
                _parent = parent;

                _parent.PropertyChanged +=
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
                _parent._eventAggregator.GetEvent<RegisterViewClosedEvent>().Publish();
            }
        }
    }
}
