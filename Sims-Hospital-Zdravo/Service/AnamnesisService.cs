using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.Repository;
using Model;

namespace Sims_Hospital_Zdravo.Service
{
    public class AnamnesisService
    {
        private AnamnesisRepository anamnesisRepository;

        public AnamnesisService(AnamnesisRepository anamnesisRepository)
        {
            this.anamnesisRepository = anamnesisRepository;
        }

        public void Create(Anamnesis anamnesis)
        {
            anamnesisRepository.Create(anamnesis);
        }

        public void Update(Anamnesis anamnesis)
        {
            anamnesisRepository.Update(anamnesis);
        }

        public ref ObservableCollection<Anamnesis> ReadAll()
        {
            return ref anamnesisRepository.ReadAll();
        }

        public ObservableCollection<Anamnesis> findAnamnesisByDoctor(int id)
        {
            return anamnesisRepository.FindAnamnesisByDoctor(id);
        }
        public Anamnesis FindAnamnesisByAppointment(Appointment appointment)
        {
            return anamnesisRepository.FindAnamnesisByAppointment(appointment);
        }
    }
}
