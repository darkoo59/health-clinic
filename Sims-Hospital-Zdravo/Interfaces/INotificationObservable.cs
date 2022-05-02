namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface INotificationObservable
    {
        void AddObserver(INotificationObserver observer);
        void RemoveObserver(INotificationObserver observer);
        void Notify(string description);
    }
}