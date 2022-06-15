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
        private SuppliesController _suppliesController;
        private List<RoomEquipment> _roomEquipment;
        private App app;
        public SuppliesHome()
        {
            InitializeComponent();
            app = Application.Current as App;
            this._suppliesController = new SuppliesController();
            foreach (RoomEquipment roomEquipment in _suppliesController.FindAllEquipment())
            {
                ListBoxAvaialble.Items.Add(roomEquipment.ToString());
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
                        new SuppliesAcquisition(roomEquipments,
                            interval);
                    _suppliesController.Create(suppliesAcquisition);
                    if(app._currentLanguage.Equals("en-US"))
                        MessageBox.Show("Successfully made supplies acquisition!");
                    else 
                        MessageBox.Show("Uspesno napravljen zahtev za dobavljanje opreme!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if(app._currentLanguage.Equals("en-US"))
                    MessageBox.Show("First add some supplies!");
                else 
                    MessageBox.Show("Prvo dodajte neku opremu!");
            }
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
                if(app._currentLanguage.Equals("en-US"))
                    MessageBox.Show("Name or quanitity is empty", "Add data", MessageBoxButton.OK);
                else 
                    MessageBox.Show("Ime ili količina su prazni", "Unesite podatke", MessageBoxButton.OK);
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
