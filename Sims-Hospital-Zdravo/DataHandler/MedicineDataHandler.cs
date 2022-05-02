using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    class MedicineDataHandler
    {
        public ObservableCollection<Medicine> ReadAll()
        {
            string medicineSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Medicine> medicines = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Medicine>>(medicineSerialized);
            return medicines;
        }

        public void Write(ObservableCollection<Medicine> medicines)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(medicines);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\medicines.txt";
    }
}
