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

        public App() 
        {
            RoomDataHandler roomDataHandler = new RoomDataHandler();
            RoomRepository roomRepository = new RoomRepository(roomDataHandler);
            roomRepository.loadData();
            RoomService roomService = new RoomService(roomRepository);
            RoomController roomController = new RoomController(roomService);

            List<Room> rooms = roomController.Read();

            foreach(Room room in rooms) {
                Console.WriteLine(room._Floor);
                Console.WriteLine(room._Id);
                
            }
        }
    }
}
