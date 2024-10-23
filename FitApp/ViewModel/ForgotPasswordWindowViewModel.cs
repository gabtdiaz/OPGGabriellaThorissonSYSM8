using FitApp.MVVM;
using FitApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FitApp.ViewModel
{
    public class ForgotPasswordWindowViewModel : ViewModelBase
    {
        public Window _forgotpassword;

        public ForgotPasswordWindowViewModel (Window forgotPasswordWindow)
        {
            _forgotpassword = forgotPasswordWindow;
        }
        public static implicit operator ForgotPasswordWindowViewModel(ForgotPasswordWindowVievModel v)
        {
            throw new NotImplementedException();
        }
    }
}
