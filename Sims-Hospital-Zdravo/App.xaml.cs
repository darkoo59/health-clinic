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

namespace Sims_Hospital_Zdravo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal RoomController roomController;

        public App() 
        {


            RoomDataHandler roomDataHandler = new RoomDataHandler();
            RoomRepository roomRepository = new RoomRepository(roomDataHandler);
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);

            PatientDataHandler patientDataHandler = new PatientDataHandler();


            MedicalRecordDataHandler medicalRecordDataHandler = new MedicalRecordDataHandler();
            MedicalRecordsRepository medicalRepo = new MedicalRecordsRepository(patientDataHandler, medicalRecordDataHandler);
            medicalRepo.loadData();
            MedicalRecordService recordService = new MedicalRecordService(medicalRepo);
            MedicalRecordController recordController = new MedicalRecordController(recordService);

            recordController.Create(new MedicalRecord(1, 1, GenderType.MALE, BloodType.ABNEGATIVE, MaritalType.MARRIED),
                new Patient(1,"Darko","Selakovic",new DateTime(2000,11,01),"darkoo@gmail.com","123214123","+381321333"));

        }
    }
}
