using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.DataHandler;

namespace Sims_Hospital_Zdravo.Repository
{

    public class PrescriptionRepository
    {
        private ObservableCollection<Prescription> prescriptions;
        private PrescriptionDataHandler prescriptionDataHandler;
        public PrescriptionRepository(PrescriptionDataHandler prescriptionDataHandler)
        {
            this.prescriptionDataHandler = prescriptionDataHandler;
            this.prescriptions = new ObservableCollection<Prescription>();
            this.prescriptionDataHandler = prescriptionDataHandler;
            LoadDataFromFiles();

        }

        public void Create(Prescription prescription)
        {
            prescriptions.Add(prescription);
            LoadDataToFiles();
        }
        public void Delete(Prescription prescription)
        {
            prescriptions.Remove(prescription);
        }

       
      
        public ObservableCollection<Prescription> ReadAll()
        {
            return prescriptions;
        }
        
        public void LoadDataFromFiles()
        {
            prescriptions = prescriptionDataHandler.ReadAll();
        }
            public void LoadDataToFiles()
            {
                prescriptionDataHandler.Write(prescriptions);
            }


        }
    }

