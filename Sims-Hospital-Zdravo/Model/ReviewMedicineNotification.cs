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
        private string reasonForNotValidating;
        private MedicineValidationType validationType;
        private Medicine medicine;

        public ReviewMedicineNotification(string content, string reasonForNotValidating, Medicine medicine, int id, MedicineValidationType medicineValidationType) : base(content, id)
        {
            this.reasonForNotValidating = reasonForNotValidating;
            this.validationType = medicineValidationType;
            this.medicine = medicine;
        }

        public string _ReasonForNotValidating
        {
            get { return reasonForNotValidating; }

            set { reasonForNotValidating = value; }
        }

        public Medicine _Medicine
        {
            get { return medicine; }

            set { medicine = value; }
        }

        public MedicineValidationType _ValidationType
        {
            get { return validationType; }

            set { validationType = value; }
        }
    }
}