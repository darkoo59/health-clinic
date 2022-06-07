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
using System.Linq;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;

namespace Repository
{
    public class MedicalRecordsRepository:IMedicalRecordsRepository
    {
        public List<MedicalRecord> _medicalRecords;
        public DataHandler.MedicalRecordDataHandler _medicalRecordDataHandler;
        public List<Patient> _patients;

        public MedicalRecordsRepository(MedicalRecordDataHandler recordDataHandler)
        {
            _medicalRecords = new List<MedicalRecord>();
            _medicalRecordDataHandler = recordDataHandler;
            LoadDataFromFile();
        }

        public void Create(Model.MedicalRecord medicalRecord)
        {
            LoadDataFromFile();
            this._medicalRecords.Add(medicalRecord);
            LoadDataToFile();
        }

        public MedicalRecord FindById(int id)
        {
            LoadDataFromFile();
            return _medicalRecords.FirstOrDefault(record => record.Id == id);
        }

        public Patient FindPatientById(int id)
        {
            LoadDataFromFile();
            return (from medicalRecord in _medicalRecords where medicalRecord.Patient._Id == id select
                medicalRecord.Patient).FirstOrDefault();
        }

        public List<MedicalRecord> FindAll()
        {
            LoadDataFromFile();
            return _medicalRecords;
        }

        public void Update(MedicalRecord medicalRecord)
        {
            LoadDataFromFile();
            foreach (var record in _medicalRecords.Where(record => record.Id == medicalRecord.Id))
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

        public void DeleteById(int id)
        {
            LoadDataFromFile();
            foreach (var record in _medicalRecords.Where(record => record.Id == id))
            {
                _medicalRecords.Remove(record);
                LoadDataToFile();
                return;
            }
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            LoadDataFromFile();
            _medicalRecords.Remove(medicalRecord);
            LoadDataToFile();
        }

        public void AddNotes(Appointment appointment, Anamnesis anamnesis, string notes)
        {
            LoadDataFromFile();
            foreach (var ana in from medical in _medicalRecords where
                         appointment.Patient._Id == medical.Patient._Id from ana in medical.Anamnesis where
                         ana._TimeInterval.Start.Equals(anamnesis._TimeInterval.Start) select ana)
            {
                ana.Notes = notes;
                LoadDataToFile();
                return;
            }
        }
        public void AddStartDate(DateTime dateTime, Prescription prescription, Appointment appointment)
        {
            LoadDataFromFile();
            foreach (var pres in from medical in _medicalRecords where
                         appointment.Patient._Id == medical.Patient._Id from
                         pres in medical.Prescriptions where
                         pres.Doctor._Id == prescription.Doctor._Id &&
                         pres.TimeInterval.Equals(prescription.TimeInterval.Start) select pres)
            {
                pres.StartDate = dateTime;
                LoadDataToFile();
                return;
            }
        }
        public Anamnesis GetAnamnesis(Appointment appointment)
        {
            LoadDataFromFile();
            return _medicalRecords.Where(medicalRecord => medicalRecord.Patient._Id == appointment.Patient._Id)
                .SelectMany(medicalRecord => medicalRecord.Anamnesis).FirstOrDefault(anamnesis => anamnesis.Doctor._Id
                    == appointment.Doctor._Id && anamnesis._TimeInterval.Start.Equals(appointment.Time.Start));
        }
        public List<Prescription> GetPrescriptions(Appointment appointment)
        {
            LoadDataFromFile();
            List<Prescription> prescriptions = new List<Prescription>();
            foreach (var medicalRecord in _medicalRecords.Where(medicalRecord => medicalRecord.Patient._Id 
                         == appointment.Patient._Id))
            {
                prescriptions.AddRange(medicalRecord.Prescriptions.Where
                    (prescription => prescription.TimeInterval.Start.Equals(appointment.Time.Start)));
                break;
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