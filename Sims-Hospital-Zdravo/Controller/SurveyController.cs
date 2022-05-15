using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Controller
{
    public class SurveyController
    {
        public SurveyService surveyService;
        public SurveyController(SurveyService surveyService)
        {
            this.surveyService = surveyService;
        }
        public void CreateDoctorSurvey(DoctorSurvey doctorSurvey) 
        {
            surveyService.CreateDoctorSurvey(doctorSurvey);        
        }
        public void CreateHospitalSurvey(HospitalSurvey hospitalSurvey) 
        {
            surveyService.CreateHospitalSurvey(hospitalSurvey);
        }
    }
}
