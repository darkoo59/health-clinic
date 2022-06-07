using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using Model;
using System;
using System.Collections.ObjectModel;

namespace Repository
{
    public class DoctorRepository
    {
        public DoctorDataHandler docHandler;
        public ObservableCollection<Doctor> doctors;

        public DoctorRepository()
        {
            this.docHandler = new DoctorDataHandler();
            this.doctors = new ObservableCollection<Doctor>();
            LoadDataFromFile();
        }

        public void Create(Doctor doct)
        {
            doctors.Add(doct);
            LoadDataToFile();
        }

        public void Delete(Doctor doc)
        {
            doctors.Remove(doc);
            LoadDataToFile();
        }

        public void Update(Doctor newDoc)
        {
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
            foreach (Doctor doc in doctors)
            {
                if (doc._Id == id) return doc;
            }

            return null;
        }

       

        public ObservableCollection<Doctor> FindDoctorsBySpecalty(SpecialtyType specalty)
        {
            
            ObservableCollection<Doctor> doctorss = new ObservableCollection<Doctor>();

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
            List<Doctor> docs = new List<Doctor>();
            foreach (Doctor doc in doctors)
            {
                Console.WriteLine(doc.Specialty);
                if (doc.Specialty == type)
                    docs.Add(doc);
            }

            return docs;
        }

        public ObservableCollection<Doctor> ReadAll()
        {
            return this.doctors;
        }

        public void LoadDataFromFile()
        {
            this.doctors = docHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            docHandler.Write(doctors);
        }
    }
}