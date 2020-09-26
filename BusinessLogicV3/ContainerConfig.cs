using MedAssistBusinessLogic.Authorization;
using MedAssistBusinessLogic.Medication;
using EventAggregatorLibrary;
using IoCContainerLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using MedAssistBusinessLogic.LocationService;
using MedAssistBusinessLogic;
using MedAssistBusinessLogic.Healthcare;
using MedAssistBusinessLogic.MedWallet;
using BusinessLogicV3.DataBindingModel;
using BusinessLogicV3.BindingModelMapper;

namespace MedAssistBusinessLogic
{
    internal static class ContainerConfig
    {
        internal static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(typeof(EventAggregator), typeof(IEventAggregator), true);
            builder.Register(typeof(LocationService.LocationService), typeof(ILocationService), true);

            builder.Register(typeof(MedicationRequestMapper), typeof(MedicationRequestMapper), false);

            //Managers
            builder.Register(typeof(AuthorizationManager), typeof(AuthorizationManager), false);
            builder.Register(typeof(HealthcareManager), typeof(HealthcareManager), false);
            builder.Register(typeof(MedicationManager), typeof(MedicationManager), false);
            builder.Register(typeof(MedWalletManager), typeof(MedWalletManager), false);

            return builder.Build();
        }
    }
}
