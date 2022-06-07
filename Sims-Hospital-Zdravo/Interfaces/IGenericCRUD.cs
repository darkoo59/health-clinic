namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IGenericCRUD<T> : IGenericRepository<T>
    {
        void Create(T obj);
        void Delete(T obj);
        void Update(T obj);
        void DeleteById(int id);
    }
}