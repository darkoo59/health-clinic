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
        public Allergens ReadAll()
        {
            string allergensSerialized = System.IO.File.ReadAllText(_path);
            Allergens allergens = Newtonsoft.Json.JsonConvert.DeserializeObject<Allergens>(allergensSerialized);
            return allergens;
            //return null;
            
        }

        public void Write(Allergens allergens)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(allergens);
            System.IO.File.WriteAllText(_path, serialized);
        }

        private String _path = @"..\..\Resources\allergens.txt";
    }
}
