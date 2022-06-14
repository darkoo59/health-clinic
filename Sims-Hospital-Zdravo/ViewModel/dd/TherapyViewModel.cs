using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Controller;
using Model;
using Controller;
using Sims_Hospital_Zdravo.View.ViewDoctor;
using System.Windows.Input;

namespace Sims_Hospital_Zdravo.ViewModel.dd
{
    public class TherapyViewModel : BindableBase
    {

        private MedicalRecord _medicalRecord;
        private Frame frame;
        private List<Medicine> _medicines;
        private MedicalRecordController _medicalRecordController;
        private List<Prescription> _prescriptions;
        private MedicineController medicineController;
        public ICommand makePrescriptionCommand => new MakePrescriptionCommand(_frame,_medicalRecord);
        public ICommand prescriptionListCommand => new PrescriptionListCommand(_frame,_medicalRecord);
        private Frame _frame;

        public TherapyViewModel(int id,Frame frame )
        {
            this._medicalRecordController = new MedicalRecordController();
            this._medicalRecord = _medicalRecordController.FindById(id);
            this.medicineController = new MedicineController();
            this._frame = frame;
            LoadMedicines();

        }

        public List<Prescription> PrescriptionsOne
        {
            get
            {
                return _prescriptions;
            }
            set
            {
                _prescriptions = value;
            }
        }
        public MedicalRecord MedicalRecord
        {
            get
            {
                return _medicalRecord;
            }
        }
        public List<Medicine> Medicines
        {
            get
            {
                return _medicines;
            }
            set
            {
                _medicines = value;
            }
        }

       public void  LoadMedicines()
        {
            List<Medicine> meds = new List<Medicine>();
            meds = medicineController.ReadAllMedicines();
            Medicines = meds;
        }

    }
    public class MakePrescriptionCommand : ICommand
    {
        private MedicineController medicineController;
        private MedicalRecord _medicalRecord;
        private Frame _frame;

        public MakePrescriptionCommand(Frame frame,MedicalRecord medicalRecord)
        {
            this.medicineController = new MedicineController();
            this._frame = frame;
            this._medicalRecord = medicalRecord;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            ListOfMedecinesinSystem listOfMedecines = new ListOfMedecinesinSystem(_medicalRecord.Id,_medicalRecord,_frame);
            _frame.Content = listOfMedecines;
            
        }

        public event EventHandler CanExecuteChanged;
    }
    public class PrescriptionListCommand : ICommand
    {
        private MedicalRecordController medicalRecordController;
        private MedicalRecord _medicalRecord;
        private Frame _frame;

        public PrescriptionListCommand(Frame frame, MedicalRecord medicalRecord)
        {
            this.medicalRecordController = new MedicalRecordController();
            this._frame = frame;
            this._medicalRecord = medicalRecord;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            PrescriptionList prescriptionList = new PrescriptionList(medicalRecordController,_medicalRecord,_frame);
            _frame.Content = prescriptionList;

        }

        public event EventHandler CanExecuteChanged;
    }
}

