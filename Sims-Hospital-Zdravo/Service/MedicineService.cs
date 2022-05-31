using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Repository;
using System.Collections.ObjectModel;
using Repository;
using Model;
using Sims_Hospital_Zdravo.Model;


namespace Sims_Hospital_Zdravo.Service
{
    public class MedicineService
    {
        private MedicineRepository _medicineRepository;
        private NotificationRepository _notificationRepository;

        public MedicineService(MedicineRepository medicineRepository, NotificationRepository notificationRepository)
        {
            this._medicineRepository = medicineRepository;
            this._notificationRepository = notificationRepository;
        }

        public ref ObservableCollection<Medicine> ReadAllMedicine()
        {
            return ref _medicineRepository.ReadAll();
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
            List<Medicine> medicines = new List<Medicine>(_medicineRepository.ReadAll());
            List<int> ids = new List<int>(medicines.Select(x => x.Id));

            int id = 0;

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }

        public void CheckIfPatientAllergicToMedicine(MedicalRecord medicalRecord)
        {
            ObservableCollection<Medicine> medicines = _medicineRepository.ReadAll();
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
            ObservableCollection<Medicine> medicines = _medicineRepository.ReadAll();
            foreach(Medicine med in medicines)
            {
                med.NotAllergic = true;
            }
        }

        public void CheckIfPatientAllergicToMedicineIngredients(MedicalRecord medicalRecord)
        {
            ObservableCollection<Medicine> medicines = _medicineRepository.ReadAll();
            foreach(Medicine med in medicines)
            {
                foreach(string ingredient in med.Ingredients)
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