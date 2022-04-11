/***********************************************************************
 * Module:  MedicalRecordsRepository.cs
 * Author:  Darko
 * Purpose: Definition of the Class Repository.MedicalRecordsRepository
 ***********************************************************************/

using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class MedicalRecordsRepository
    {
        public int Create(Model.MedicalRecord medicalRecord)
        {
            // TODO: implement
            return 0;
        }

        public MedicalRecord FindById(int id)
        {
            // TODO: implement
            return null;
        }

        public System.Collections.Generic.List<MedicalRecord> FindAll()
        {
            // TODO: implement
            return null;
        }

        public MedicalRecord Update(MedicalRecord medicalRecord)
        {
            // TODO: implement
            return null;
        }

        public void DeleteById(int id)
        {
            // TODO: implement
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            // TODO: implement
        }

        public List<MedicalRecord> FindByPatient(Patient patient)
        {
            // TODO: implement
            return null;
        }


        public System.Collections.ArrayList medicalRecords;
        public DataHandler.PatientDataHandler patientDataHandler;

    }
}