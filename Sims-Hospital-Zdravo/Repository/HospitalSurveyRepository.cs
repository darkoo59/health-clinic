using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class HospitalSurveyRepository: IHospitalSurveyRepository
    {
        public HospitalSurveyDataHandler hospitalSurveyDataHandler;
        public List<HospitalSurvey> surveys;

        public HospitalSurveyRepository()
        {
            this.hospitalSurveyDataHandler = new HospitalSurveyDataHandler();
            this.surveys = new List<HospitalSurvey>();
            LoadDataFromFile();
        }

        public void Create(HospitalSurvey hospitalSurvey)
        {
            LoadDataFromFile();
            surveys.Add(hospitalSurvey);
            LoadDataToFile();
        }

        public List<HospitalSurvey> FindAll()
        {
            LoadDataFromFile();
            return surveys;
        }

        public void LoadDataFromFile()
        {
            surveys = hospitalSurveyDataHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            hospitalSurveyDataHandler.Write(surveys);
        }
    }
}