using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sims_Hospital_Zdravo.Utils
{
    public class MedicalRecordValidator
    {
        private MedicalRecordService _recordService;


        public MedicalRecordValidator(MedicalRecordService service)
        {
            this._recordService = service;
        }

        private void JmbgRightFormat(String jmbg)
        {
            if(jmbg.Length != 13)
            {
                throw new Exception("JMBG must have 13 numbers!");
            }
            else
            {
                foreach (char c in jmbg)
                {
                    if (c < '0' || c > '9')
                        throw new Exception("JMBG can have numbers only!");
                }
            }
        }


        public void InsertValidation(String jmbg)
        {
            JmbgRightFormat(jmbg);
        }

        public void UpdateValidation(String jmbg)
        {
            JmbgRightFormat(jmbg);
        }
    }
}
