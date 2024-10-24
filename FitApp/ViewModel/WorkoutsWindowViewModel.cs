using FitApp.Model;
using FitApp.MVVM;
using FitApp.Services;
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

        public Window workoutsWindow;
        private Window workoutDetailsWindow;
        public UserManager userManager;
        public ObservableCollection<Workout> Workouts { get; set; }
        
        public ICommand AddWorkoutCommand { get; }
        public ICommand UserDetailsCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand WorkoutDetailsCommand { get; }

        private Workout selectedWorkout;

        public Workout SelectedWorkout 
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
        public WorkoutsWindowViewModel(UserManager userManager, Window workoutsWindow) 
        {
            this.userManager = userManager;
            this.workoutsWindow = workoutsWindow;
            Workouts = new ObservableCollection<Workout>
        {
            new CardioWorkout { Type = "HIIT Run"},
            new StrengthWorkout { Type = "Full Body Strength"}
        };
            // Commands
            AddWorkoutCommand = new RelayCommand(AddWorkout);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
            UserDetailsCommand = new RelayCommand(UserDetails);
            WorkoutDetailsCommand = new RelayCommand(() => WorkoutDetails(SelectedWorkout));
            SignOutCommand = new RelayCommand(SignOut);
        }

        public WorkoutsWindowViewModel() {}


        // Metod som öppnar AddWorkoutWindow
        public void AddWorkout()
        {
            MessageBox.Show("Clicked");
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show(); // Öppnar nytt fönster
            Application.Current.MainWindow.Close();
        }

        // Metod som tar bort det valda träningspasset
        public void RemoveWorkout()
        {
            MessageBox.Show("Clicked");
            if (SelectedWorkout != null)
            {
                Workouts.Remove(SelectedWorkout);
            }
            else
            {
                MessageBox.Show("You must choose a workout to remove.", "Error", MessageBoxButton.OK);
            }

        }
        // Metod som öppnar UserDetailsWindow och visar användarens uppgifter
        public void UserDetails () 
        {
            MessageBox.Show("Clicked UserDetails");
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
            userDetailsWindow.Show();
            Application.Current.MainWindow.Close();
        }

        // Metod som öppnar WorkoutDetailsWindow visar ytterligare träningsdetajler
        public void WorkoutDetails(Workout workout) 
        {
            MessageBox.Show("Clicked");
            if (SelectedWorkout != null)
            {
                workoutDetailsWindow.Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Choose a Workout to see details","Error",MessageBoxButton.OK);
            }
        }

        // Metod som "nollställer" CurrentUser och navigerar till HomePage
        public void SignOut()
        {
            MessageBox.Show("Clicked");
            userManager.SignOut();  // Nollställer CurrentUser
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show(); // Öppnar helt nytt random fönster LOL
            Application.Current.MainWindow.Close();

        }

    }
}
