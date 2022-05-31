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
        private AllergensDataHandler _allergensDataHandler;
        public Allergens _allergens;

        public AllergensRepository(AllergensDataHandler dataHandler)
        {
            this._allergensDataHandler = dataHandler;
            this._allergens = new Allergens();
            LoadDataFromFile();
        }

        public Allergens ReadAll()
        {
            return this._allergens;

        }

        public List<String> ReadAllCommonAllergens()
        {
            return _allergens.CommonAllergens;
        }

        public List<String> ReadAllMedicalAllergens()
        {
            return _allergens.MedicalAllergens;
        }

        public void LoadDataFromFile()
        {
            this._allergens = _allergensDataHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            _allergensDataHandler.Write(_allergens);
        }

    }
}
