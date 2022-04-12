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
        internal MedicalRecordController recordController;

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


        }
    }
}
