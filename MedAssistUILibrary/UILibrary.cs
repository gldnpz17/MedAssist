using UILibrary.Authorization;
using UILibrary.Accounts;
using System;
using System.Collections.Generic;
using System.Text;
using IoCContainerLibrary;
using EventAggregatorLibrary;
using UILibrary.Events;
using UILibrary.Medication;
using MedAssistUILibrary.Authorization;

namespace UILibrary
{
    public class UILibrary
    {
        private IContainer _container;
        private IEventAggregator _eventAggregator;
        private UserCredential _loggedInUser;

        public UILibrary()
        {
            _container = ContainerConfig.Configure();

            _authorizationManager = _container.GetInstance<AuthorizationManager>();
            _accountManager = _container.GetInstance<AccountManager>();
            _medicationManager = _container.GetInstance<MedicationManager>();

            _eventAggregator = _container.GetInstance<IEventAggregator>();
        }

        private AuthorizationManager _authorizationManager;
        public AuthorizationManager Authorization { get { return _authorizationManager; } }

        private AccountManager _accountManager;
        public AccountManager Account { get { return _accountManager; } }

        private MedicationManager _medicationManager;
        public MedicationManager Medication { get { return _medicationManager; } }

        
    }
}
