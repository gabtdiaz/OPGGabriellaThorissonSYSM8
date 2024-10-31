using FitApp.Services;
using FitApp.ViewModel;
using System.Windows;

namespace FitApp.View
{
    /// <summary>
    /// Interaction logic for WorkoutsWindow.xaml
    /// </summary>
    public partial class WorkoutsWindow : Window
    {
        public WorkoutsWindow(UserManager userManager)
        {
            InitializeComponent();
            WorkoutsWindowViewModel viewModel = new WorkoutsWindowViewModel(userManager, this);
            this.DataContext = viewModel;
        }
    }
}
