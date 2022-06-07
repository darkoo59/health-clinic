using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class MedicineDataHandler
    {
        public List<Medicine> ReadAll()
        {
            string medicineSerialized = System.IO.File.ReadAllText(Path);
            List<Medicine> medicines = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Medicine>>(medicineSerialized);
            return medicines;
        }

        public void Write(List<Medicine> medicines)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(medicines);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\medicines.txt";
    }
}