using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims_Hospital_Zdravo.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
        List<Appointment> FindByDoctorId(int id);
        List<Appointment> FindByDoctorSpecialityBeforeDate(SpecialtyType type, DateTime endDate);
        List<Appointment> FindByPatientId(int id);
        Appointment GetByID(Appointment app);
        List<TimeInterval> GetTimeIntervalsForRoom(Room room);
        List<TimeInterval> GetTimeIntervalsForRoom(int roomId);
        List<TimeInterval> GetTimeIntervalsForDoctor(int id);
        List<Appointment> FindByRoomId(int roomId);
        List<TimeInterval> GetTimeIntervalsForDoctor(Doctor doctor);
        List<Appointment> ReadAllAppointmentsForDate(DateTime date);
        void SetAppointmentRated(Appointment appointment);
    }
}
