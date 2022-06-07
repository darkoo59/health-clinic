using System;
using System.Windows.Input;
using Model;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo.ViewModel.Commands
{
    public class CreatePdfCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Doctor doctor = parameter as Doctor;
            if (doctor == null)
            {
                HospitalSurveyPdfCreator pdfCreator = new HospitalSurveyPdfCreator();
                pdfCreator.PrintSurvey();
            }
            else
            {
                Console.WriteLine(doctor);
                DoctorSurveyPdfCreator pdfCreator = new DoctorSurveyPdfCreator(doctor);
                pdfCreator.PrintSurvey();
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}