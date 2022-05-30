/***********************************************************************
 * Module:  MedicalRecordsRepository.cs
 * Author:  Darko
 * Purpose: Definition of the Class Repository.MedicalRecordsRepository
 ***********************************************************************/

using DataHandler;
using Model;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Repository
{
    public class MedicalRecordsRepository
    {
        public ObservableCollection<MedicalRecord> medicalRecords;
        public DataHandler.MedicalRecordDataHandler medicalRecordDataHandler;
        public ObservableCollection<Patient> patients;

        public MedicalRecordsRepository(MedicalRecordDataHandler recordDataHandler)
        {
            medicalRecords = new ObservableCollection<MedicalRecord>();
            medicalRecordDataHandler = recordDataHandler;
            LoadDataFromFile();
        }

        public void Create(Model.MedicalRecord medicalRecord)
        {
            // TODO: implement
            this.medicalRecords.Add(medicalRecord);
            LoadDataToFile();
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

        public Patient FindPatientById(int id)
        {
            foreach(MedicalRecord medicalRecord in medicalRecords)
            {
                if (medicalRecord._Patient._Id == id)
                    return medicalRecord._Patient;
            }
            return null;
        }

        public ref ObservableCollection<MedicalRecord> ReadAll()
        {
            return ref medicalRecords;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            // TODO: implement
            foreach (MedicalRecord record in medicalRecords)
            {
                if (record._Id == medicalRecord._Id)
                {
                    record._BloodType = medicalRecord._BloodType;
                    record._Gender = medicalRecord._Gender;
                    record._MaritalStatus = medicalRecord._MaritalStatus;
                    record._PatientAllergens = medicalRecord._PatientAllergens;
                    record._Patient = medicalRecord._Patient;
                    LoadDataToFile();
                    break;
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
                    LoadDataToFile();
                    return;
                }
            }
            return;
                
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            // TODO: implement
            medicalRecords.Remove(medicalRecord);
            LoadDataToFile();
        }

        public void AddNotes(Appointment appointment, Anamnesis anamnesis, string notes) 
        {
            foreach (MedicalRecord medical in medicalRecords) 
            {
                if (appointment._Patient._Id == medical._Patient._Id) 
                {
                    foreach(Anamnesis ana in medical._Anamnesis)
                    {
                        if (ana._TimeInterval.Start.Equals(anamnesis._TimeInterval.Start)) 
                        {
                            ana.Notes = notes;
                            LoadDataToFile();
                            return;
                        }
                    }
                }
            }
        }
        public void AddStartDate(DateTime dateTime, Prescription prescription, Appointment appointment)
        {
            foreach (MedicalRecord medical in medicalRecords)
            {
                if (appointment._Patient._Id == medical._Patient._Id)
                {
                    foreach (Prescription pres in medical._Prescriptions)
                    {
                        if (pres._Doctor._Id == prescription._Doctor._Id && pres._TimeInterval.Start.Equals(prescription._TimeInterval.Start))
                        {
                            pres._StartDate = dateTime;
                            LoadDataToFile();
                            return;
                        }
                    }
                }
            }
        }
        public Anamnesis GetAnamnesis(Appointment appointment)
        {
            foreach (MedicalRecord medicalRecord in medicalRecords)
            {
                if (medicalRecord._Patient._Id == appointment._Patient._Id)
                {
                    foreach (Anamnesis anamnesis in medicalRecord._Anamnesis)
                    {
                        if (anamnesis._Doctor._Id == appointment._Doctor._Id && anamnesis._TimeInterval.Start.Equals(appointment._Time.Start))
                        {
                            return anamnesis;
                        }
                    }
                }
            }
            return null;
        }
        public ObservableCollection<Prescription> GetPrescriptions(Appointment appointment)
        {
            ObservableCollection<Prescription> prescriptions = new ObservableCollection<Prescription>();
            foreach (MedicalRecord medicalRecord in medicalRecords)
            {
                if (medicalRecord._Patient._Id == appointment._Patient._Id)
                {
                    foreach (Prescription prescription in medicalRecord._Prescriptions)
                    {
                        if (prescription._TimeInterval.Start.Equals(appointment._Time.Start))
                        {
                            prescriptions.Add(prescription);
                        }
                    }
                    break;
                }
            }
            return prescriptions;
        }
        public void LoadDataFromFile()
        {
            medicalRecords = medicalRecordDataHandler.ReadAll();
        }

        

        public void LoadDataToFile()
        {
            medicalRecordDataHandler.Write(medicalRecords);
        }
    }
}