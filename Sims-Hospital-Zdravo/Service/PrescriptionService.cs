using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Model
{
   public class PrescriptionService
    {
        private PrescriptionRepository _prescriptionRepository;

        public PrescriptionService()
        {
            this._prescriptionRepository = new PrescriptionRepository();
        }
        public ObservableCollection<Prescription> ReadAll()
        {
            return this._prescriptionRepository.ReadAll();
        }

        public void Create(Prescription prescription)
        {
            _prescriptionRepository.Create(prescription);
        }

        
    }
}
