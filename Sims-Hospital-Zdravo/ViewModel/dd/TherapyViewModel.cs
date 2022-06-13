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
        private List<string> _medicines;
        private MedicalRecordController _medicalRecordController;
        public ICommand makePrescriptionCommand => new MakePrescriptionCommand(_frame,_medicalRecord);
        private Frame _frame;

        public TherapyViewModel(int id,Frame frame )
        {
            this._medicalRecordController = new MedicalRecordController();
            this._medicalRecord = _medicalRecordController.FindById(id);
            this._frame = frame;

        }

        
        public MedicalRecord MedicalRecord
        {
            get
            {
                return _medicalRecord;
            }
        }
        public List<string> Medicines
        {
            get
            {
                return _medicines;
            }
        }

       public void  LoadMedicines()
        {

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
            ListOfMedecinesinSystem listOfMedecines = new ListOfMedecinesinSystem(_medicalRecord.Id,_medicalRecord);
            _frame.Content = listOfMedecines;
            
        }

        public event EventHandler CanExecuteChanged;
    }
}

