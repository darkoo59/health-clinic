using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using Service;
using Controller;
using Repository;
using DataHandler;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.DataHandler;
using Sims_Hospital_Zdravo.Model;
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
        // internal RoomController _roomController;
        internal MedicalRecordController _recordController;
        internal AppointmentPatientController _appointmentPatientController;
        internal DoctorAppointmentController _doctorAppointmentController;

        internal EquipmentTransferController _equipmentTransferController;

        // internal EquipmentController _equipmentController;
        internal AccountController _accountController;

        internal RenovationController _renovationController;
        internal PatientMedicalRecordController _patientMedRecController;
        internal AnamnesisController _anamnesisController;
        internal SecretaryAppointmentController _secretaryAppointmentController;
        internal PrescriptionController _prescriptionController;
        internal TaskScheduleTimer _taskScheduleTimer;
        internal MedicineController _medicineController;
        internal NotificationController _notificationController;
        internal SuppliesController _suppliesController;
        internal RequestForFreeDaysController _requestForFreeDaysController;
        internal SurveyController _surveyController;
        internal MeetingController _meetingController;

        public App()
        {
            EventManager.RegisterClassHandler(typeof(Window), Window.LoadedEvent,
                new RoutedEventHandler(WindowLoaded));
            // NotificationDataHandler notificationDataHandler = new NotificationDataHandler();
            // NotificationRepository notificationRepository = new NotificationRepository();
            // NotificationService notificationService = new NotificationService();
            _notificationController = new NotificationController();

            // RoomDataHandler roomDataHandler = new RoomDataHandler();
            // RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService();
            // _roomController = new RoomController();

            PatientDataHandler patientDataHandler = new PatientDataHandler();
            PatientRepository patientRepository = new PatientRepository(patientDataHandler);

            AllergensDataHandler allergensDataHandler = new AllergensDataHandler();
            AllergensRepository allergensRepository = new AllergensRepository(allergensDataHandler);


            PrescriptionDataHandler prescriptionDataHandler = new PrescriptionDataHandler();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository(prescriptionDataHandler);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository);

            MedicineDataHandler medicineDataHandler = new MedicineDataHandler();
            MedicineRepository medicineRepository = new MedicineRepository(medicineDataHandler);
            MedicineService medicineService = new MedicineService(medicineRepository);
            _medicineController = new MedicineController(medicineService);

            MedicalRecordDataHandler medicalRecordDataHandler = new MedicalRecordDataHandler();
            MedicalRecordsRepository medicalRepo = new MedicalRecordsRepository(medicalRecordDataHandler);
            MedicalRecordService recordService = new MedicalRecordService(medicalRepo, patientRepository, allergensRepository);
            _recordController = new MedicalRecordController(recordService, prescriptionService);

            AccountDataHandler accountDataHandler = new AccountDataHandler();
            AccountRepository accountRepository = new AccountRepository(accountDataHandler);
            AccountService accountService = new AccountService(accountRepository);
            _accountController = new AccountController(accountService);

            AppointmentDataHandler appointmentDataHandler = new AppointmentDataHandler();
            DoctorDataHandler doctorDataHandler = new DoctorDataHandler();
            DoctorRepository doctorRepository = new DoctorRepository(doctorDataHandler);
            AppointmentRepository appointmentRepository = new AppointmentRepository(appointmentDataHandler);
            AppointmentPatientService appointmentPatientService =
                new AppointmentPatientService(appointmentRepository, doctorRepository, accountRepository);
            _appointmentPatientController = new AppointmentPatientController(appointmentPatientService);

            DoctorRepository docRepo = new DoctorRepository(doctorDataHandler);

            // EquipmentDataHandler equipmentDataHandler = new EquipmentDataHandler();
            // EquipmentRepository equipmentRepository = new EquipmentRepository();
            // EquipmentService equipmentService = new EquipmentService();
            // _equipmentController = new EquipmentController();

            RelocationAppointmentDataHandler relocationAppointmentDataHandler = new RelocationAppointmentDataHandler();
            RelocationAppointmentRepository relocationAppointmentRepository =
                new RelocationAppointmentRepository();
            RenovationDataHandler renovationDataHandler = new RenovationDataHandler();
            RenovationRepository renovationRepository = new RenovationRepository();


            TimeSchedulerService timeSchedulerService = new TimeSchedulerService(appointmentRepository);

            DoctorAppointmentService doctorAppointmentService =
                new DoctorAppointmentService(appointmentRepository, patientRepository, docRepo, timeSchedulerService, roomService);
            _doctorAppointmentController = new DoctorAppointmentController(doctorAppointmentService);

            RenovationService renovationService =
                new RenovationService(timeSchedulerService);
            _renovationController = new RenovationController(renovationService);

            EquipmentTransferService equipmentTransferService =
                new EquipmentTransferService(timeSchedulerService);
            _equipmentTransferController = new EquipmentTransferController(equipmentTransferService);


            PatientMedicalRecordService patientMedicalRecordService = new PatientMedicalRecordService(medicalRepo, patientRepository);
            _patientMedRecController = new PatientMedicalRecordController(patientMedicalRecordService);

            AnamnesisDataHandler anamnesisDataHandler = new AnamnesisDataHandler();
            AnamnesisRepository anamnesisRepository = new AnamnesisRepository(anamnesisDataHandler);
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository);
            _anamnesisController = new AnamnesisController(anamnesisService);

            DoctorSurveyDataHandler doctorSurveyDataHandler = new DoctorSurveyDataHandler();
            HospitalSurveyDataHandler hospitalSurveyDataHandler = new HospitalSurveyDataHandler();
            DoctorQuestionDataHandler doctorQuestionDataHandler = new DoctorQuestionDataHandler();
            HospitalQuestionDataHandler hospitalQuestionDataHandler = new HospitalQuestionDataHandler();
            QuestionRepository questionRepository = new QuestionRepository(doctorQuestionDataHandler, hospitalQuestionDataHandler);
            DoctorSurveyRepository doctorSurveyRepository = new DoctorSurveyRepository(doctorSurveyDataHandler);
            HospitalSurveyRepository hospitalSurveyRepository = new HospitalSurveyRepository(hospitalSurveyDataHandler);
            SurveyService surveyService = new SurveyService(doctorSurveyRepository, hospitalSurveyRepository, questionRepository);
            _surveyController = new SurveyController(surveyService);

            SecretaryAppointmentService secretaryAppointmentService =
                new SecretaryAppointmentService(appointmentRepository, patientRepository, timeSchedulerService, doctorRepository);
            _secretaryAppointmentController = new SecretaryAppointmentController(secretaryAppointmentService);

            SuppliesAcquisitionDataHandler _suppliesAcquisitionDataHandler = new SuppliesAcquisitionDataHandler();
            SuppliesAcquisitionRepository _suppliesAcquisitionRepository = new SuppliesAcquisitionRepository(_suppliesAcquisitionDataHandler);
            SuppliesService suppliesService =
                new SuppliesService(_suppliesAcquisitionRepository);
            _suppliesController = new SuppliesController(suppliesService);

            MeetingDataHandler _meetingDataHandler = new MeetingDataHandler();
            MeetingRepository _meetingRepository = new MeetingRepository(_meetingDataHandler);
            MeetingService _meetingService = new MeetingService(accountRepository, _meetingRepository);
            _meetingController = new MeetingController(_meetingService);


            _prescriptionController = new PrescriptionController(prescriptionService);

            _taskScheduleTimer = new TaskScheduleTimer
            (
                _equipmentTransferController,
                _renovationController,
                _doctorAppointmentController,
                _recordController,
                _notificationController,
                _suppliesController,
                _accountController
            );

            RequestForFreeDaysDataHandler _requestForFreeDaysDataHandler = new RequestForFreeDaysDataHandler();
            RequestForFreeDaysRepository _requestForfreeDaysRepository = new RequestForFreeDaysRepository(_requestForFreeDaysDataHandler);
            RequestForFreeDaysService _requestForFreeDaysService = new RequestForFreeDaysService(_requestForfreeDaysRepository, appointmentRepository);
            _requestForFreeDaysController = new RequestForFreeDaysController(_requestForFreeDaysService);
        }

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            var window = e.Source as Window;
            System.Threading.Thread.Sleep(100);
            window.Dispatcher.Invoke(
                new Action(() => { window.MoveFocus(new TraversalRequest(FocusNavigationDirection.First)); }));
        }
    }
}