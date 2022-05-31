using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum MedicineValidationType
{
    MEDICINE_APPROVED,
    MEDICINE_REJECTED
}

namespace Sims_Hospital_Zdravo.Model
{
    public class ReviewMedicineNotification : Notification
    {
        private string _reasonForNotValidating;
        private MedicineValidationType _validationType;
        private Medicine _medicine;

        public ReviewMedicineNotification(string content, string reasonForNotValidating, Medicine medicine, int id, MedicineValidationType medicineValidationType) : base(content, id)
        {
            this._reasonForNotValidating = reasonForNotValidating;
            this._validationType = medicineValidationType;
            this._medicine = medicine;
        }

        public string ReasonForNotValidating
        {
            get { return _reasonForNotValidating; }

            set { _reasonForNotValidating = value; }
        }

        public Medicine Medicine
        {
            get { return _medicine; }

            set { _medicine = value; }
        }

        public MedicineValidationType ValidationType
        {
            get { return _validationType; }

            set { _validationType = value; }
        }
    }
}