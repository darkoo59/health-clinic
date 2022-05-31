using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.DataHandler;
namespace Sims_Hospital_Zdravo.Repository
{
    public class AnamnesisRepository
    {
        private AnamnesisDataHandler _anamnesisDataHandler;
        private ObservableCollection<Anamnesis> _anamnesis;
        public AnamnesisRepository(AnamnesisDataHandler anamnesisDataHandler)
        {

            this._anamnesisDataHandler = anamnesisDataHandler;
            _anamnesis = new ObservableCollection<Anamnesis>();
            LoadDataFromFiles();

        }


        public void Create (Anamnesis anamnesis)
        {
            _anamnesis.Add(anamnesis);
            LoadDataToFiles();
        }

        public void Update (Anamnesis anamnesis)
        {
            foreach( Anamnesis anam in _anamnesis)
            {
                if (anam.Doctor == anam.Doctor)
                {
                    anam.Diagnosis = anam.Diagnosis;
                    anam.Anamensis = anam.Anamensis;
                    LoadDataToFiles();
                    return;
                }
            }
            

        }
        public ObservableCollection<Anamnesis> FindAnamnesisByDoctor(int id)
        {
            ObservableCollection<Anamnesis> listOfAnamnesisByDoctor = new ObservableCollection<Anamnesis>();
            
            foreach(Anamnesis anam in _anamnesis)
            {
                if(anam.Doctor._Id == id)
                {
                    listOfAnamnesisByDoctor.Add(anam);
                }
            }
            return listOfAnamnesisByDoctor;
            
            
        }
        public ObservableCollection<Anamnesis> FindAnamesisByPatient(MedicalRecord medicalRecord)
        {
            ObservableCollection<Anamnesis> listOfPatientAnamnesis = new ObservableCollection<Anamnesis>();
            listOfPatientAnamnesis = medicalRecord.Anamnesis;
            foreach(Anamnesis anma in listOfPatientAnamnesis)
            {
                Console.WriteLine(anma.Diagnosis+"xnxnxn");
            }
            return listOfPatientAnamnesis;
            


        }

        //public ObservableCollection<Anamnesis> FindAnamnesisByPatient (MedicalRecord med)
        //{
        //    ObservableCollection<Anamnesis> list = new ObservableCollection<Anamnesis>();
        //    foreach(Anamnesis anam in Anamnesis)
        //    {
        //        if (anam._MedicalRecord._Patient._Jmbg == med._Patient._Jmbg)
        //        {
        //            list.Add(anam);
        //        }
        //    }
        //    return list;
        //}
        public ref ObservableCollection<Anamnesis> ReadAll()
        {
            return ref _anamnesis;
        }

        public void LoadDataFromFiles()
        {
            _anamnesis = _anamnesisDataHandler.ReadAll();
        }

        public void LoadDataToFiles()
        {
            _anamnesisDataHandler.Write(_anamnesis);
        }



    }
}
