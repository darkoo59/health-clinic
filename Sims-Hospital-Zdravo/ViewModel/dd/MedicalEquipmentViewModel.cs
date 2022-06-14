using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public  class MedicalEquipmentViewModel:BindableBase
    {

        private List<MedicalEquipment> _medicalEquipment;
        private ICommand makeEquipment;
        private List<string> _medicalEquipementString;
        public MedicalEquipmentViewModel()
        {
            LoadMedicalEquiment();
        }
        public List<MedicalEquipment> MedicalEquipmentList
        {
            get
            {
                return _medicalEquipment;
            }
            set
            {
                _medicalEquipment = value;
            }
        }
        public List <string> MedicalEquipementString
        {
            get { return _medicalEquipementString; }
            set
            {
                _medicalEquipementString = value;
            }
        }
        public void LoadMedicalEquiment()
        {
            List<MedicalEquipment> medEq = new List<MedicalEquipment>();
            List<string> medEqString = new List<string>();
            medEq.Add(new MedicalEquipment("X-ray"));
            medEq.Add(new MedicalEquipment("Defirbillator"));
            medEq.Add(new MedicalEquipment("EKG-machine"));
            medEq.Add(new MedicalEquipment("Patient monitors"));
            _medicalEquipment = medEq;
            foreach(MedicalEquipment medeq in medEq)
            {
                medEqString.Add(medeq.MedicalEquipmentType);
            }
            MedicalEquipementString = medEqString;

            

        }



    }
    public class MakeEquipmentCommand : ICommand
    {
        

        public MakeEquipmentCommand()
        {
           
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("report successfully sent");

        }

        public event EventHandler CanExecuteChanged;
    }
}
