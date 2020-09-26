using MedAssistUI.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MedAssistUI.Views
{
    /// <summary>
    /// Interaction logic for AppointmentRequestView.xaml
    /// </summary>
    public partial class AppointmentRequestView : UserControl
    {
        private IAppointmentRequestViewModel _viewModel;

        public AppointmentRequestView(IAppointmentRequestViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = _viewModel;
        }
    }
}
