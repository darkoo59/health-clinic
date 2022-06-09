using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Controller;
using Model;
using Controller;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class SearchPatientViewModel : BindableBase
    {
        private ObservableCollection<Patient> _patients;
        private DoctorAppointmentController _doctorAppointmentController;

        public SearchPatientViewModel()
        {
            //this._doctorAppointmentController = new DoctorAppointmentController();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
