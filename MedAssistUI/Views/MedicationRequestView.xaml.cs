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
    /// Interaction logic for MedicineRequestView.xaml
    /// </summary>
    public partial class MedicationRequestView : UserControl
    {
        private IMedicationRequestViewModel _viewModel;
        
        public MedicationRequestView(IMedicationRequestViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = _viewModel;
        }
    }
}
