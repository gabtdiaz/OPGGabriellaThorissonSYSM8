using FitApp.ViewModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainViewModel = new MainWindowViewModel();
            DataContext = mainViewModel;

        }
        //private void btnSignIn_Click(object sender, RoutedEventArgs e)
        //{
        //    var mainViewModel = DataContext as MainWindowViewModel;

        //    if (mainViewModel != null)
        //    {
        //        // Läs av lösenordet från PasswordBox och skicka det till ViewModel
        //        string enteredPassword = PasswordInput.Password;
        //        mainViewModel.PasswordInput = enteredPassword;

        //        // Kör inloggningslogiken i ViewModel
        //        mainViewModel.SignIn();
        //    }
        }
    }
