using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class AnamnesisDataHandler
    {
        public ObservableCollection<Anamnesis> ReadAll()
        {
            string anamnesisSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<Anamnesis> anamnesis = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Anamnesis>>(anamnesisSerialized);
            return anamnesis;
        }

        public void Write(ObservableCollection<Anamnesis> anamnesis)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(anamnesis);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\anamnesis.txt";




    }
}
