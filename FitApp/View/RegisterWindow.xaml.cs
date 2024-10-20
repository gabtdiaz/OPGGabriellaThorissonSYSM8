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
            DataContext = registerViewModel;
        }

        private void Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //det ska inte hamna här.
        }
    }
}
