using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class AllergensRepository
    {
        private AllergensDataHandler allergensDataHandler;
        public Allergens allergens;

        public AllergensRepository(AllergensDataHandler dataHandler)
        {
            this.allergensDataHandler = dataHandler;
            this.allergens = new Allergens();
            LoadDataFromFile();
        }

        public Allergens ReadAll()
        {
            return this.allergens;

        }

        public List<String> ReadAllCommonAllergens()
        {
            return allergens._Allergens;
        }

        public List<String> ReadAllMedicalAllergens()
        {
            return allergens._MedicalAllergens;
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
