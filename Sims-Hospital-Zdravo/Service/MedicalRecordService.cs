/***********************************************************************
 * Module:  MedicalRecordService.cs
 * Author:  Darko
 * Purpose: Definition of the Class Service.MedicalRecordService
 ***********************************************************************/

using Model;
using Repository;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Model;
using Sims_Hospital_Zdravo.Model;

namespace Service
{
    public class MedicalRecordService
    {

        private MedicalRecordsRepository _medicalRecordRepository;
        private PatientRepository _patientRepository;
        private AllergensRepository _allergensRepository;
        private MedicalRecordValidator _validator;

        public MedicalRecordService()
        {
            _medicalRecordRepository = new MedicalRecordsRepository();
            _patientRepository = new PatientRepository();
            _allergensRepository = new AllergensRepository();
            _validator = new MedicalRecordValidator(this);
        }
        public void Create(MedicalRecord medicalRecord, Patient patient)
        {
            // TODO: implement
            _validator.InsertValidation(patient.Jmbg);
            _medicalRecordRepository.Create(medicalRecord);
            _patientRepository.Create(patient);
            return;
        }

        public MedicalRecord FindById(int id)
        {
            return _medicalRecordRepository.FindById(id);
        }


        public List<MedicalRecord> ReadAll()
        {
            return _medicalRecordRepository.FindAll();
        }

        public void Update(MedicalRecord medicalRecord, Patient patient)
        {
            // TODO: implement
            _validator.UpdateValidation(patient.Jmbg);
            _patientRepository.Update(patient);
            _medicalRecordRepository.Update(medicalRecord);
        }

        public void DeleteById(int id)
        {
            _medicalRecordRepository.DeleteById(id);
        }

        public void Delete(MedicalRecord medicalRecord)
        {
            // TODO: implement
            _medicalRecordRepository.Delete(medicalRecord);
        }

        public Patient FindPatientById(int id)
        {
            return _medicalRecordRepository.FindPatientById(id);
        }

        public List<String> ReadAllCommonAllergens()
        {
            return _allergensRepository.FindAllCommonAllergens();
        }


        public List<String> ReadAllMedicalAllergens()
        {
            return _allergensRepository.FindAllMedicalAllergens();
        }

        public int GenerateId()
        {
            List<MedicalRecord> medicalRecords = _medicalRecordRepository.FindAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (MedicalRecord record in medicalRecords)
            {
                ids.Add(record.Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;

        }

        public int GenreatePatientId()
        {
            ObservableCollection<Patient> patients = _patientRepository.ReadAll();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (Patient patient in patients)
            {
                ids.Add(patient.Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
        }

        public ObservableCollection<Prescription> GetPrescriptionByMedicalRecord(MedicalRecord medicalRecord)
        {
            return _medicalRecordRepository.GetPrescriptionsByMedicalRecord(medicalRecord);
        }

        public Anamnesis GetAnamnesis(Appointment appointment)
        {
            return _medicalRecordRepository.GetAnamnesis(appointment);
        }
        public void AddNotes(Appointment appointment, Anamnesis anamnesis, string notes)
        {
            _medicalRecordRepository.AddNotes(appointment, anamnesis, notes);
        }
        public List<Prescription> GetPrescriptions(Appointment appointment)
        {
            return _medicalRecordRepository.GetPrescriptions(appointment);
        }
        public void AddStartDate(DateTime dateTime, Prescription prescription, Appointment appointment)
        {
            _medicalRecordRepository.AddStartDate(dateTime, prescription, appointment);
        }
   }
}