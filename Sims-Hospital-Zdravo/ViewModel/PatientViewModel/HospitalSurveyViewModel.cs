using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Sims_Hospital_Zdravo.ViewModel.PatientViewModel
{
    public class HospitalSurveyViewModel
    {
        public List<QuestionForSurvey> Questions { get; set; }
        public Frame Frame { get; set; }
        private SurveyController _surveyController;
        public HospitalSurveyViewModel(Frame frame) 
        {
            this.Frame = frame;
            _surveyController = new SurveyController();
            InitalizeList();
        }
        private void InitalizeList()
        {
            Questions = new List<QuestionForSurvey>();
            foreach (QuestionForSurvey questionForSurvey in _surveyController.GetHospitalQuestions())
            {
                SetRadioButtons(questionForSurvey);
                Questions.Add(questionForSurvey);
            }
        }
        private void SetRadioButtons(QuestionForSurvey questionForSurvey)
        {
            questionForSurvey.One = false;
            questionForSurvey.Two = false;
            questionForSurvey.Three = false;
            questionForSurvey.Four = false;
            questionForSurvey.Five = false;
        }
        private ICommand _clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                _clickCommand = new CommandHandler(() => MyAction(), true);
                return _clickCommand;
            }
        }

        public void MyAction()
        {
            List<QuestionAndRate> questionsAndRates = new List<QuestionAndRate>();
            foreach (QuestionForSurvey questionForSurvey in Questions)
            {
                questionsAndRates.Add(new QuestionAndRate(questionForSurvey.Id, questionForSurvey.Text, CheckIfComboBoxChecked(questionForSurvey)));
            }
            _surveyController.CreateHospitalSurvey(new HospitalSurvey(questionsAndRates));
            Frame.Content = new HomePatient(Frame);
        }
        private int CheckIfComboBoxChecked(QuestionForSurvey questionForSurvey)
        {
            if (questionForSurvey.One) return 1;
            if (questionForSurvey.Two) return 2;
            if (questionForSurvey.Three) return 3;
            if (questionForSurvey.Four) return 4;
            if (questionForSurvey.Five) return 5;
            return 0;
        }
    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
