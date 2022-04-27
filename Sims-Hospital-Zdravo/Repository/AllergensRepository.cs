using Sims_Hospital_Zdravo.DataHandler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    class AllergensRepository
    {
        private AllergensDataHandler allergensDataHandler;
        public List<String> allergens;

        public AllergensRepository(AllergensDataHandler dataHandler)
        {
            this.allergensDataHandler = dataHandler;
            allergens = new List<string>();
            LoadDataFromFile();
        }

        public List<String> ReadAll()
        {
            return this.allergens;

        }

        public void LoadDataFromFile()
        {
            this.allergens = allergensDataHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            allergensDataHandler.Write(allergens);
        }

    }
}
