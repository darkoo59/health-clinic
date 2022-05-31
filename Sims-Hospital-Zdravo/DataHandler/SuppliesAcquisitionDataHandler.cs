using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class SuppliesAcquisitionDataHandler
    {
        public ObservableCollection<SuppliesAcquisition> ReadAll()
        {
            string suppliesSerialized = System.IO.File.ReadAllText(_path);
            ObservableCollection<SuppliesAcquisition> supplies = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<SuppliesAcquisition>>(suppliesSerialized);
            return supplies;
        }

        public void Write(ObservableCollection<SuppliesAcquisition> supplies)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(supplies);
            System.IO.File.WriteAllText(_path, serialized);
        }

        private String _path = @"..\..\Resources\supplies_acquisition.txt";
    }
}
