using IoCContainerLibrary;
using MedAssistUI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MedAssistUI
{
    class Bootstrapper
    {
        private IContainer _container;

        public Bootstrapper()
        {
            Application.Current.DispatcherUnhandledException += (sender, e) => MessageBox.Show(e.Exception.Message, "Unhandled Exception Thrown");
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => MessageBox.Show(e.ExceptionObject.ToString(), "Unhandled Exception");

            try
            {
                _container = ContainerConfig.Configure();

                _container.GetInstance<MainView>();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
