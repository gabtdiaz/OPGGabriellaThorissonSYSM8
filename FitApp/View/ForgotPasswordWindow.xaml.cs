using FitApp.Services;
using FitApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitApp.View
{
    /// <summary>
    /// Interaction logic for ForgotPasswordWindow.xaml
    /// </summary>
    public partial class ForgotPasswordWindowVievModel : Window
    {
        public ForgotPasswordWindowVievModel(UserManager userManager)
        {
            InitializeComponent();
            this.DataContext = new ForgotPasswordWindowViewModel(userManager);
        }
    }
}
