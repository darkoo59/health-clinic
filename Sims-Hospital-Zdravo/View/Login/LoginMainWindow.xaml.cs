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
            switch (role)
            {
                case RoleType.MANAGER: return new ManagerDashboard();
                //case RoleType.DOCTOR: return new DoctorMain();
                case RoleType.PATIENT: return new PatientWindow();
                case RoleType.SECRETARY: return new SecretaryHome();
                default: return null;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            AccountController accountController = app._accountController;
            String username = txtUsername.Text;
            String password = txtPassword.Password.ToString();
            User account = accountController.GetAccountByUsernameAndPassword(username, password);
            if (account != null)
            {
                switch (account._Role)
                {
                    case RoleType.MANAGER:
                        ManagerDashboard manaegerHome = new ManagerDashboard();
                        this.Close();
                        manaegerHome.Show();
                        break;
                    case RoleType.SECRETARY:
                        MedicalRecordController medicalController = app._recordController;
                        SecretaryHome secretaryHomeWindow = new SecretaryHome();
                        this.Close();
                        secretaryHomeWindow.Show();
                        break;
                    case RoleType.DOCTOR:
                        DoctorAppointmentController doctorAppController = app._doctorAppointmentController;
                        RoomController roomControl = app._roomController;
                        int id = account._Id;
                        DoctorMain doctorMain = new DoctorMain(id);
                        this.Close();
                        doctorMain.Show();
                        break;
                    case RoleType.PATIENT:
                        AppointmentPatientController appointmentPatientController = app._appointmentPatientController;
                        PatientWindow pw = new PatientWindow();
                        this.Close();
                        pw.Show();
                        break;
                }
            }
            else
            {
                MessageBox.Show(" Incorrect Username/Password. Login Denied ", " Error! ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}