using CuoiKi.Constants;
using CuoiKi.Controllers;
using CuoiKi.Models;
using CuoiKi.States;
using CuoiKi.UI.Manager;
using CuoiKi.UI.Staff;
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Employee? foundEmployee = _authController.Login(txtUsername.Text, txtPassword.Password);

            if (foundEmployee == null)
            {
                MessageBox.Show("Invalid Credentials");
                return;
            }

            LoginInfoState.Id = foundEmployee.ID;
            LoginInfoState.Name = foundEmployee.Name;
            LoginInfoState.Role = foundEmployee.Role;

            if (foundEmployee.Role == Role.Manager)
            {
                UI_ManagerForm uI_ManagerForm = new();
                uI_ManagerForm.Show();
                
            }
            else 
            {
                UI_StaffForm uI_StaffForm = new();
                uI_StaffForm.Show();
            }
            this.Close();
        }
    }
}
