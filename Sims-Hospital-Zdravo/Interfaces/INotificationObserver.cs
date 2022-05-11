using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface INotificationObserver
    {
        void Notify(Notification notification);
    }
}