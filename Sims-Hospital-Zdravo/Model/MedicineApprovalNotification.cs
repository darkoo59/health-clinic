namespace Sims_Hospital_Zdravo.Model
{
    public class MedicineApprovalNotification : Notification
    {
        private int doctorId;
        private int managerId;
        private Medicine medicine;

        public MedicineApprovalNotification(string content, int managerId, int doctorId, Medicine medicine, int id) : base(content, id)
        {
            this.doctorId = doctorId;
            this.managerId = managerId;
            this.medicine = medicine;
        }
    }
}