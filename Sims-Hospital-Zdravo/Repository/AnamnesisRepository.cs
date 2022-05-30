﻿using System;
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
            LoadDataFromFiles();

        }


        public void Create (Anamnesis anamnesis)
        {
            Anamnesis.Add(anamnesis);
            LoadDataToFiles();
        }

        public void Update (Anamnesis anamnesis)
        {
            foreach( Anamnesis anam in Anamnesis)
            {
                if (anam._Doctor == anamnesis._Doctor)
                {
                    anam._Diagnosis = anamnesis._Diagnosis;
                    anam._Anamensis = anamnesis._Anamensis;
                    LoadDataToFiles();
                    return;
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
        //public ObservableCollection<Anamnesis> FindAnamesisByPatient(int id)
        //{
        //    ObservableCollection<Anamnesis> listOfPatientAnamnesis = new ObservableCollection<Anamnesis>();

        //    foreach( Anamnesis anam in Anamnesis)
        //    {
        //        if(anam.pa)
        //    }


        //}
        
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
        public Anamnesis FindAnamnesisByAppointment(Appointment appointment)
        {
            foreach (Anamnesis anamnesis in Anamnesis) 
            { 
                if(anamnesis._MedicalRecord._Patient._Id == appointment._Patient._Id && anamnesis._Doctor._Id == appointment._Doctor._Id && anamnesis._TimeInterval.Start.Equals(appointment._Time.Start))
                {
                    return anamnesis;
                }
            }
            return null;
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
