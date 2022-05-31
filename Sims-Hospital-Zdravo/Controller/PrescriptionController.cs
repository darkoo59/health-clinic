using Sims_Hospital_Zdravo.Model;
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
        private PrescriptionService _prescriptionService;

        public PrescriptionController(PrescriptionService prescriptionService)
        {
            this._prescriptionService = prescriptionService;
        }
        public ObservableCollection<Prescription> ReadAll()
        {
            return this._prescriptionService.ReadAll();
        }
    }
}
