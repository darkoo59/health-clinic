using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace Sims_Hospital_Zdravo.Repository
{
    
    internal class DoctorRepository
    {
        public DoctorDataHandler docHandler;
        public ObservableCollection<Doctor> doctors;

        public DoctorRepository(DoctorDataHandler docHandler)
        {
            this.docHandler = docHandler;
            this.doctors = new ObservableCollection<Doctor>();
        }

        public  void Create( Doctor doct)
        {

        }

        public void Delete(Doctor doc)
        {

        }

        public void Update (Doctor doc)
        {

        }

        public ref ObservableCollection <Doctor> ReadAll()
        {
            return ref this.doctors;

        }
         public void LoadData()
        {
            this.doctors = docHandler.ReadAll();
        }


    }
}
