using System;
using System.Collections.Generic;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IAllergensRepository:IGenericRepository<String>
    {
        List<String> FindAllCommonAllergens();
        List<String> FindAllMedicalAllergens();
    }
}