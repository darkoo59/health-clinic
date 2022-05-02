using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Service
{
    class PrescriptionService
    {
        private PrescriptionRepository prescriptionRepository;

        public PrescriptionService(PrescriptionRepository prescriptionRepository) 
        {
            this.prescriptionRepository = prescriptionRepository;
        }
        public ObservableCollection<Prescription> ReadAll() 
        {
            return this.prescriptionRepository.ReadAll();  
        }
    }
}
