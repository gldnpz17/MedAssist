using EventAggregatorLibrary;
using IoCContainerLibrary;
using MedAssistBusinessLogic;
using MedAssistUI.Factories;
using MedAssistUI.ViewModels;
using MedAssistUI.ViewModels.Interfaces;
using MedAssistUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedAssistUI
{
    internal static class ContainerConfig
    {
        internal static IContainer Configure()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Register(typeof(Container), typeof(IContainer), true);

            builder.Register(typeof(EventAggregator), typeof(IEventAggregator), true);
            builder.Register(typeof(MedAssistBLLibrary), typeof(MedAssistBLLibrary), true);

            //DI Factories
            builder.Register(typeof(DIFactory<WelcomeView>), typeof(IDIFactory<WelcomeView>), false);
            builder.Register(typeof(DIFactory<LoginView>), typeof(IDIFactory<LoginView>), false);
            builder.Register(typeof(DIFactory<RegisterView>), typeof(IDIFactory<RegisterView>), false);
            builder.Register(typeof(DIFactory<MedicationStoreView>), typeof(IDIFactory<MedicationStoreView>), false);
            builder.Register(typeof(DIFactory<AddToCartView>), typeof(IDIFactory<AddToCartView>), false);
            builder.Register(typeof(DIFactory<ManageCartView>), typeof(IDIFactory<ManageCartView>), false);
            builder.Register(typeof(DIFactory<CheckoutView>), typeof(IDIFactory<CheckoutView>), false);
            builder.Register(typeof(DIFactory<HealthcareView>), typeof(IDIFactory<HealthcareView>), false);
            builder.Register(typeof(DIFactory<MedWalletView>), typeof(IDIFactory<MedWalletView>), false);
            builder.Register(typeof(DIFactory<AmbulanceRequestView>), typeof(IDIFactory<AmbulanceRequestView>), false);
            builder.Register(typeof(DIFactory<AppointmentRequestView>), typeof(IDIFactory<AppointmentRequestView>), false);
            builder.Register(typeof(DIFactory<ManageDoctorsView>), typeof(IDIFactory<ManageDoctorsView>), false);
            builder.Register(typeof(DIFactory<ManageVouchersView>), typeof(IDIFactory<ManageVouchersView>), false);
            builder.Register(typeof(DIFactory<MedicationRequestView>), typeof(IDIFactory<MedicationRequestView>), false);
            builder.Register(typeof(DIFactory<PharmacyMedWalletView>), typeof(IDIFactory<PharmacyMedWalletView>), false);

            //Views
            builder.Register(typeof(MainView), typeof(MainView), false);
            builder.Register(typeof(LoginView), typeof(LoginView), false);
            builder.Register(typeof(RegisterView), typeof(RegisterView), false);
            builder.Register(typeof(WelcomeView), typeof(WelcomeView), false);
            builder.Register(typeof(MedicationStoreView), typeof(MedicationStoreView), false);
            builder.Register(typeof(AddToCartView), typeof(AddToCartView), false);
            builder.Register(typeof(ManageCartView), typeof(ManageCartView), false);
            builder.Register(typeof(CheckoutView), typeof(CheckoutView), false);
            builder.Register(typeof(HealthcareView), typeof(HealthcareView), false);
            builder.Register(typeof(MedWalletView), typeof(MedWalletView), false);
            builder.Register(typeof(AmbulanceRequestView), typeof(AmbulanceRequestView), false);
            builder.Register(typeof(AppointmentRequestView), typeof(AppointmentRequestView), false);
            builder.Register(typeof(ManageDoctorsView), typeof(ManageDoctorsView), false);
            builder.Register(typeof(ManageVouchersView), typeof(ManageVouchersView), false);
            builder.Register(typeof(MedicationRequestView), typeof(MedicationRequestView), false);
            builder.Register(typeof(PharmacyMedWalletView), typeof(PharmacyMedWalletView), false);

            //ViewModels
            builder.Register(typeof(MainViewModel), typeof(IMainViewModel), false);
            builder.Register(typeof(LoginViewModel), typeof(ILoginViewModel), false);
            builder.Register(typeof(RegisterViewModel), typeof(IRegisterViewModel), false);
            builder.Register(typeof(WelcomeViewModel), typeof(IWelcomeViewModel), false);
            builder.Register(typeof(MedicationStoreViewModel), typeof(IMedicationStoreViewModel), false);
            builder.Register(typeof(AddMedicationRequestViewModel), typeof(IAddMedicationRequestViewModel), false);
            builder.Register(typeof(ManageMedicationRequestsModel), typeof(IManagemedicationRequestsViewModel), false);
            builder.Register(typeof(CheckoutViewModel), typeof(ICheckoutViewModel), false);
            builder.Register(typeof(HealthcareViewModel), typeof(IHealthcareViewModel), false);
            builder.Register(typeof(MedWalletViewModel), typeof(IMedWalletViewModel), false);
            builder.Register(typeof(AmbulanceRequestViewModel), typeof(IAmbulanceRequestViewModel), false);
            builder.Register(typeof(AppointmentRequestViewModel), typeof(IAppointmentRequestViewModel), false);
            builder.Register(typeof(ManageDoctorsViewModel), typeof(IManageDoctorsViewModel), false);
            builder.Register(typeof(ManageVouchersViewModel), typeof(IManageVouchersViewModel), false);
            builder.Register(typeof(MedicationRequestViewModel), typeof(IMedicationRequestViewModel), false);
            builder.Register(typeof(PharmacyMedWalletViewModel), typeof(IPharmacyMedWalletViewModel), false);

            return builder.Build();
        }
    }
}
