using MedAssistBusinessLogic.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using IoCContainerLibrary;
using EventAggregatorLibrary;
using MedAssistBusinessLogic.Events;
using MedAssistBusinessLogic.Medication;
using MedAssistBusinessLogic.Healthcare;
using MedAssistBusinessLogic.MedWallet;
using BusinessLogicV3.BindingModelMapper;

namespace MedAssistBusinessLogic
{
    public class MedAssistBLLibrary
    {
        private IContainer _container;
        private IEventAggregator _eventAggregator;

        public MedAssistBLLibrary()
        {
            _container = ContainerConfig.Configure();

            _authorizationManager = _container.GetInstance<AuthorizationManager>();
            _healthcareManager = _container.GetInstance<HealthcareManager>();
            _medicationManager = _container.GetInstance<MedicationManager>();
            _medWalletManager = _container.GetInstance<MedWalletManager>();

            MedicationRequestMapper = _container.GetInstance<MedicationRequestMapper>();

            _eventAggregator = _container.GetInstance<IEventAggregator>();
        }

        private AuthorizationManager _authorizationManager;
        public AuthorizationManager Authorization { get { return _authorizationManager; } }

        private HealthcareManager _healthcareManager;
        public HealthcareManager Healthcare { get { return _healthcareManager; } }

        private MedicationManager _medicationManager;
        public MedicationManager Medication { get { return _medicationManager; } }

        private MedWalletManager _medWalletManager;
        public MedWalletManager MedWallet { get { return _medWalletManager; } }

        public MedicationRequestMapper MedicationRequestMapper { get; private set; }
    }
}
