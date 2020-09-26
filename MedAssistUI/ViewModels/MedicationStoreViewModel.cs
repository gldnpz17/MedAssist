using EventAggregatorLibrary;
using MedAssistUI.Events;
using MedAssistUI.Factories;
using MedAssistUI.ViewModels.Interfaces;
using MedAssistUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using MedAssistBusinessLogic;
using BusinessLogicV3.Models;
using BusinessLogicV3.DataBindingModel;

namespace MedAssistUI.ViewModels
{
    public class MedicationStoreViewModel : ViewModelBase, IMedicationStoreViewModel
    {
        private MedAssistBLLibrary _medAssistLib;
        private IEventAggregator _eventAggregator;

        private IDIFactory<AddToCartView> _addToCartViewFactory;
        private IDIFactory<ManageCartView> _manageMedicationRequestsViewFactory;
        private IDIFactory<CheckoutView> _checkoutViewFactory;

        private ObservableCollection<MedicationRequestModel> _medicationRequests = new ObservableCollection<MedicationRequestModel>();

        public MedicationStoreViewModel(MedAssistBLLibrary medAssistLib, IEventAggregator eventAggregator, IDIFactory<AddToCartView> addToCartViewFactory, 
            IDIFactory<ManageCartView> manageCartViewFactory, IDIFactory<CheckoutView> checkoutViewFactory)
        {
            _medAssistLib = medAssistLib;
            _eventAggregator = eventAggregator;
            _addToCartViewFactory = addToCartViewFactory;
            _manageMedicationRequestsViewFactory = manageCartViewFactory;
            _checkoutViewFactory = checkoutViewFactory;

            //load store items
            StoreItems = new ObservableCollection<MedicationModel>(_medAssistLib.Medication.GetMedicationsList());

            //initialize commands
            AddMedicationRequest = new AddMedicationRequestCommand(this);
            ManageMedicationRequests = new ManageMedicationRequestsCommand(this);
            Checkout = new CheckoutCommand(this);

            //subscribe to events
            _eventAggregator.GetEvent<AddMedicationRequestViewClosedEvent>().Subscribe(OnAddMedicationRequestViewClosed);
            _eventAggregator.GetEvent<ManageMedicationRequestsViewClosedEvent>().Subscribe(OnManageMedicationRequestsViewClosed);
            _eventAggregator.GetEvent<CheckoutViewClosedEvent>().Subscribe(OnCheckoutViewClosed);
            _eventAggregator.GetEvent<MedicationRequestAddedEvent>().Subscribe(OnMedicationRequestAdded);
            _eventAggregator.GetEvent<CheckoutSuccessfulEvent>().Subscribe(OnCheckoutSuccessful);
        }

        private void OnAddMedicationRequestViewClosed()
        {
            ChildView = null;
        }
        private void OnMedicationRequestAdded(MedicationRequestModel medicationRequest)
        {
            _medicationRequests.Add(medicationRequest);
            ChildView = null;
        }
        private void OnManageMedicationRequestsViewClosed(ObservableCollection<MedicationRequestModel> MedicationRequests)
        {
            _medicationRequests = MedicationRequests;
            ChildView = null;
        }
        private void OnCheckoutViewClosed()
        {
            ChildView = null;
        }
        private void OnCheckoutSuccessful()
        {
            _medicationRequests = new ObservableCollection<MedicationRequestModel>();
        }

        private UserControl _childView;
        public UserControl ChildView 
        {
            get
            {
                return _childView;
            }
            private set
            {
                _childView = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MedicationModel> StoreItems { get; private set; }

        public AddMedicationRequestCommand AddMedicationRequest { get; private set; }
        public ManageMedicationRequestsCommand ManageMedicationRequests { get; private set; }
        public CheckoutCommand Checkout { get; private set; }

        public class AddMedicationRequestCommand : ICommand
        {
            private MedicationStoreViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public AddMedicationRequestCommand(MedicationStoreViewModel parent)
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
                MedicationRequestModel MedicationRequest = new MedicationRequestModel()
                {
                    MedicationId = ((MedicationModel)parameter).Id
                };
                _parent.ChildView = _parent._addToCartViewFactory.GetInstance();
                _parent._eventAggregator.GetEvent<AddMedicationRequestViewOpenedEvent>().Publish(MedicationRequest);
            }
        }
        public class ManageMedicationRequestsCommand : ICommand
        {
            private MedicationStoreViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public ManageMedicationRequestsCommand(MedicationStoreViewModel parent)
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
                _parent.ChildView = _parent._manageMedicationRequestsViewFactory.GetInstance();
                _parent._eventAggregator.GetEvent<ManageMedicationRequestsViewOpenedEvent>().Publish(_parent._medicationRequests);
            }
        }
        public class CheckoutCommand : ICommand
        {
            private MedicationStoreViewModel _parent;

            public event EventHandler CanExecuteChanged;

            public CheckoutCommand(MedicationStoreViewModel parent)
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
                _parent.ChildView = _parent._checkoutViewFactory.GetInstance();
                _parent._eventAggregator.GetEvent<CheckoutViewOpenedEvent>().Publish(_parent._medicationRequests);
            }
        }
    }
}
