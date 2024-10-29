using FitApp.Model;
using FitApp.MVVM;
using FitApp.Services;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace FitApp.ViewModel
{
    public class WorkoutsWindowViewModel : ViewModelBase
    {
        // Egenskaper
       
        // Referenser
        public Window workoutsWindow;
        public Window workoutDetailsWindow;
        public UserManager userManager;
        private RegisterWindowViewModel registerWindow;
        public ObservableCollection<Workout> Workouts { get; set; }
        
        // Commands
        public ICommand AddWorkoutCommand { get; }
        public ICommand UserDetailsCommand { get; }
        public ICommand RemoveWorkoutCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand WorkoutDetailsCommand { get; }

        public ICollectionView FilteredWorkouts { get; private set; }
        public ICommand FilterCommand { get; }
        public ICommand ClearFilterCommand { get; }

        // Filteregenskaper
        public DateTime? FilterDate { get; set; }
        public string FilterType { get; set; }
        public TimeSpan? FilterDuration { get; set; }

        // Egenskap som returnerar användarnamnet
        public string CurrentUserName
        {
            get
            {
                // Kontrollera om CurrentUser är null innan jag försöker få tillgång till Username
                return userManager.CurrentUser?.Username ?? "No User";
            }
            set
            {
                // Detta kommer inte direkt sätta CurrentUserName, utan det behöver hanteras via CurrentUser
                if (userManager.CurrentUser != null)
                {
                    userManager.CurrentUser.Username = value;
                    OnPropertyChanged(nameof(CurrentUserName));
                }
            }
        }
        // Egenskap som returnerar det valda träningspasset
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
            // Skapar träningspass
        {
            new CardioWorkout { Type = "Cardio", Distance = 5,  Duration = new TimeSpan(0, 20, 0), CaloriesBurned = 250, DateTime = new DateTime(2024, 10, 24, 08, 00, 0), Notes = "Run"},
            new StrengthWorkout { Type = "Strength", Repetitions = 10, Duration = new TimeSpan(0, 45, 0), CaloriesBurned = 300, DateTime = new DateTime(2024, 10, 20, 18, 30, 0), Notes = "Gym"}
        };
            // Commands
            AddWorkoutCommand = new RelayCommand(AddWorkout);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
            UserDetailsCommand = new RelayCommand(UserDetails);
            WorkoutDetailsCommand = new RelayCommand(() => WorkoutDetails(SelectedWorkout));
            SignOutCommand = new RelayCommand(SignOut);
            FilteredWorkouts = CollectionViewSource.GetDefaultView(Workouts);
            FilteredWorkouts.Filter = FilterWorkouts;

            FilterCommand = new RelayCommand(ApplyFilter);
            ClearFilterCommand = new RelayCommand(ClearFilter);
        }

        public WorkoutsWindowViewModel() {}


        // Metod som öppnar AddWorkoutWindow
        public void AddWorkout()
        {
            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow(this);
            addWorkoutWindow.Show();
            workoutsWindow.Close();
            
        }

        // Metod som tar bort det valda träningspasset
        public void RemoveWorkout()
        {
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
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(userManager, registerWindow);
            userDetailsWindow.Show();
            workoutsWindow.Close();
        }

        // Metod som öppnar WorkoutDetailsWindow visar ytterligare träningsdetajler
        public void WorkoutDetails(Workout workout) 
        {
        
            // Skapa och visa WorkoutDetailsWindow och spara referensen i workoutDetailsWindow
            workoutDetailsWindow = new WorkoutDetailsWindow(workout, this);
            workoutDetailsWindow.Show();
            
            workoutsWindow.Close();
            
        }

        // Metod som "nollställer" CurrentUser och navigerar till HomePage
        public void SignOut()
        {
            userManager.SignOut();  // Nollställer CurrentUser

            
            MainWindow mainWindow = new MainWindow(userManager); // öppnar MainWindow
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
            workoutsWindow.Close();
        }
        private bool FilterWorkouts(object obj)
        {
            if (obj is not Workout workout) return false;

            // Filtrera på datum
            if (FilterDate.HasValue && workout.DateTime.Date != FilterDate.Value.Date) return false;

            // Filtrera på typ
            if (!string.IsNullOrEmpty(FilterType) && !workout.Type.Equals(FilterType, StringComparison.OrdinalIgnoreCase)) return false;

            // Filtrera på varaktighet
            if (FilterDuration.HasValue && workout.Duration < FilterDuration.Value) return false;

            return true;
        }

        // Använder filter baserat på användarinmatning
        private void ApplyFilter()
        {
            FilteredWorkouts.Refresh();
        }

        // Rensar filter
        private void ClearFilter()
        {
            FilterDate = null;
            FilterType = null;
            FilterDuration = null;
            FilteredWorkouts.Refresh();
        }

    }
}
