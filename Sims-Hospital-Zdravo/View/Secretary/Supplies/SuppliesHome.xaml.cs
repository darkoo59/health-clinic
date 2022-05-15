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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Sims_Hospital_Zdravo.View.Secretary.Supplies
{
    /// <summary>
    /// Interaction logic for SuppliesHome.xaml
    /// </summary>
    public partial class SuppliesHome : Window
    {
        private App app;
        private SuppliesController _suppliesController;
        private List<String> addedSupplies;
        public SuppliesHome()
        {
            app = Application.Current as App;
            InitializeComponent();
            this._suppliesController = app._suppliesController;
            this.addedSupplies = new List<string>();
            foreach (RoomEquipment roomEquipment in _suppliesController.FindAllEquipment())
            {
                ListBoxAvaialble.Items.Add(roomEquipment.ToString());
            }
        }
        
        
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if(TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
                tt_meetings.Visibility = Visibility.Collapsed;
                tt_accounts.Visibility = Visibility.Collapsed;
                tt_equipment.Visibility = Visibility.Collapsed;
                tt_appointments.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_medical_records.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_sign_out.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_profile.Visibility = Visibility.Visible;
                tt_about.Visibility = Visibility.Visible;
                tt_meetings.Visibility = Visibility.Visible;
                tt_accounts.Visibility = Visibility.Visible;
                tt_equipment.Visibility = Visibility.Visible;
                tt_appointments.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_medical_records.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_sign_out.Visibility = Visibility.Visible;
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }
        
        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryWindow window = new SecretaryWindow(app._recordController);
            window.Show();
            this.Close();
        }
        
        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow(app._secretaryAppointmentController);
            window.Show();
            this.Close();
        }
        
        private void Equipment_Click(object sender, MouseButtonEventArgs e)
        {
            SuppliesHome window = new SuppliesHome();
            window.Show();
            this.Close();
        }

        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (!ListBoxAddedSupplies.Items.IsEmpty)
            {
                try
                {
                    List<String> equipmentNames = new List<String>();
                    List<int> equipmentQuantity = new List<int>();
                    for (int i = 0; i < ListBoxAddedSupplies.Items.Count; i++)
                    {
                        String[] parts = ListBoxAddedSupplies.Items[i].ToString().Split(':');
                        equipmentNames.Add(parts[0]);
                        equipmentQuantity.Add(Int32.Parse(parts[1]));
                    }

                    List<RoomEquipment> roomEquipments = new List<RoomEquipment>();
                    for (int i = 0; i < equipmentNames.Count; i++)
                    {
                        Equipment equipment = _suppliesController.CreateNewEquipment(equipmentNames[i]);
                        roomEquipments.Add(new RoomEquipment(equipment, equipmentQuantity[i]));
                    }
                    DateTime start = DateTime.Now;
                    DateTime end = DateTime.Now;
                    end = end.AddDays(3);
                    TimeInterval interval = new TimeInterval(start, end);
                    SuppliesAcquisition suppliesAcquisition =
                        new SuppliesAcquisition(_suppliesController.GenerateSuppliesAcquistionId(), roomEquipments,
                            interval);
                    _suppliesController.Create(suppliesAcquisition);
                    MessageBox.Show("Successfully made supplies acquisition!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else
                MessageBox.Show("First add some supplies!");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text.Length != 0 && txtQuantity.Text.Length != 0)
            {
                try
                {
                    ListBoxAddedSupplies.Items.Add(txtName.Text + ":" + txtQuantity.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
            }
            else
            {
                MessageBox.Show("Name or quanitity is empty", "Add data", MessageBoxButton.OK);
            }
        }
        
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAddedSupplies.SelectedItem != null)
            {
                ListBoxAddedSupplies.Items.Remove(ListBoxAddedSupplies.SelectedItem);
            }
        }
    }
}
