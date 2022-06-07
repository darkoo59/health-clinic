using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Repository;
using System.Collections.ObjectModel;
using Repository;
using Model;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;


namespace Sims_Hospital_Zdravo.Service
{
    public class MedicineService
    {
        private IMedicineRepository _medicineRepository;
        private INotificationRepository _notificationRepository;

        public MedicineService()
        {
            _medicineRepository = new MedicineRepository();
            _notificationRepository = new NotificationRepository();
        }

        public  List<Medicine> ReadAllMedicine()
        {
            return  _medicineRepository.FindAll();
        }

        public List<Medicine> FindByStatus(MedicineStatus status)
        {
            return _medicineRepository.FindByStatus(status);
        }

        public void Delete(Medicine medicine)
        {
            _medicineRepository.Delete(medicine);
        }

        public void DeleteById(int id)
        {
            _medicineRepository.DeleteById(id);
        }

        public void Create(Medicine medicine)
        {
            _medicineRepository.Create(medicine);
        }

        public void CreateMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            Create(medicine);
            _notificationRepository.Create(notification);
        }

        public void ResubmitMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            DeleteById(medicine.Id);
            CreateMedicineWithNotifyingDoctor(medicine, notification);
        }

        public void ValidateMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public int GenerateId()
        {
            List<Medicine> medicines = new List<Medicine>(_medicineRepository.FindAll());
            List<int> ids = new List<int>(medicines.Select(x => x.Id));

            int id = 0;

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }


        public List <Medicine> PatientAllergicToMedicine(MedicalRecord medicalRecord)
        {
            List<Medicine> medicines1 = _medicineRepository.FindAll();
             CheckIfPatientAllergicToMedicine(medicalRecord,medicines1);
            CheckIfPatientAllergicToMedicineIngredients(medicalRecord,medicines1);
            return medicines1;

        }
                
        public void CheckIfPatientAllergicToMedicine(MedicalRecord medicalRecord,List<Medicine> medicines)
        {
            
            foreach (Medicine med in medicines)
            {

                if (medicalRecord.PatientAllergens.MedicalAllergens.Contains(med.Name))
                {
                    
                    med.NotAllergic = false;
                }
            }
            
        }

        public void ReturnListOfMedicinesToStart()
        {
            List<Medicine> medicines = _medicineRepository.FindAll();
            foreach (Medicine med in medicines)
            {
                med.NotAllergic = true;
            }
        }

        public void CheckIfPatientAllergicToMedicineIngredients(MedicalRecord medicalRecord, List<Medicine> medicines)
        {
            
            foreach (Medicine med in medicines)
            {
                foreach (string ingredient in med.Ingredients)
                {
                    if (medicalRecord.PatientAllergens.MedicalAllergens.Contains(ingredient))
                    {
                        
                        med.NotAllergic = false;
                    }
                }
            }
        }
    }
}