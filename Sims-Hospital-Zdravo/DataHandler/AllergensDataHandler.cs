using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class AllergensDataHandler
    {
        public List<String> ReadAll()
        {
            string allergensSerialized = System.IO.File.ReadAllText(Path);
            List<String> allergens = Newtonsoft.Json.JsonConvert.DeserializeObject<List<String>>(allergensSerialized);
            return allergens;
        }

        public void Write(List<String> allergens)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(allergens);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\allergens.txt";
    }
}
