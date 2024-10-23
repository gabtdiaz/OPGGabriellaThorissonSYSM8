using FitApp.Model;
using FitApp.ViewModel;
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
    /// Interaction logic for WorkoutsWindow.xaml
    /// </summary>
    public partial class WorkoutsWindow : Window
    {
        private readonly User currentUser;
        public WorkoutsWindow(User user)
        {
            InitializeComponent();
            currentUser = user; // Tilldela användaren som skickas in
            var viewModel = new WorkoutsWindowViewModel(currentUser, this);
            DataContext = viewModel;
        }
    }
}
