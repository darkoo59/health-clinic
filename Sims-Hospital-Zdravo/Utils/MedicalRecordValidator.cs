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
        private MedicalRecordService recordService;


        public MedicalRecordValidator(MedicalRecordService service)
        {
            this.recordService = service;
        }

        private void jmbgRightFormat(String jmbg)
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
            jmbgRightFormat(jmbg);
        }

        public void UpdateValidation(String jmbg)
        {
            jmbgRightFormat(jmbg);
        }
    }
}
