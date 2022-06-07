using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Model;
using Sims_Hospital_Zdravo.Service;

namespace Sims_Hospital_Zdravo.Controller
{
    public class MedicineController
    {
        private MedicineService _medicineService;

        public MedicineController()
        {
            this._medicineService = new MedicineService();
        }

        public  List<Medicine> ReadAllMedicines()
        {
            return  _medicineService.ReadAllMedicine();
        }
        public void Update(Medicine medicine)
        {
            _medicineService.Update(medicine);
        }
        public void CreateMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            _medicineService.CreateMedicineWithNotifyingDoctor(medicine, notification);
        }

        public void ResubmitMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            _medicineService.ResubmitMedicineWithNotifyingDoctor(medicine, notification);
        }

        public void ValidateMedicineWithNotifyindMenager(Medicine medicine, Notification notification)
        {
            _medicineService.ValidateMedicineWithNotifyingDoctor(medicine, notification);
        }
        

        public int GenerateId()
        {
            return _medicineService.GenerateId();
        }

        public List<Medicine> PatientAllergicToMedicine(MedicalRecord medicalRecord)
        {
            return _medicineService.PatientAllergicToMedicine(medicalRecord);
        }
        //public void CheckIfPatientAllergicToMedicine(MedicalRecord medicalRecord)
        //{
        //    _medicineService.CheckIfPatientAllergicToMedicine(medicalRecord);
        //}
        // public void CheckIfPatientAllergicToMedicineIngredients(MedicalRecord medicalRecord)
        //{
        //    _medicineService.CheckIfPatientAllergicToMedicineIngredients(medicalRecord);

        //}

        public void ReturnListOfMedicineToStart()
        {
            _medicineService.ReturnListOfMedicinesToStart();
        }
    }
}