using System;
using System.Collections.Generic;
using Model;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IMedicalRecordsRepository:IGenericCRUD<MedicalRecord>
    {
        MedicalRecord FindById(int id);
        Patient FindPatientById(int id);
        void AddNotes(Appointment appointment, Anamnesis anamnesis, string notes);
        void AddStartDate(DateTime dateTime, Prescription prescription, Appointment appointment);
        Anamnesis GetAnamnesis(Appointment appointment);
        List<Prescription> GetPrescriptions(Appointment appointment);
    }
}