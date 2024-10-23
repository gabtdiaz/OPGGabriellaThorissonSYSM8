using FitApp.Model;
using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace FitApp.ViewModel
{
    public class WorkoutsWindowViewModel : ViewModelBase
    {
        // Egenskaper
        new List<Workout> WorkoutList = new List<Workout>();
        public ObservableCollection<Workout> Workouts { get; set; }

        public User currentUser {  get; set; }
        public ICommand AddWorkoutCommand { get; }
        public ICommand UserDetailsCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand WorkoutDetailsCommand { get; }

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
            Workouts = new ObservableCollection<Workout>();
            {
                new CardioWorkout { Type = "Cardio", Distance = 5, Duration = new TimeSpan(0, 30, 0), CaloriesBurned = 300 };
                new StrengthWorkout { Type = "Strength", Repetitions = 10, Duration = new TimeSpan(0, 45, 0), CaloriesBurned = 400 };
            }; // tilldela direkt?

            AddWorkoutCommand = new RelayCommand(AddWorkout);
            UserDetailsCommand = new RelayCommand(UserDetails);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
            SignOutCommand = new RelayCommand(SignOut);
            WorkoutDetailsCommand = new RelayCommand(WorkoutDetails);

        }

        public WorkoutsWindowViewModel()
        {
        }

        // Metoder
        public void AddWorkout()
        {
            AddWorkoutWindowViewModel addWorkoutWindow = new AddWorkoutWindowViewModel();
            //addWorkoutWindow.Show(); // Öppnar nytt fönster
            Application.Current.MainWindow.Close();
            // Efter att fönstret stängs, lägg till ny workout.
            
        }
        public void RemoveWorkout()
        {
            if (SelectedWorkout != null)
            {
                //Workouts.Remove(SelectedWorkout);
            }
            else
            {
                MessageBox.Show("You must choose a workout to remove.", "Error", MessageBoxButton.OK);
            }

        }
        public void UserDetails () 
        {

        }

        public void WorkoutDetails(Workout workout) 
        {
        }

        public void SignOut()
        {
            WorkoutsWindowViewModel workoutsWindow = new WorkoutsWindowViewModel(); 
            //workoutsWindow.Close();
            MainWindowViewModel mainWindow = new MainWindowViewModel();
            //workoutsWindow.Show(); // Inte VM, kommer det funka?
            
        }
    }
}
