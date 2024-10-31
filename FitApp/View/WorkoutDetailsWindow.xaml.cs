using FitApp.Model;
using FitApp.ViewModel;
using System.Windows;

namespace FitApp.View
{
    /// <summary>
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {
        public WorkoutDetailsWindow(Workout workout, WorkoutsWindowViewModel workoutsViewModel)
        {
            InitializeComponent();
            this.DataContext = new WorkoutDetailsWindowViewModel(workout, this, workoutsViewModel);
        }
    }
}

