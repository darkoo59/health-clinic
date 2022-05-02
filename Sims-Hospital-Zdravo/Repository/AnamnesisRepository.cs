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
        private AnamnesisDataHandler AnamnesisDataHandler;
        private ObservableCollection<Anamnesis> Anamnesis;
        public AnamnesisRepository(AnamnesisDataHandler anamnesisDataHandler)
        {

            this.AnamnesisDataHandler = anamnesisDataHandler;
            Anamnesis = new ObservableCollection<Anamnesis>();
        }


        public void Create (Anamnesis anamnesis)
        {
            Anamnesis.Add(anamnesis);
        }

        public void Update (Anamnesis anamnesis)
        {
            foreach( Anamnesis anam in Anamnesis)
            {
                if (anam._AnamnesisID == anamnesis._AnamnesisID)
                {
                    anam._Diagnosis = anamnesis._Diagnosis;
                    anam._Anamensis = anamnesis._Anamensis;
                    
                }
            }

        }
        public ObservableCollection<Anamnesis> FindAnamnesisByDoctor(int id)
        {
            ObservableCollection<Anamnesis> listOfAnamnesisByDoctor = new ObservableCollection<Anamnesis>();
            
            foreach(Anamnesis anam in Anamnesis)
            {
                if(anam._Doctor._Id == id)
                {
                    listOfAnamnesisByDoctor.Add(anam);
                }
            }
            return listOfAnamnesisByDoctor;
            
            
        }
        
        public ObservableCollection<Anamnesis> FindAnamnesisByPatient (MedicalRecord med)
        {
            ObservableCollection<Anamnesis> list = new ObservableCollection<Anamnesis>();
            foreach(Anamnesis anam in Anamnesis)
            {
                if (anam._MedicalRecord._Patient._Jmbg == med._Patient._Jmbg)
                {
                    list.Add(anam);
                }
            }
            return list;
        }
        public ref ObservableCollection<Anamnesis> ReadAll()
        {
            return ref Anamnesis;
        }

        public void LoadDataFromFiles()
        {
            Anamnesis = AnamnesisDataHandler.ReadAll();
        }

        public void LoadDataToFiles()
        {
            AnamnesisDataHandler.Write(Anamnesis);
        }



    }
}
