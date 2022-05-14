using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.View.ViewDoctor;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.View.Manager;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Sims_Hospital_Zdravo.View.Login
{
    /// <summary>
    /// Interaction logic for LoginMainWindow.xaml
    /// </summary>
    public partial class LoginMainWindow : Window
    {
        App app;

        public LoginMainWindow()
        {
            InitializeComponent();
            app = System.Windows.Application.Current as App;
        }

        private Window GetWindowByRole(RoleType role)
        {
            AccountController accountController = app._accountController;
            String username = txtUsername.Text;
            String password = txtPassword.Password.ToString();
            User account = accountController.GetAccountByUsernameAndPassword(username, password);
            int id = account._Id;
            //             DoctorMain doctorMain = new DoctorMain(id);
            switch (role)
            {
                case RoleType.MANAGER: return new ManagerMainWindow();
                case RoleType.DOCTOR: return new DoctorMain(id);
                case RoleType.PATIENT: return new PatientWindow();
                // case RoleType.DOCTOR: return new DoctorMain();
                case RoleType.PATIENT: return new PatientDashboard();
                case RoleType.SECRETARY: return new SecretaryHome();
                default: return null;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AccountController accountController = app._accountController;
                String username = txtUsername.Text;
                String password = txtPassword.Password.ToString();
                accountController.Login(username, password);
                User account = accountController.GetLoggedAccount();
                Window window = GetWindowByRole(account._Role);
                window.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}