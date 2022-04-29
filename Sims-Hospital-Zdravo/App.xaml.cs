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
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Service;

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
        internal EquipmentTransferController equipmentTransferController;
        internal EquipmentController equipmentController;
        internal AccountController accountController;
        internal PatientMedicalRecordController patientMedRecController;

        public App()
        {


            RoomDataHandler roomDataHandler = new RoomDataHandler();
            RoomRepository roomRepository = new RoomRepository(roomDataHandler);
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientDataHandler patientDataHandler = new PatientDataHandler();
            PatientRepository patientRepository = new PatientRepository(patientDataHandler);


            MedicalRecordDataHandler medicalRecordDataHandler = new MedicalRecordDataHandler();
            MedicalRecordsRepository medicalRepo = new MedicalRecordsRepository(medicalRecordDataHandler);
            MedicalRecordService recordService = new MedicalRecordService(medicalRepo, patientRepository);
            recordController = new MedicalRecordController(recordService);

            AppointmentDataHandler appointmentDataHandler = new AppointmentDataHandler();
            DoctorDataHandler doctorDataHandler = new DoctorDataHandler();
            DoctorRepository doctorRepository = new DoctorRepository(doctorDataHandler);
            AppointmentRepository appointmentRepository = new AppointmentRepository(appointmentDataHandler);
            AppointmentPatientService appointmentPatientService = new AppointmentPatientService(appointmentRepository, doctorRepository);
            appointmentPatientController = new AppointmentPatientController(appointmentPatientService);

            DoctorRepository docRepo = new DoctorRepository(doctorDataHandler);
            DoctorAppointmentService doctorAppointmentService = new DoctorAppointmentService(appointmentRepository, patientRepository, docRepo);
            doctorAppointmentController = new DoctorAppointmentController(doctorAppointmentService);

            EquipmentDataHandler equipmentDataHandler = new EquipmentDataHandler();
            EquipmentRepository equipmentRepository = new EquipmentRepository(equipmentDataHandler);
            EquipmentService equipmentService = new EquipmentService(equipmentRepository);
            equipmentController = new EquipmentController(equipmentService);

            RelocationAppointmentDataHandler relocationAppointmentDataHandler = new RelocationAppointmentDataHandler();
            RelocationAppointmentRepository relocationAppointmentRepository = new RelocationAppointmentRepository(relocationAppointmentDataHandler);

            TimeSchedulerService timeSchedulerService = new TimeSchedulerService(appointmentRepository);


            EquipmentTransferService equipmentTransferService = new EquipmentTransferService(roomRepository, relocationAppointmentRepository, timeSchedulerService);
            equipmentTransferController = new EquipmentTransferController(equipmentTransferService);

            AccountDataHandler accountDataHandler = new AccountDataHandler();
            AccountRepository accountRepository = new AccountRepository(accountDataHandler);
            AccountService accountService = new AccountService(accountRepository);
            accountController = new AccountController(accountService);

            PatientMedicalRecordService patientMedicalRecordService = new PatientMedicalRecordService(medicalRepo, patientRepository);
            patientMedRecController = new PatientMedicalRecordController(patientMedicalRecordService);
            //DoctorAppointmentService doctorService = new DoctorAppointmentService();
        }
    }
}
