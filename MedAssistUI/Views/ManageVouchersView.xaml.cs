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
    /// Interaction logic for ManageVouchersView.xaml
    /// </summary>
    public partial class ManageVouchersView : UserControl
    {
        private IManageVouchersViewModel _viewModel;

        public ManageVouchersView(IManageVouchersViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = _viewModel;
        }
    }
}
