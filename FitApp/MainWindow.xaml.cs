using FitApp.Services;
using FitApp.ViewModel;
using System.Windows;


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
            UserManager userManager = new UserManager();
            this.DataContext = new MainWindowViewModel(this, userManager);  
        }

        // Lägg till en andra konstruktor som tar UserManager som parameter
        public MainWindow(UserManager userManager)
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel(this, userManager);
        }
    }
}

 
