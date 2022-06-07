using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using Model;
using System;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Interfaces;

namespace Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        public DoctorDataHandler docHandler;
        public List<Doctor> doctors;

        public DoctorRepository()
        {
            this.docHandler = new DoctorDataHandler();
            this.doctors = new List<Doctor>();
            LoadDataFromFile();
        }

        public void Create(Doctor doct)
        {
            LoadDataFromFile();
            doctors.Add(doct);
            LoadDataToFile();
        }

        public void Delete(Doctor doc)
        {
            LoadDataFromFile();
            doctors.Remove(doc);
            LoadDataToFile();
        }

        public void Update(Doctor newDoc)
        {
            LoadDataFromFile();
            foreach (Doctor doc in doctors)
            {
                if (doc._Id == newDoc._Id)
                {
                    doc._Name = newDoc._Name;
                    doc._Email = newDoc._Email;
                    doc._BirthDate = newDoc._BirthDate;
                    doc._Address = newDoc._Address;
                    doc._Jmbg = newDoc._Jmbg;
                    doc._Password = newDoc._Password;
                    doc._Surname = newDoc._Surname;
                    doc._Username = newDoc._Username;
                    doc.Specialty = newDoc.Specialty;
                    LoadDataToFile();
                    return;
                }
            }
        }


        public Doctor FindDoctorById(int id)
        {
            LoadDataFromFile();
            foreach (Doctor doc in doctors)
            {
                if (doc._Id == id) return doc;
            }

            return null;
        }

       

        public List<Doctor> FindDoctorsBySpecalty(SpecialtyType specalty)
        {
            LoadDataFromFile();
            List<Doctor> doctorss = new List<Doctor>();

            foreach(Doctor doc in doctors.ToList())
            {
                
                if (doc.Specialty == specalty)
                {
                    doctorss.Add(doc);
                }
            }
            
            return doctorss;
            

        }
        public List<Doctor> FindDoctorsBySpeciality(SpecialtyType type)
        {
            LoadDataFromFile();
            List<Doctor> docs = new List<Doctor>();
            foreach (Doctor doc in doctors)
            {
                Console.WriteLine(doc.Specialty);
                if (doc.Specialty == type)
                    docs.Add(doc);
            }

            return docs;
        }

        public List<Doctor> FindAll()
        {
            LoadDataFromFile();
            return doctors;
        }

        public void LoadDataFromFile()
        {
            doctors = docHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            docHandler.Write(doctors);
        }
    }
}