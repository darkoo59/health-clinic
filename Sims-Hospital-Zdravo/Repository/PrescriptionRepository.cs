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
        private ObservableCollection<Prescription> _prescriptions;
        private PrescriptionDataHandler _prescriptionDataHandler;
        public PrescriptionRepository()
        {
            this._prescriptionDataHandler = new PrescriptionDataHandler();
            this._prescriptions = new ObservableCollection<Prescription>();
            LoadDataFromFiles();

        }

        public void Create(Prescription prescription)
        {
            _prescriptions.Add(prescription);
            LoadDataToFiles();
        }
        public void Delete(Prescription prescription)
        {
            _prescriptions.Remove(prescription);
        }

       
      
        public ObservableCollection<Prescription> ReadAll()
        {
            return _prescriptions;
        }
        
        public void LoadDataFromFiles()
        {
            _prescriptions = _prescriptionDataHandler.ReadAll();
        }
            public void LoadDataToFiles()
            {
                _prescriptionDataHandler.Write(_prescriptions);
            }


        }
    }

