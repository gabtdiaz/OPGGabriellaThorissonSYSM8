﻿using FitApp.ViewModel;
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
using System.Windows.Shapes;

namespace FitApp.View
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
            RegisterWindowViewModel registerViewModel = new RegisterWindowViewModel();
            DataContext = registerViewModel; // kan göra såhär istället(?)
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterWindowViewModel viewModel)
            {
                viewModel.PasswordInput = ((PasswordBox)sender).Password;
            }
        }

        private void PasswordBoxConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegisterWindowViewModel viewModel)
            {
                viewModel.PasswordInput = ((PasswordBox)sender).Password; // är denna rätt?
            }
        }
    }
}
