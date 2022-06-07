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

        public MedicalRecordService(MedicalRecordsRepository medicalRepo, PatientRepository patientRepo, AllergensRepository alergRepo)
        {
            _medicalRecordRepository = medicalRepo;
            _patientRepository = patientRepo;
            _allergensRepository = alergRepo;
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
            // TODO: implement
            return _medicalRecordRepository.FindById(id);
        }


        public ref ObservableCollection<MedicalRecord> ReadAll()
        {
            // TODO: implement
            return ref _medicalRecordRepository.ReadAll();
        }

        public void Update(MedicalRecord medicalRecord, Patient patient)
        {
            // TODO: implement
            _validator.UpdateValidation(patient.Jmbg);
            _patientRepository.Update(patient);
            _medicalRecordRepository.Update(medicalRecord);
            return;
        }

        public void DeleteById(int id)
        {
            // TODO: implement
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
            return _allergensRepository.ReadAllCommonAllergens();
        }


        public List<String> ReadAllMedicalAllergens()
        {
            return _allergensRepository.ReadAllMedicalAllergens();
        }

        public int GenerateId()
        {
            ObservableCollection<MedicalRecord> medicalRecords = _medicalRecordRepository.ReadAll();
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
        public ObservableCollection<Prescription> GetPrescriptions(Appointment appointment)
        {
            return _medicalRecordRepository.GetPrescriptions(appointment);
        }
        public void AddStartDate(DateTime dateTime, Prescription prescription, Appointment appointment)
        {
            _medicalRecordRepository.AddStartDate(dateTime, prescription, appointment);
        }
   }
}