using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Service;
using Controller;
using Repository;
using Model;
using DataHandler;
using Sims_Hospital_Zdravo.Repository;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal RoomController roomController;
        internal MedicalRecordController recordController;
        internal AppointmentPatientController appointmentPatientController;
        internal DoctorAppointmentController doctorAppointmentController;

        public App() 
        {


            RoomDataHandler roomDataHandler = new RoomDataHandler();
            RoomRepository roomRepository = new RoomRepository(roomDataHandler);
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientDataHandler patientDataHandler = new PatientDataHandler();


            MedicalRecordDataHandler medicalRecordDataHandler = new MedicalRecordDataHandler();
            MedicalRecordsRepository medicalRepo = new MedicalRecordsRepository(patientDataHandler, medicalRecordDataHandler);
            MedicalRecordService recordService = new MedicalRecordService(medicalRepo);
            recordController = new MedicalRecordController(recordService);

            AppointmentDataHandler appointmentDataHandler = new AppointmentDataHandler();
            DoctorDataHandler doctorDataHandler = new DoctorDataHandler();
            DoctorRepository doctorRepository = new DoctorRepository(doctorDataHandler);
            AppointmentRepository appointmentRepository = new AppointmentRepository(appointmentDataHandler);
            AppointmentPatientService appointmentPatientService = new AppointmentPatientService(appointmentRepository,doctorRepository);
            appointmentPatientController = new AppointmentPatientController(appointmentPatientService);


            //DoctorAppointmentRepository doctorAppointmentRepository = new DoctorAppointmentRepository(patientDataHandler, appointmentDataHandler);
            PatientRepository patientRepository = new PatientRepository(patientDataHandler);
            DoctorAppointmentService doctorAppointmentService = new DoctorAppointmentService(appointmentRepository, patientRepository);
            doctorAppointmentController = new DoctorAppointmentController(doctorAppointmentService);


            //DoctorAppointmentService doctorService = new DoctorAppointmentService();
        }
    }
}
