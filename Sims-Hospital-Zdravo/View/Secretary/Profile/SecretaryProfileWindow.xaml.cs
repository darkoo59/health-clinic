using Model;
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

namespace Sims_Hospital_Zdravo.View.Secretary.Profile
{
    /// <summary>
    /// Interaction logic for SecretaryProfileWindow.xaml
    /// </summary>
    public partial class SecretaryProfileWindow : Window
    {
        private App app;
        private User loggedUser;
        public SecretaryProfileWindow()
        {
            app = Application.Current as App;
            InitializeComponent();
            lblNameCaption.Content = app._accountController.GetLoggedAccount().Name + " " + app._accountController.GetLoggedAccount().Surname;
            loggedUser = app._accountController.GetLoggedAccount();
            txtName.Text = loggedUser.Name;
            txtSurname.Text = loggedUser.Surname;
            txtEmail.Text = loggedUser.Email;
            txtJmbg.Text = loggedUser.Jmbg;
            txtPhone.Text = loggedUser.PhoneNumber;
            txtUsername.Text = loggedUser.Username;
            pswPassword.Password = loggedUser.Password;
            datePickerBirthDate.SelectedDate = loggedUser.BirthDate;


        }


        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void ShowPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(pswPassword.Visibility == Visibility.Visible)
            {
                txtPasswrod.Text = pswPassword.Password;
                txtPasswrod.Visibility = Visibility.Visible;
                pswPassword.Visibility = Visibility.Hidden;
            }
            else
            {
                pswPassword.Password = txtPasswrod.Text;
                pswPassword.Visibility = Visibility.Visible;
                txtPasswrod.Visibility = Visibility.Hidden;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User userUpdated = new User();
                if (pswPassword.Visibility == Visibility.Visible)
                    userUpdated.Password = pswPassword.Password;
                else
                    userUpdated.Password = txtPasswrod.Text;
                userUpdated.Address = loggedUser.Address;
                userUpdated.BirthDate = (DateTime)datePickerBirthDate.SelectedDate;
                userUpdated.Blocked = loggedUser.Blocked;
                userUpdated.Cancels = loggedUser.Cancels;
                userUpdated.Email = txtEmail.Text;
                userUpdated.Id = loggedUser.Id;
                userUpdated.Jmbg = txtJmbg.Text;
                userUpdated.Name = txtName.Text;
                userUpdated.PhoneNumber = txtPhone.Text;
                userUpdated.Role = loggedUser.Role;
                userUpdated.Surname = txtSurname.Text;
                userUpdated.Username = txtUsername.Text;
                app._accountController.Update(userUpdated);
                MessageBox.Show("Profile successfully updated!", "Successfully updated!", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
