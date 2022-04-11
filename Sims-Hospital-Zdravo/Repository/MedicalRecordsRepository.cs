/***********************************************************************
 * Module:  MedicalRecordsRepository.cs
 * Author:  Darko
 * Purpose: Definition of the Class Repository.MedicalRecordsRepository
 ***********************************************************************/

using DataHandler;
using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class MedicalRecordsRepository
    {
        public MedicalRecordsRepository(PatientDataHandler patientHandler)
        {
            patientDataHandler = patientHandler;
        }

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

        //dodato
        public System.Collections.Generic.List<Patient> ReadAll()
        {
            return patientDataHandler.ReadAll();
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

        public DataHandler.AppointmentDataHandler appointmentDataHandler;
        public System.Collections.ArrayList medicalRecord;

        /// <pdGenerated>default getter</pdGenerated>
        public System.Collections.ArrayList GetMedicalRecord()
        {
            if (medicalRecord == null)
                medicalRecord = new System.Collections.ArrayList();
            return medicalRecord;
        }

        /// <pdGenerated>default setter</pdGenerated>
        public void SetMedicalRecord(System.Collections.ArrayList newMedicalRecord)
        {
            RemoveAllMedicalRecord();
            foreach (Model.MedicalRecord oMedicalRecord in newMedicalRecord)
                AddMedicalRecord(oMedicalRecord);
        }

        /// <pdGenerated>default Add</pdGenerated>
        public void AddMedicalRecord(Model.MedicalRecord newMedicalRecord)
        {
            if (newMedicalRecord == null)
                return;
            if (this.medicalRecord == null)
                this.medicalRecord = new System.Collections.ArrayList();
            if (!this.medicalRecord.Contains(newMedicalRecord))
                this.medicalRecord.Add(newMedicalRecord);
        }

        /// <pdGenerated>default Remove</pdGenerated>
        public void RemoveMedicalRecord(Model.MedicalRecord oldMedicalRecord)
        {
            if (oldMedicalRecord == null)
                return;
            if (this.medicalRecord != null)
                if (this.medicalRecord.Contains(oldMedicalRecord))
                    this.medicalRecord.Remove(oldMedicalRecord);
        }

        /// <pdGenerated>default removeAll</pdGenerated>
        public void RemoveAllMedicalRecord()
        {
            if (medicalRecord != null)
                medicalRecord.Clear();
        }
        public DataHandler.PatientDataHandler patientDataHandler;
    }
}