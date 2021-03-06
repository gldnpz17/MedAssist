﻿using MedAssistUI.ViewModels.Interfaces;
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
    /// Interaction logic for DoctorsView.xaml
    /// </summary>
    public partial class ManageDoctorsView : UserControl
    {
        private IManageDoctorsViewModel _viewModel;

        public ManageDoctorsView(IManageDoctorsViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;

            DataContext = _viewModel;
        }
    }
}
