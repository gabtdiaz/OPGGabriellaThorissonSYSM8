using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace FitApp.ViewModel
{
    public class WorkoutsWindowViewModel : MainWindowViewModel
    {
        // Egenskaper
        public User User {  get; set; }
        new List<Workout> WorkoutList = new List<Workout>();
        

        public ICommand AddWorkoutCommand { get; }
        public ICommand OpenDetailsCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand SignOutCommand { get; }

        private string selectedWorkout;

        public string SelectedWorkout 
        {
            get 
            { 
                return selectedWorkout;
            }
            set
            {
                selectedWorkout = value;
                OnPropertyChanged(nameof(SelectedWorkout));
            }
        }

        // Konstruktor
        public WorkoutsWindowViewModel(User currentUser) 
        {
            User = currentUser;
            WorkoutList = new List<Workout>
        {
            new CardioWorkout { Type = "Cardio", Distance = 5, Duration = new TimeSpan(0, 30, 0), CaloriesBurned = 300 },
            new StrengthWorkout { Type = "Strength", Repetitions = 10, Duration = new TimeSpan(0, 45, 0), CaloriesBurned = 400 }
        }; // tilldela direkt?

            AddWorkoutCommand = new RelayCommand(AddWorkout);
            OpenDetailsCommand = new RelayCommand(OpenDetails);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
            SignOutCommand = new RelayCommand(SignOut);
        }

        // Metoder
        public void AddWorkout()
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show();

        }
        public void RemoveWorkout()
        {
            //WorkoutList.Remove(SelectedWorkout);
        }
        public void OpenDetails (Workout workout) 
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
        }
        public void SignOut()
        {
            WorkoutsWindow workoutsWindow = new WorkoutsWindow(); 
            workoutsWindow.Close();
            MainWindow mainWindow = new MainWindow();
            workoutsWindow.Show(); // Inte VM, kommer det funka?
            
        }
    }
}
