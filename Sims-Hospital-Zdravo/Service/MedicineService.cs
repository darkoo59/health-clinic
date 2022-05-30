using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Repository;
using System.Collections.ObjectModel;
using Repository;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Service
{
    public class MedicineService
    {
        private MedicineRepository medicineRepository;
        private NotificationRepository notificationRepository;

        public MedicineService(MedicineRepository medicineRepository, NotificationRepository notificationRepository)
        {
            this.medicineRepository = medicineRepository;
            this.notificationRepository = notificationRepository;
        }

        public ref ObservableCollection<Medicine> ReadAllMedicine()
        {
            return ref medicineRepository.ReadAll();
        }

        public List<Medicine> FindByStatus(MedicineStatus status)
        {
            return medicineRepository.FindByStatus(status);
        }

        public void Delete(Medicine medicine)
        {
            medicineRepository.Delete(medicine);
        }

        public void DeleteById(int id)
        {
            medicineRepository.DeleteById(id);
        }

        public void Create(Medicine medicine)
        {
            medicineRepository.Create(medicine);
        }

        public void CreateMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            Create(medicine);
            notificationRepository.Create(notification);
        }

        public void ResubmitMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            DeleteById(medicine._Id);
            CreateMedicineWithNotifyingDoctor(medicine, notification);
        }

        public void ValidateMedicineWithNotifyingDoctor(Medicine medicine, Notification notification)
        {
            notificationRepository.Create(notification);
        }

        public int GenerateId()
        {
            List<Medicine> medicines = new List<Medicine>(medicineRepository.ReadAll());
            List<int> ids = new List<int>(medicines.Select(x => x._Id));

            int id = 0;

            while (ids.Contains(id))
            {
                id++;
            }

            return id;
        }
    }
}