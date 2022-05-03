using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Controller
{
    class PrescriptionController
    {
        private PrescriptionService prescriptionService;

        public PrescriptionController(PrescriptionService prescriptionService)
        {
            this.prescriptionService = prescriptionService;
        }
        public ObservableCollection<Prescription> ReadAll()
        {
            return this.prescriptionService.ReadAll();
        }
    }
}
