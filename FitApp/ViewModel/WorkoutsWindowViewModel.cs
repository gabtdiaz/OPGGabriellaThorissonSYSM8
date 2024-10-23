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

        public Window _workoutsWindow;
        //new List<Workout> WorkoutList = new List<Workout>();
        public ObservableCollection<Workout> Workouts { get; set; }

        public User currentUser {  get; set; }
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
        public WorkoutsWindowViewModel(User currentUser, Window workoutsWindow) 
        { 
            this.currentUser = currentUser;
            this._workoutsWindow = workoutsWindow;
            Workouts = new ObservableCollection<Workout>
        {
            new CardioWorkout { Type = "HIIT Run", },
            new StrengthWorkout { Type = "Full Body Strength", }
        };
            AddWorkoutCommand = new RelayCommand(AddWorkout);
            RemoveWorkoutCommand = new RelayCommand(RemoveWorkout);
            UserDetailsCommand = new RelayCommand(UserDetails);
            WorkoutDetailsCommand = new RelayCommand(() => WorkoutDetails(SelectedWorkout));
            SignOutCommand = new RelayCommand(SignOut);
        }

        public WorkoutsWindowViewModel()
        {
        }

        // Metoder

        // Metod som gör det möjligt för användaren att lägga till träningspass
        public void AddWorkout()
        {

            AddWorkoutWindow addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.Show(); // Öppnar nytt fönster
            Application.Current.MainWindow.Close();
            // Efter att fönstret stängs, lägg till ny workout.
            
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
            if (SelectedWorkout != null) 
            {
                UserDetailsWindow userDetailsWindow = new UserDetailsWindow();
                userDetailsWindow.Show();
                Application.Current.MainWindow.Close();
            }
        }

        // Metod som öppnar WorkoutDetailsWindow visar ytterligare träningsdetajler
        public void WorkoutDetails(Workout workout) 
        {
            {
                if (SelectedWorkout != null)
                {
                    WorkoutDetailsWindow workoutDetailsWindow = new WorkoutDetailsWindow();
                    workoutDetailsWindow.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    MessageBox.Show("Choose a Workout to see details","Error",MessageBoxButton.OK);
                }
            }
        }

        // Metod som stänger ner fönstret och öppnar MainWindow - inloggningssidan.
        public void SignOut()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.MainWindow.Close();

        }
    }
}
