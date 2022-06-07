using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Interfaces;

namespace Sims_Hospital_Zdravo.Repository
{
    public class AllergensRepository:IAllergensRepository
    {
        private AllergensDataHandler _allergensDataHandler;
        public Allergens _allergens;

        public AllergensRepository()
        {
            this._allergensDataHandler = new AllergensDataHandler();
            this._allergens = new Allergens();
            LoadDataFromFile();
        }

        public List<String> FindAll()
        {
            LoadDataFromFile();
            return _allergens.CommonAllergens.Concat(_allergens.MedicalAllergens).ToList();

        }

        public List<String> FindAllCommonAllergens()
        {
            LoadDataFromFile();
            return _allergens.CommonAllergens;
        }

        public List<String> FindAllMedicalAllergens()
        {
            LoadDataFromFile();
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
