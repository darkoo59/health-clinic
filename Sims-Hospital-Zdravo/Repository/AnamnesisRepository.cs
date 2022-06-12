using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Repository
{
    public class AnamnesisRepository : IAnamnesisRepository
    {
        private AnamnesisDataHandler _anamnesisDataHandler;
        private List<Anamnesis> _anamnesis;

        public AnamnesisRepository()
        {
            this._anamnesisDataHandler = new AnamnesisDataHandler();
            _anamnesis = new List<Anamnesis>();
            
        }


        public void Create(Anamnesis anamnesis)
        {
            LoadDataFromFiles();
            _anamnesis.Add(anamnesis);
            LoadDataToFiles();
        }

        public void Update(Anamnesis anamnesis)
        {
            LoadDataFromFiles();
            foreach (Anamnesis anam in _anamnesis)
            {
                if (anam.Doctor == anamnesis.Doctor)
                {
                    anam.Diagnosis = anamnesis.Diagnosis;
                    anam.Anamensis = anamnesis.Anamensis;
                    LoadDataToFiles();
                    return;
                }
            }
        }

        public List<Anamnesis> FindAnamnesisByDoctor(int id)
        {
            List<Anamnesis> listOfAnamnesisByDoctor = new List<Anamnesis>();

            foreach (Anamnesis anam in _anamnesis)
            {
                if (anam.Doctor.Id == id)
                {
                    listOfAnamnesisByDoctor.Add(anam);
                }
            }

            return listOfAnamnesisByDoctor;
        }

        public List<Anamnesis> FindAnamesisByPatient(MedicalRecord medicalRecord)
        {
            List<Anamnesis> listOfPatientAnamnesis = medicalRecord.Anamnesis;
            foreach (Anamnesis anma in listOfPatientAnamnesis)
            {
                Console.WriteLine(anma.Diagnosis + "xnxnxn");
            }

            return listOfPatientAnamnesis;
        }

        
        public  List<Anamnesis> FindAll()
        {
            return  _anamnesisDataHandler.ReadAll();
        }

        public void LoadDataFromFiles()
        {
            _anamnesis = _anamnesisDataHandler.ReadAll();
        }

        public void LoadDataToFiles()
        {
            _anamnesisDataHandler.Write(_anamnesis);
        }

        public void Delete(Anamnesis obj)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}