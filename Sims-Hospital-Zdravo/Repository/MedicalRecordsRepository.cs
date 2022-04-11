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
        public MedicalRecordsRepository(PatientDataHandler patientHandler, MedicalRecordDataHandler recordDataHandler)
        {
            patientDataHandler = patientHandler;
            medicalRecordDataHandler = recordDataHandler;
        }

        public void Create(Model.MedicalRecord medicalRecord, Patient patient)
        {
            // TODO: implement
            this.medicalRecords.Add(medicalRecord);
            this.patients.Add(patient);
        }

        public MedicalRecord FindById(int id)
        {
            // TODO: implement
            foreach(MedicalRecord record in medicalRecords)
            {
                if (record._Id == id)
                    return record;
            }
            return null;
        }

        //dodato
        public System.Collections.Generic.List<MedicalRecord> ReadAll()
        {
            return medicalRecords;
        }

        public System.Collections.Generic.List<Patient> ReadAllPatients()
        {
            return patients;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            // TODO: implement
            foreach(MedicalRecord record in medicalRecords)
            {
                if(record._Id == medicalRecord._Id)
                {
                    medicalRecords.Remove(record);
                    medicalRecords.Add(medicalRecord);
                    return;
                }
            }
            return;
        }

        public void DeleteById(int id)
        {
            // TODO: implement
            foreach(MedicalRecord record in medicalRecords)
            {
                if(record._Id == id)
                {
                    medicalRecords.Remove(record);
                    return;
                }
            }
            return;
                
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            // TODO: implement
            medicalRecords.Remove(medicalRecord);
        }

        public MedicalRecord FindByPatient(Patient patient)
        {
            // TODO: implement
            foreach (MedicalRecord record in medicalRecords)
            {
                if (record._Patient._Id == patient._Id)
                {
                    return record;
                }
            }
            return null;
        }


        public List<MedicalRecord> medicalRecords;
        public List<Patient> patients;
        public DataHandler.PatientDataHandler patientDataHandler;
        public DataHandler.MedicalRecordDataHandler medicalRecordDataHandler;

        public void loadData()
        {
            this.medicalRecords = medicalRecordDataHandler.ReadAll();
            this.patients = patientDataHandler.ReadAll();
            foreach(MedicalRecord medicalRecord in medicalRecords)
            {
                foreach(Patient patient in patients)
                {
                    if(medicalRecord._PatientId == patient._Id)
                    {
                        medicalRecord._Patient = patient;
                        break;
                    }
                } 
            }
        }
    }
}