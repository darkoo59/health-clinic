using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IMeetingRepository:IGenericRepository<Meeting>
    {
        void Create(Meeting meeting);
        int GenerateId();
    }
}