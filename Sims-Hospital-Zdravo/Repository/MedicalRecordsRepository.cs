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
using Sims_Hospital_Zdravo.Model;

namespace Repository
{
    public class MedicalRecordsRepository
    {
        public ObservableCollection<MedicalRecord> _medicalRecords;
        public DataHandler.MedicalRecordDataHandler _medicalRecordDataHandler;
        public ObservableCollection<Patient> _patients;

        public MedicalRecordsRepository(MedicalRecordDataHandler recordDataHandler)
        {
            _medicalRecords = new ObservableCollection<MedicalRecord>();
            _medicalRecordDataHandler = recordDataHandler;
            LoadDataFromFile();
        }

        public void Create(Model.MedicalRecord medicalRecord)
        {
            // TODO: implement
            this._medicalRecords.Add(medicalRecord);
            LoadDataToFile();
        }

        public MedicalRecord FindById(int id)
        {
            // TODO: implement
            foreach(MedicalRecord record in _medicalRecords)
            {
                if (record.Id == id)
                    return record;
            }
            return null;
        }

        public Patient FindPatientById(int id)
        {
            foreach(MedicalRecord medicalRecord in _medicalRecords)
            {
                if (medicalRecord.Patient._Id == id)
                    return medicalRecord.Patient;
            }
            return null;
        }

        public ref ObservableCollection<MedicalRecord> ReadAll()
        {
            return ref _medicalRecords;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            // TODO: implement
            foreach (MedicalRecord record in _medicalRecords)
            {
                if (record.Id == medicalRecord.Id)
                {
                    record.BloodType = medicalRecord.BloodType;
                    record.Gender = medicalRecord.Gender;
                    record.MaritalStatus = medicalRecord.MaritalStatus;
                    record.PatientAllergens = medicalRecord.PatientAllergens;
                    record.Patient = medicalRecord.Patient;
                    LoadDataToFile();
                    break;
                }
            }
            return;
        }

        public void DeleteById(int id)
        {
            // TODO: implement
            foreach(MedicalRecord record in _medicalRecords)
            {
                if(record.Id == id)
                {
                    _medicalRecords.Remove(record);
                    LoadDataToFile();
                    return;
                }
            }
            return;
                
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            // TODO: implement
            _medicalRecords.Remove(medicalRecord);
            LoadDataToFile();
        }

        public void AddNotes(Appointment appointment, Anamnesis anamnesis, string notes) 
        {
            foreach (MedicalRecord medical in _medicalRecords) 
            {
                if (appointment.Patient._Id == medical.Patient._Id) 
                {
                    foreach(Anamnesis ana in medical.Anamnesis)
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
            foreach (MedicalRecord medical in _medicalRecords)
            {
                if (appointment.Patient._Id == medical.Patient._Id)
                {
                    foreach (Prescription pres in medical.Prescriptions)
                    {
                        if (pres._Doctor._Id == prescription._Doctor._Id && pres.TimeInterval.Start.Equals(prescription.TimeInterval.Start))
                        {
                            pres.StartDate = dateTime;
                            LoadDataToFile();
                            return;
                        }
                    }
                }
            }
        }
        public Anamnesis GetAnamnesis(Appointment appointment)
        {
            foreach (MedicalRecord medicalRecord in _medicalRecords)
            {
                if (medicalRecord.Patient._Id == appointment.Patient._Id)
                {
                    foreach (Anamnesis anamnesis in medicalRecord.Anamnesis)
                    {
                        if (anamnesis.Doctor._Id == appointment.Doctor._Id && anamnesis._TimeInterval.Start.Equals(appointment.Time.Start))
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
            foreach (MedicalRecord medicalRecord in _medicalRecords)
            {
                if (medicalRecord.Patient._Id == appointment.Patient._Id)
                {
                    foreach (Prescription prescription in medicalRecord.Prescriptions)
                    {
                        if (prescription.TimeInterval.Start.Equals(appointment.Time.Start))
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
            _medicalRecords = _medicalRecordDataHandler.ReadAll();
        }

        

        public void LoadDataToFile()
        {
            _medicalRecordDataHandler.Write(_medicalRecords);
        }

        public ObservableCollection<Prescription> GetPrescriptionsByMedicalRecord(MedicalRecord medicalRecord)
        {
            return medicalRecord.Prescriptions;
        }
    }
}