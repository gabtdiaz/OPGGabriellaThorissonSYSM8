﻿using FitApp.Model;
using FitApp.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitApp.ViewModel
{
    public class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        // Egenskaper
        public Window workoutDetailsWindow;
        public Workout Workout {  get; set; }

        // Konstruktor
        public WorkoutDetailsWindowViewModel(Workout Workout, Window workoutDetailsWindow)
        {
            this.Workout = Workout; 
            this.workoutDetailsWindow = workoutDetailsWindow;
        }

        // Metoder
        public void EditWorkout() 
        {
            // listbox med träningar, man ska kunna redigera, lägga till och spara.
        }
        public void SaveWorkout() 
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
