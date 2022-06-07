/***********************************************************************
 * Module:  MedicalRecordController.cs
 * Author:  Darko
 * Purpose: Definition of the Class Controller.MedicalRecordController
 ***********************************************************************/

using Model;
using Service;
using Sims_Hospital_Zdravo.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;

namespace Controller
{
   public class MedicalRecordController
   {

        public Service.MedicalRecordService medicalRecordService;
        public MedicalRecordValidator validator;
        public PrescriptionService prescriptionService;
        public MedicalRecordController()
        {
            medicalRecordService = new MedicalRecordService();
            validator = new MedicalRecordValidator(medicalRecordService);
            this.prescriptionService = new PrescriptionService();
        }

        public void Create(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            medicalRecordService.Create(medicalRecord, patient);
        }
      
      public MedicalRecord FindById(int id)
      {
         // TODO: implement
         return medicalRecordService.FindById(id);
      }

      public Patient FindPatientById(int id)
        {
            return medicalRecordService.FindPatientById(id);
        }


      public List<MedicalRecord> ReadAll()
      {
         // TODO: implement
         return medicalRecordService.ReadAll();
      }
      
      public void Update(MedicalRecord medicalRecord, Patient patient)
      {
            // TODO: implement
            medicalRecordService.Update(medicalRecord, patient);
      }
      
      public void DeleteById(int id)
      {
            // TODO: implement
            medicalRecordService.DeleteById(id);
      }
      
      public void Delete(MedicalRecord medicalRecord)
      {
            // TODO: implement
            medicalRecordService.Delete(medicalRecord);
      }

        public int GenerateId()
        {
           return  medicalRecordService.GenerateId();
        }

        public int GeneratePatientId()
        {
            return medicalRecordService.GenreatePatientId();
        }

        public List<String> ReadAllCommonAllergens()
        {
            return medicalRecordService.ReadAllCommonAllergens();
        }

        public List<String> ReadAllMedicalAllergens()
        {
            return medicalRecordService.ReadAllMedicalAllergens();
        }

        public ObservableCollection<Prescription> ReadAllPrescriptions()
        {
            return prescriptionService.ReadAll();
        }

        public void createPrescription( Prescription prescription)
        {
            prescriptionService.Create(prescription);
        }

        public ObservableCollection<Prescription> GetPrescriptionsByMedicalRecord (MedicalRecord medicalRecord)
        {
            return medicalRecordService.GetPrescriptionByMedicalRecord(medicalRecord);
        }
        public Anamnesis GetAnamnesis(Appointment appointment) 
        {
            return medicalRecordService.GetAnamnesis(appointment);
        }

        public void AddNotes(Appointment appointment, Anamnesis anamnesis, string notes)
        {
            medicalRecordService.AddNotes(appointment, anamnesis, notes);
        }

        public List<Prescription> GetPrescriptions(Appointment appointment) 
        {
            return medicalRecordService.GetPrescriptions(appointment);
        }

        public void AddStartDate(DateTime dateTime, Prescription prescription, Appointment appointment) 
        {
            medicalRecordService.AddStartDate(dateTime, prescription, appointment);
        }
    }
}