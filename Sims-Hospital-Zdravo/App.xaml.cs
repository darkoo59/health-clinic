﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using Service;
using Controller;
using Repository;
using DataHandler;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Repository;
using Sims_Hospital_Zdravo.Service;
using Sims_Hospital_Zdravo.Utils;

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal RoomController _roomController;
        internal MedicalRecordController _recordController;
        internal AppointmentPatientController _appointmentPatientController;
        internal DoctorAppointmentController _doctorAppointmentController;
        internal EquipmentTransferController _equipmentTransferController;
        internal EquipmentController _equipmentController;
        internal AccountController _accountController;
        internal RenovationController _renovationController;
        internal PrescriptionController _prescriptionController;
        internal TaskScheduleTimer taskScheduleTimer;

        public App()
        {
            RoomDataHandler roomDataHandler = new RoomDataHandler();
            RoomRepository roomRepository = new RoomRepository(roomDataHandler);
            RoomService roomService = new RoomService(roomRepository);
            _roomController = new RoomController(roomService);

            PatientDataHandler patientDataHandler = new PatientDataHandler();
            PatientRepository patientRepository = new PatientRepository(patientDataHandler);

            AllergensDataHandler allergensDataHandler = new AllergensDataHandler();
            AllergensRepository allergensRepository = new AllergensRepository(allergensDataHandler);


            MedicalRecordDataHandler medicalRecordDataHandler = new MedicalRecordDataHandler();
            MedicalRecordsRepository medicalRepo = new MedicalRecordsRepository(medicalRecordDataHandler);
            MedicalRecordService recordService = new MedicalRecordService(medicalRepo, patientRepository, allergensRepository);
            _recordController = new MedicalRecordController(recordService);

            AppointmentDataHandler appointmentDataHandler = new AppointmentDataHandler();
            DoctorDataHandler doctorDataHandler = new DoctorDataHandler();
            DoctorRepository doctorRepository = new DoctorRepository(doctorDataHandler);
            AppointmentRepository appointmentRepository = new AppointmentRepository(appointmentDataHandler);
            AppointmentPatientService appointmentPatientService =
                new AppointmentPatientService(appointmentRepository, doctorRepository);
            _appointmentPatientController = new AppointmentPatientController(appointmentPatientService);

            DoctorRepository docRepo = new DoctorRepository(doctorDataHandler);
            DoctorAppointmentService doctorAppointmentService =
                new DoctorAppointmentService(appointmentRepository, patientRepository, docRepo);
            _doctorAppointmentController = new DoctorAppointmentController(doctorAppointmentService);

            EquipmentDataHandler equipmentDataHandler = new EquipmentDataHandler();
            EquipmentRepository equipmentRepository = new EquipmentRepository(equipmentDataHandler);
            EquipmentService equipmentService = new EquipmentService(equipmentRepository);
            _equipmentController = new EquipmentController(equipmentService);

            RelocationAppointmentDataHandler relocationAppointmentDataHandler = new RelocationAppointmentDataHandler();
            RelocationAppointmentRepository relocationAppointmentRepository =
                new RelocationAppointmentRepository(relocationAppointmentDataHandler);
            RenovationDataHandler renovationDataHandler = new RenovationDataHandler();
            RenovationRepository renovationRepository = new RenovationRepository(renovationDataHandler);


            TimeSchedulerService timeSchedulerService = new TimeSchedulerService(appointmentRepository,
                renovationRepository, relocationAppointmentRepository);

            RenovationService renovationService =
                new RenovationService(renovationRepository, timeSchedulerService, roomRepository);
            _renovationController = new RenovationController(renovationService);

            EquipmentTransferService equipmentTransferService =
                new EquipmentTransferService(roomRepository, relocationAppointmentRepository, timeSchedulerService);
            _equipmentTransferController = new EquipmentTransferController(equipmentTransferService);

            AccountDataHandler accountDataHandler = new AccountDataHandler();
            AccountRepository accountRepository = new AccountRepository(accountDataHandler);
            AccountService accountService = new AccountService(accountRepository);
            _accountController = new AccountController(accountService);

            PrescriptionDataHandler prescriptionDataHandler = new PrescriptionDataHandler();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository(prescriptionDataHandler);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);
            _prescriptionController = new PrescriptionController(prescriptionService);

            taskScheduleTimer = new TaskScheduleTimer(_equipmentTransferController, _renovationController, _prescriptionController);
        }
    }
}