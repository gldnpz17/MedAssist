using UILibrary.Accounts;
using UILibrary.Authorization;
using UILibrary.Medication;
using EventAggregatorLibrary;
using IoCContainerLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using MedAssistUILibrary.Facades;
using MedAssistUILibrary;

namespace UILibrary
{
    internal static class ContainerConfig
    {
        internal static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(typeof(EventAggregator), typeof(IEventAggregator), true);
            builder.Register(typeof(LocationService), typeof(ILocationService), true);

            //Managers
            builder.Register(typeof(AuthorizationManager), typeof(AuthorizationManager), false);
            builder.Register(typeof(AccountManager), typeof(AccountManager), false);
            builder.Register(typeof(MedicationManager), typeof(MedicationManager), false);

            return builder.Build();
        }
    }
}
