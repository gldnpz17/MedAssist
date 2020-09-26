using EventAggregatorLibrary;
using MedAssistBusinessLogic;
using MedAssistUI.Events;
using MedAssistUI.ViewModels.Interfaces;
using MedAssistUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedAssistUI.ViewModels
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private IEventAggregator _eventAggregator;
        private MedAssistBLLibrary _medAssistLib;

        public LoginViewModel(IEventAggregator eventAggregator, MedAssistBLLibrary medAssistLib)
        {
            _eventAggregator = eventAggregator;
            _medAssistLib = medAssistLib;

            Login = new LoginCommand(this);
            Register = new RegisterCommand(this, eventAggregator);
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (value == _username) { return; }
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _loginError;
        public string LoginError
        {
            get { return _loginError; }
            private set
            {
                _loginError = value;

                if (string.IsNullOrEmpty(_loginError))
                {
                    LoginErrorVisible = false;
                }
                else
                {
                    LoginErrorVisible = true;
                }

                OnPropertyChanged();
            }
        }

        private bool _loginErrorVisible;
        public bool LoginErrorVisible
        {
            get { return _loginErrorVisible; }
            private set
            {
                _loginErrorVisible = value;
                OnPropertyChanged();
            }
        }

        public LoginCommand Login { get; private set; }

        public RegisterCommand Register { get; private set; }

        public class LoginCommand : ICommand
        {
            private LoginViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public LoginCommand(LoginViewModel parent)
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
                bool ExceptionThrown = false;
                try
                {
                    PasswordBox passwordBox = (PasswordBox)parameter;
                    _parent._medAssistLib.Authorization.LogIn(_parent.Username, passwordBox.Password);

                    _parent._eventAggregator.GetEvent<UserLoggedInEvent>().Publish();
                }
                catch (Exception ex)
                {
                    _parent.LoginError = ex.Message;
                    ExceptionThrown = true;
                }

                if(ExceptionThrown == false)
                {
                    _parent.LoginError = "";
                }
            }
        }
        public class RegisterCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private LoginViewModel _parent;
            private IEventAggregator _eventAggregator;

            internal RegisterCommand(LoginViewModel parent, IEventAggregator eventAggregator)
            {
                _parent = parent;
                parent.PropertyChanged +=
                    (sender, e) =>
                    {
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                    };
                _eventAggregator = eventAggregator;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _eventAggregator.GetEvent<RegisterViewOpenedEvent>().Publish();
            }
        }
    }
}