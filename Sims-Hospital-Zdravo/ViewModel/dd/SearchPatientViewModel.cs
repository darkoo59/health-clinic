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
        private List<MedicalRecord> _medicalRecords;
        private MedicalRecordController _medicalRecordController;
        private ObservableCollection<MedicalRecord> medicalRecordsCollection;
        private bool tooltip;



        public SearchPatientViewModel( bool  tooltip)
        {
            this._medicalRecordController = new MedicalRecordController();
           this._medicalRecords  = _medicalRecordController.FindAll() ;
             this.medicalRecordsCollection = new ObservableCollection<MedicalRecord>(_medicalRecords);
            this.tooltip = tooltip;
        }

        public ObservableCollection<MedicalRecord> MedicalRecordCollection
        {
            get
            {
                return medicalRecordsCollection;
            }
            
        }

        public bool Tooltip
        {
            get
            {
                return  tooltip;
            }
            set
            {
                tooltip = value;
            }
        }
    }
}
