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
        private PatientMedicalRecordService patMedRecService;

        public PatientMedicalRecordController(PatientMedicalRecordService patientMedicalRecordService)
        {
            this.patMedRecService = patientMedicalRecordService;
        }

        public MedicalRecord findMedicalRecordByPatient(Patient patient)
        {
            return patMedRecService.findMedicalRecordByPatient(patient);
        }

    }
}
