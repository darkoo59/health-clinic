using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        void Create(Doctor doct);

        void Delete(Doctor doc);
        void Update(Doctor newDoc);
        Doctor FindDoctorById(int id);
        List<Doctor> FindDoctorsBySpecalty(SpecialtyType specalty);
        List<Doctor> FindDoctorsBySpeciality(SpecialtyType type);
    }
}
