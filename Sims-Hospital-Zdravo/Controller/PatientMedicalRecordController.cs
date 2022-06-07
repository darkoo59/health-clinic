using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;
namespace Controller
{
    public class PatientMedicalRecordController
    {
        private PatientMedicalRecordService _patMedRecService;

        public PatientMedicalRecordController()
        {
            this._patMedRecService = new PatientMedicalRecordService();
        }

        public MedicalRecord findMedicalRecordByPatient(Patient patient)
        {
            return _patMedRecService.FindMedicalRecordByPatient(patient);
        }

    }
}
