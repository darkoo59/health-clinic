using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Controller;

namespace Sims_Hospital_Zdravo.ViewModel.Doctor
{
    public class LabaratoryTestViewModel
    {

        private ObservableCollection<LabaratoryTest> _labaratoryTests;

        public LabaratoryTestViewModel()
        {
            LoadTests();
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
