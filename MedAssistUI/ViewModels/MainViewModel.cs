using EventAggregatorLibrary;
using MedAssistBusinessLogic;
using MedAssistUI.Events;
using MedAssistUI.Factories;
using MedAssistUI.ViewModels.Interfaces;
using MedAssistUI.Views;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MedAssistUI.ViewModels
{
    class MainViewModel : ViewModelBase, IMainViewModel
    {
        private IEventAggregator _eventAggregator;
        private IDIFactory<LoginView> _loginViewFactory;
        private IDIFactory<WelcomeView> _welcomeViewFactory;
        private IDIFactory<MedicationStoreView> _medicationStoreViewFactory;
        private IDIFactory<RegisterView> _registerViewFactory;
        private IDIFactory<HealthcareView> _healthcareViewFactory;
        private IDIFactory<MedWalletView> _medWalletViewFactory;
        private IDIFactory<AmbulanceRequestView> _ambulanceRequestViewFactory;
        private IDIFactory<AppointmentRequestView> _appointmentRequestViewFactory;
        private IDIFactory<ManageDoctorsView> _manageDoctorsViewFactory;
        private IDIFactory<ManageVouchersView> _manageVouchersViewFactory;
        private IDIFactory<MedicationRequestView> _medicationRequestViewFactory;
        private IDIFactory<PharmacyMedWalletView> _pharmacyMedWalletViewFactory;

        private MedicationStoreView _medicationStoreView;
        private HealthcareView _healthcareView;
        private MedWalletView _medWalletView;

        private AmbulanceRequestView _ambulanceRequestView;
        private AppointmentRequestView _appointmentRequestView;
        private ManageDoctorsView _manageDoctorsView;
        private ManageVouchersView _manageVouchersView;

        private MedicationRequestView _medicationRequestView;
        private PharmacyMedWalletView _pharmacyMedWalletView;

        private MedAssistBLLibrary _medAssistLibrary;

        public MainViewModel(IEventAggregator eventAggregator, IDIFactory<LoginView> loginViewFactory, IDIFactory<WelcomeView> welcomeViewFactory,
            IDIFactory<MedicationStoreView> medicationStoreViewFactory, IDIFactory<RegisterView> registerViewFactory, IDIFactory<HealthcareView> healthcareViewFactory,
            IDIFactory<MedWalletView> medWalletViewFactory, MedAssistBLLibrary medAssistLibrary, IDIFactory<AmbulanceRequestView> ambulanceRequestViewFactory, 
            IDIFactory<AppointmentRequestView> appointmentRequestViewFactory, IDIFactory<ManageDoctorsView> manageDoctorsViewFactory, 
            IDIFactory<ManageVouchersView> manageVouchersViewFactory, IDIFactory<MedicationRequestView> medicationRequestViewFactory, 
            IDIFactory<PharmacyMedWalletView> pharmacyMedWalletViewFactory)
        {
            _eventAggregator = eventAggregator;
            _loginViewFactory = loginViewFactory;
            _welcomeViewFactory = welcomeViewFactory;
            _medicationStoreViewFactory = medicationStoreViewFactory;
            _registerViewFactory = registerViewFactory;
            _healthcareViewFactory = healthcareViewFactory;
            _medWalletViewFactory = medWalletViewFactory;
            _ambulanceRequestViewFactory = ambulanceRequestViewFactory;
            _appointmentRequestViewFactory = appointmentRequestViewFactory;
            _manageDoctorsViewFactory = manageDoctorsViewFactory;
            _manageVouchersViewFactory = manageVouchersViewFactory;
            _medicationRequestViewFactory = medicationRequestViewFactory;
            _pharmacyMedWalletViewFactory = pharmacyMedWalletViewFactory;

            _medAssistLibrary = medAssistLibrary;

            OpenMedicationStoreView = new OpenMedicationStoreViewCommand(this);
            OpenHealthcareView = new OpenHealthcareViewCommand(this);
            OpenMedWalletView = new OpenMedWalletViewCommand(this);

            OpenAmbulanceRequestView = new OpenAmbulanceRequestViewCommand(this);
            OpenAppointmentRequestView = new OpenAppointmentRequestViewCommand(this);
            OpenManageDoctorsView = new OpenManageDoctorsViewCommand(this);
            OpenManageVouchersView = new OpenManageVouchersViewCommand(this);

            OpenMedicationRequestView = new OpenMedicationRequestViewCommand(this);
            OpenPharmacyMedWalletView = new OpenPharmacyMedWalletViewCommand(this);

            LogOut = new LogOutCommand(this);

            _eventAggregator.GetEvent<UserLoggedInEvent>().Subscribe(OnUserLoggedIn);
            _eventAggregator.GetEvent<RegisterViewOpenedEvent>().Subscribe(OnRegisterViewOpened);
            _eventAggregator.GetEvent<RegisterViewClosedEvent>().Subscribe(OnRegisterViewClosed);

            ChildView = loginViewFactory.GetInstance();
            UserIsLoggedIn = false;
        }

        #region Events
        private void OnUserLoggedIn()
        {
            ChildView = _welcomeViewFactory.GetInstance();
            if (_medAssistLibrary.Authorization.GetUserInfo().IsHospital)
            {
                UserIsHospital = true;
                _ambulanceRequestView = _ambulanceRequestViewFactory.GetInstance();
                _appointmentRequestView = _appointmentRequestViewFactory.GetInstance();
                _manageDoctorsView = _manageDoctorsViewFactory.GetInstance();
                _manageVouchersView = _manageVouchersViewFactory.GetInstance();
            }
            else if (_medAssistLibrary.Authorization.GetUserInfo().IsPharmacy)
            {
                UserIsPharmacy = true;
                _medicationRequestView = _medicationRequestViewFactory.GetInstance();
                _pharmacyMedWalletView = _pharmacyMedWalletViewFactory.GetInstance();
            }
            else
            {
                UserIsEndUser = true;
                _medicationStoreView = _medicationStoreViewFactory.GetInstance();
                _healthcareView = _healthcareViewFactory.GetInstance();
                _medWalletView = _medWalletViewFactory.GetInstance();
            }
            UserIsLoggedIn = true;
        }
        private void OnRegisterViewOpened()
        {
            ChildView = _registerViewFactory.GetInstance();
        }
        private void OnRegisterViewClosed()
        {
            ChildView = _loginViewFactory.GetInstance();
        }
        #endregion

        #region Properties
        private UserControl _childView;
        public UserControl ChildView 
        {
            get { return _childView; } 
            private set
            {
                _childView = value;
                OnPropertyChanged();
            } 
        }

        private bool _userIsLoggedIn;
        public bool UserIsLoggedIn
        {
            get { return _userIsLoggedIn; }
            private set
            {
                _userIsLoggedIn = value;
                OnPropertyChanged();
            }
        }

        private bool _userIsHospital = false;
        public bool UserIsHospital
        {
            get { return _userIsHospital; }
            set
            {
                _userIsHospital = value;
                OnPropertyChanged();
            }
        }
        private bool _userIsPharmacy = false;
        public bool UserIsPharmacy
        {
            get { return _userIsPharmacy; }
            set
            {
                _userIsPharmacy = value;
                OnPropertyChanged();
            }
        }
        private bool _userIsEndUser = false;
        public bool UserIsEndUser
        {
            get { return _userIsEndUser; }
            set
            {
                _userIsEndUser = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public OpenMedicationStoreViewCommand OpenMedicationStoreView { get; private set; }
        public OpenHealthcareViewCommand OpenHealthcareView { get; private set; }
        public OpenMedWalletViewCommand OpenMedWalletView { get; private set; }

        public OpenAmbulanceRequestViewCommand OpenAmbulanceRequestView { get; private set; }
        public OpenAppointmentRequestViewCommand OpenAppointmentRequestView { get; private set; }
        public OpenManageDoctorsViewCommand OpenManageDoctorsView { get; private set; }
        public OpenManageVouchersViewCommand OpenManageVouchersView { get; private set; }

        public OpenMedicationRequestViewCommand OpenMedicationRequestView { get; private set; }
        public OpenPharmacyMedWalletViewCommand OpenPharmacyMedWalletView { get; private set; }

        public LogOutCommand LogOut { get; private set; }
        #endregion

        public class OpenMedicationStoreViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenMedicationStoreViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._medicationStoreView;
            }
        }
        public class OpenHealthcareViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenHealthcareViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._healthcareView;
            }
        }
        public class OpenMedWalletViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenMedWalletViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._medWalletView;
            }
        }

        public class OpenAmbulanceRequestViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenAmbulanceRequestViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._ambulanceRequestView;
            }
        }
        public class OpenAppointmentRequestViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenAppointmentRequestViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._appointmentRequestView;
            }
        }
        public class OpenManageDoctorsViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenManageDoctorsViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._manageDoctorsView;
            }
        }
        public class OpenManageVouchersViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenManageVouchersViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._manageVouchersView;
            }
        }

        public class OpenMedicationRequestViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenMedicationRequestViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._medicationRequestView;
            }
        }
        public class OpenPharmacyMedWalletViewCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public OpenPharmacyMedWalletViewCommand(MainViewModel parent)
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
                _parent.ChildView = _parent._pharmacyMedWalletView;
            }
        }

        public class LogOutCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            private MainViewModel _parent;

            public LogOutCommand(MainViewModel parent)
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
                _parent.UserIsLoggedIn = false;
                _parent.UserIsEndUser = false;
                _parent.UserIsHospital = false;
                _parent.UserIsPharmacy = false;
                _parent.ChildView = _parent._loginViewFactory.GetInstance();
            }
        }
    }
}
