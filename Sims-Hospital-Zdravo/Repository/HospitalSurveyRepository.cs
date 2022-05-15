using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class HospitalSurveyRepository
    {
        public HospitalSurveyDataHandler hospitalSurveyDataHandler;
        public List<HospitalSurvey> surveys;
        public HospitalSurveyRepository(HospitalSurveyDataHandler hospitalSurveyDataHandler) 
        {
            this.hospitalSurveyDataHandler = hospitalSurveyDataHandler;
            this.surveys = new List<HospitalSurvey>();
            LoadDataFromFile();
        }
        public void Create(HospitalSurvey hospitalSurvey)
        {
            surveys.Add(hospitalSurvey);
            LoadDataToFile();
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
