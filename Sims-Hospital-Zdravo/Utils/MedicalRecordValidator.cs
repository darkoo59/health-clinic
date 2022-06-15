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
        private App app;


        public MedicalRecordValidator(MedicalRecordService service)
        {
            app = Application.Current as App;
            this._recordService = service;
        }

        private void JmbgRightFormat(String jmbg)
        {
            if(jmbg.Length != 13)
            {
                if(app._currentLanguage.Equals("en-US"))
                    throw new Exception("JMBG must have 13 numbers!");
                else
                    throw new Exception("JMBG treba da ima 13 brojeva!");
            }
            else
            {
                foreach (char c in jmbg)
                {
                    if (c < '0' || c > '9')
                    {
                        if(app._currentLanguage.Equals("en-US"))
                            throw new Exception("JMBG can have numbers only!");
                        else 
                            throw new Exception("JMBG treba da ima samo brojeve!");
                    }
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
