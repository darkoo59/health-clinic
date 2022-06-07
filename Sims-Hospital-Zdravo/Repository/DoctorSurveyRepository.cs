using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Repository
{
    public class DoctorSurveyRepository
    {
        public DoctorSurveyDataHandler doctorSurveyDataHandler;
        public List<DoctorSurvey> surveys;

        public DoctorSurveyRepository(DoctorSurveyDataHandler doctorSurveyDataHandler)
        {
            this.doctorSurveyDataHandler = doctorSurveyDataHandler;
            this.surveys = new List<DoctorSurvey>();
            LoadDataFromFile();
        }

        public void Create(DoctorSurvey doctorSurvey)
        {
            doctorSurvey.Appointment.Rated = true;
            surveys.Add(doctorSurvey);
            LoadDataToFile();
        }

        public List<DoctorSurvey> ReadAll()
        {
            return surveys;
        }

        public List<DoctorSurvey> ReadAllByDoctorId(int id)
        {
            return surveys.FindAll(survey => survey.Appointment.Doctor.Id == id);
        }

        public void LoadDataFromFile()
        {
            surveys = doctorSurveyDataHandler.ReadAll();
        }

        public void LoadDataToFile()
        {
            doctorSurveyDataHandler.Write(surveys);
        }
    }
}