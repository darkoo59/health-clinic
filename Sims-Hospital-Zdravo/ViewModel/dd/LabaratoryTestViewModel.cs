using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Controller;
using Model;
using Controller;


namespace Sims_Hospital_Zdravo.ViewModel
{
    public class LabaratoryTestViewModel
    {

        private ObservableCollection<LabaratoryTest> _labaratoryTests;
        private ObservableCollection<Patient> _patients;
        private DoctorAppointmentController _doctorAppointmentController;
        private List<string> _tests;

        public LabaratoryTestViewModel()
        {
            this._doctorAppointmentController = new DoctorAppointmentController();
            this._patients = _doctorAppointmentController.GetPatients();
            LoadLabaratoryTests();
            LoadTests();
        }


        public List<string> Tests
        {
            get
            {
                return _tests;
            }
            set
            {
                _tests = value;
            }
        }
        public ObservableCollection<LabaratoryTest> LabaratoryTests
        {
            get
            {
                return _labaratoryTests;
            }
            set
            {
                _labaratoryTests = value;
            }
        }
        public ObservableCollection<Patient> Patients
        {
            get { return _patients; }
        }

        public void LoadLabaratoryTests()
        {
            List<string> tes = new List<string>();
            tes.Add("blood test");
            tes.Add("urine test");
            tes.Add("CRP");
            tes.Add("leukocytes");
            Tests = tes;

        }
        public void LoadTests()
        {
            ObservableCollection<LabaratoryTest> tests = new ObservableCollection<LabaratoryTest>();
            tests.Add(new LabaratoryTest("Marko Markovic", "diagnois", DateTime.Now));
            tests.Add(new LabaratoryTest("Marko Markovic", "diagnois", DateTime.Now));
            tests.Add(new LabaratoryTest("Marko Markovic", "diagnois", DateTime.Now));
            LabaratoryTests = tests;
        }

    }
}
