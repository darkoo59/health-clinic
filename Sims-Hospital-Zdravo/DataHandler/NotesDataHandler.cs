using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.DataHandler
{
    public class NotesDataHandler
    {
        public List<Notes> ReadAll()
        {
            string noteSerialized = System.IO.File.ReadAllText(Path);
            List<Notes> notes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Notes>>(noteSerialized);
            return notes;
        }

        public void Write(List<Notes> accounts)
        {
            string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(accounts);
            System.IO.File.WriteAllText(Path, serialized);
        }

        private String Path = @"..\..\Resources\notes.txt";
    }
}
