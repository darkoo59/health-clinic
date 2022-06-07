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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Model;
using Controller;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for ViewRequestForFreeDays.xaml
    /// </summary>
    public partial class ViewRequestForFreeDays : Page
    {
        private RequestForFreeDaysController _requestForFreeDaysController;
        private App _app;
        private int _doctorId;
        public ViewRequestForFreeDays(int id)
        {
            InitializeComponent();
            this.DataContext = this;
            this._doctorId = id;
            this._app = App.Current as App;
            this._requestForFreeDaysController = new RequestForFreeDaysController();
            DataGridRequestForFreeDays.ItemsSource = _requestForFreeDaysController.ReadAllByDoctor(id);
            DataGridRequestForFreeDays.AutoGenerateColumns = false;
            DataGridTextColumn datacolumn = new DataGridTextColumn();
            datacolumn.Header = "Time Interval";
            datacolumn.Binding = new Binding("TimeInterval");
            DataGridRequestForFreeDays.Columns.Add(datacolumn);
            datacolumn = new DataGridTextColumn();
            datacolumn.Header = "Reason";
            datacolumn.Binding = new Binding("ReasonForFreeDays");
            DataGridRequestForFreeDays.Columns.Add(datacolumn);
            datacolumn = new DataGridTextColumn();
            datacolumn.Header = "Status";
            datacolumn.Binding = new Binding("Status");
            DataGridRequestForFreeDays.Columns.Add(datacolumn);
            

            


        }
    }
}
