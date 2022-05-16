namespace Sims_Hospital_Zdravo.Model
{
    public class MedicineCreatedNotification : Notification
    {
        private int _doctorId;
        private Medicine _medicine;

        public MedicineCreatedNotification(string content, int doctorId, Medicine medicine, int id) : base(content, id)
        {
            this._doctorId = doctorId;
            this._medicine = medicine;
        }


        public int DoctorId
        {
            get { return _doctorId; }
            set { _doctorId = value; }
        }

        public Medicine Medicine
        {
            get { return _medicine; }

            set { _medicine = value; }
        }
    }
}