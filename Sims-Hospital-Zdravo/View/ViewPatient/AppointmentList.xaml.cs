using Controller;
using Model;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Utils.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for AppointmentList.xaml
    /// </summary>
    public partial class AppointmentList : Page
    {
        private AppointmentPatientController appointmentPatientController;
        private Appointment appointment;
        private List<Appointment> appointments;
        private ObservableCollection<Doctor> doctors;
        private Frame frame;
        private string priority;
        private Timer timer;
        private App app;
        public AppointmentList(Frame frame, Appointment appointment, string priority)
        {
            this.frame = frame;
            InitializeComponent();
            app = Application.Current as App;
            this.appointmentPatientController = app._appointmentPatientController;
            this.appointment = appointment;
            this.priority = priority;
            this.DataContext = this;
            appointments = appointmentPatientController.InitializeList(appointment,priority);
            Apps.ItemsSource = appointments;
            Apps.AutoGenerateColumns = false;

            Binding date = new Binding("Time.Start");
            date.StringFormat = "{0:dd/MM/yyyy}";
            DataGridTextColumn data_column = new DataGridTextColumn();
            data_column.Header = "Date";
            data_column.Binding = date;
            Apps.Columns.Add(data_column);

            data_column = new DataGridTextColumn();
            data_column.Header = "Time";
            Binding time = new Binding("Time.Start");
            time.StringFormat = "{0:HH:mm}";
            data_column.Binding = time;
            Apps.Columns.Add(data_column);

            MultiBinding doctor = new MultiBinding();
            doctor.StringFormat = "{0} {1}";
            doctor.Bindings.Add(new Binding("Doctor.Name"));
            doctor.Bindings.Add(new Binding("Doctor.Surname"));
            data_column = new DataGridTextColumn();
            data_column.Header = "Doctor";
            data_column.Binding = doctor;
            data_column.Width = 338;
            Apps.Columns.Add(data_column);

            if (appointments.Contains(appointment))
            {
                Apps.SelectedIndex = 0;
                Text.Text = "Your appointment is available";
            }
            else if (appointments.Count > 0)
            {
                Text.Text = "There is no available appointment with that parameters, here's some available suggestions";
            }
            else 
            {
                Text.Text = "There is no available appointment with that parameters";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appointment = (Appointment) Apps.SelectedItem;
            appointmentPatientController.Create(appointment);
            frame.Content = new PatientWindow(frame);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            frame.Content = new PatientCreate(frame);
        }
        public void PlayDemo()
        {
            List<IDemoCommand> commands = new List<IDemoCommand>
            {
                new SelectDataGridCommand(Apps,0)
            };
            timer = new Timer(1000);
            timer.Elapsed += (Object source, ElapsedEventArgs e1) => TimerCallback(commands);
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        private void TimerCallback(List<IDemoCommand> commands)
        {
            if (commands.Count > 0)
            {
                commands[0].Execute();
                commands.RemoveAt(0);
            }
            else
            {
                timer.Stop();
                App.Current.Dispatcher.Invoke((Action)delegate { frame.Content = new PatientWindow(frame); });
            }
        }
    }
}
