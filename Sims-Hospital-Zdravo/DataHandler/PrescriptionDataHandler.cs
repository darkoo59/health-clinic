using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class PrescriptionDataHandler
    {
        public ObservableCollection<Prescription> ReadAll()
        {
            string prescriptionSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Prescription> prescriptions = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Prescription>>(prescriptionSerialized);
            return prescriptions;
        }

        public void Write(ObservableCollection<Prescription> prescriptions)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(prescriptions);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\prescriptions.txt";
    }
}
