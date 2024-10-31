using FitApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitApp.MVVM
{
    public class RelayCommand : ICommand
    {
        // Fält för att lagra den metod som ska köras
        private readonly Action execute;
        private readonly Action<object> executeWithParam;
        private readonly Func<bool> canExecute;

        // Konstruktor som tar ett parameterlöst kommando
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        // Konstruktor som tar ett kommando med en parameter t.ex. "Workout"
        public RelayCommand(Action<object> executeWithParam, Func<bool> canExecute = null)
        {
            this.executeWithParam = executeWithParam ?? throw new ArgumentNullException(nameof(executeWithParam));
            this.canExecute = canExecute;
        }

        // Bestämmer om kommandot kan köras
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        // Utför kommandot
        public void Execute(object parameter)
        {
            // Om kommandot tar en parameter, anropa med parametern
            if (executeWithParam != null)
            {
                executeWithParam(parameter);
            }
            // Om kommandot är parameterlöst, anropa det utan parameter
            else
            {
                execute();
            }
        }

        // Event som signalerar när kommandots CanExecute-värde har ändrats
        public event EventHandler? CanExecuteChanged;

        // Används för att explicit trigga omvärdering av om kommandot kan köras
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}