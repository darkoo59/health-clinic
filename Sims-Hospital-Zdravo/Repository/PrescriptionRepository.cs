using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Repository
{
    
    internal class PrescriptionRepository
    {
       private ObservableCollection<Prescription> prescriptions;
        public PrescriptionRepository()
        {
            this.prescriptions = new ObservableCollection<Prescription>();

        }

        public void Create(Prescription prescription)
        {
            prescriptions.Add(prescription);
        }
        public void Delete(Prescription prescription)
        {
            prescriptions.Remove(prescription);
        }
        public void LoadDataToFiles()
        {

        }

        public void LoadDataFromFiles()
        {

        }


    }
}
