using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo.Model
{
    public class SurveyService
    {
        public DoctorSurveyRepository doctorSurveyRepository;
        public HospitalSurveyRepository hospitalSurveyRepository;

        public SurveyService(DoctorSurveyRepository doctorSurveyRepository, HospitalSurveyRepository hospitalSurveyRepository)
        {
            this.doctorSurveyRepository = doctorSurveyRepository;
            this.hospitalSurveyRepository = hospitalSurveyRepository;
        }
        public void CreateDoctorSurvey(DoctorSurvey doctorSurvey) 
        {
            doctorSurveyRepository.Create(doctorSurvey);
        }
        public void CreateHospitalSurvey(HospitalSurvey hospitalSurvey)
        {
            hospitalSurveyRepository.Create(hospitalSurvey);
        }
        
        
    }
}
