using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitApp.ViewModel
{
    public class SignInWindowViewModel : ViewModelBase
    {
        public void SignIn()
        {
            WorkoutsWindow workoutsWindow = new WorkoutsWindow();
            workoutsWindow.Show();
        }
    }
}
