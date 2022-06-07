using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Controller
{
    public class AnamnesisController
    {

        private AnamnesisService _anamnesisService;

        public AnamnesisController()
        {
            this._anamnesisService = new AnamnesisService();
        }

        public void Create(Anamnesis anamnesis)
        {
            _anamnesisService.Create (anamnesis);
        }

        public void Update(Anamnesis anamnesis)
        {
            _anamnesisService.Update(anamnesis);
        }

        public  List<Anamnesis> FindAll()
        {
            return   _anamnesisService.FindAll();

        }
         public ObservableCollection<Anamnesis> FindAnamnesisByDoctor(int id)
        {
            return _anamnesisService.findAnamnesisByDoctor (id);

        }
        //public Anamnesis FindAnamnesisByAppointment(Appointment appointment)
        //{
        //    return _anamnesisService.FindAnamnesisByAppointment(appointment);
        //}

        public ObservableCollection<Anamnesis> FindAnamnesisByPatient(MedicalRecord medicalRecord)
        {
            return _anamnesisService.FindAnamnesisByPatient(medicalRecord);
        }

    }
}
