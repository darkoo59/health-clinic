using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Model;
using Controller;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class MyAppointmentsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Appointment> _appointments;
        private App app;
        private DoctorAppointmentController _appointmentController;

        public MyAppointmentsViewModel() { }
        public MyAppointmentsViewModel(int id,DoctorAppointmentController doctorAppointmentController)
        {
            _appointments = new ObservableCollection<Appointment>();
            app = App.Current as App;
            //this._appointmentController = app._doctorAppointmentController;
            this._appointmentController = doctorAppointmentController;
            this._appointments.Clear();
            foreach(Appointment app in _appointmentController.ReadAll(id))
            {
                this._appointments.Add(app);
                Console.WriteLine(app.Patient.Name+"ahahhaha");
            }
            
            
        }

        public ObservableCollection<Appointment> Appointments
        {
            get { return _appointments; }
            set { _appointments = value;
            OnPropertyChanged("Appointments"); 
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
