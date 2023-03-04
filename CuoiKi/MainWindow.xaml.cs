using CuoiKi.Controllers;
using CuoiKi.Models;
using CuoiKi.States;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Input;

namespace CuoiKi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AuthenticationController _authController;
        public MainWindow()
        {
            InitializeComponent();
            _authController = new AuthenticationController();
        }
        public bool IsDarkTheme { get; set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            ITheme theme = paletteHelper.GetTheme();

            if (IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark)
            {
                IsDarkTheme = false;
                theme.SetBaseTheme(Theme.Light);
            }
            else
            {
                IsDarkTheme = true;
                theme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void onLoginButtonClick(object sender, RoutedEventArgs e)
        {
            Employee? employee = _authController.Login(txtUsername.Text, txtPassword.Password);

            if (employee == null)
            {
                MessageBox.Show("Invalid Credentials");
                return;
            }
            LoginInfoState.getInstance().Id = employee.Id;
            LoginInfoState.getInstance().Name = employee.Name;
            LoginInfoState.getInstance().Role = employee.Role;
            MessageBox.Show("Logged In as " + LoginInfoState.getInstance().Name);
        }
    }
}
