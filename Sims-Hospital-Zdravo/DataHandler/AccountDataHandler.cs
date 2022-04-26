using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class AccountDataHandler
    {
        public ObservableCollection<User> ReadAll()
        {
            string accountSerialized = System.IO.File.ReadAllText(Path);
            ObservableCollection<User> accounts = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<User>>(accountSerialized);
            return accounts;
        }

        public void Write(ObservableCollection<User> accounts)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(accounts);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\accounts.txt";
    }
}
