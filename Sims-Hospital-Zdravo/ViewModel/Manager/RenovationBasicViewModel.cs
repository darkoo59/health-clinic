using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class RenovationBasicViewModel : INotifyPropertyChanged
    {
        public Room SelectedRoom
        {
            get { return room; }
            set
            {
                room = value;
                UpdateTimeIntervals();
            }
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String Description { get; set; }
        public ObservableCollection<string> AlreadyTakenDates { get; set; }
        public List<Room> rooms { get; set; }
        public ICommand SaveRenovationCommand => new SaveRenovationCommand(this);

        private App app;
        private ObservableCollection<string> takenDates;
        private Room room;
        private RenovationController renovationController;
        private RoomController roomController;

        public RenovationBasicViewModel()
        {
            app = Application.Current as App;
            renovationController = app._renovationController;
            roomController = new RoomController();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            rooms = roomController.FindAll().ToList();
        }


        public void Validate()
        {
            ValidateRoom();
            ValidateDescription();
        }

        private void UpdateTimeIntervals()
        {
            List<TimeInterval> dates = renovationController.GetTakenDateIntervals(SelectedRoom);
            // new ObservableCollection<string>((dates.Select(x => x.toDateString())).ToList());
            AlreadyTakenDates = new ObservableCollection<string>(dates.Select(x => x.toDateString()).ToList());
            Console.WriteLine(SelectedRoom.Id);
            Console.WriteLine(AlreadyTakenDates.Count);
            OnPropertyChanged("AlreadyTakenDates");
        }

        private void ValidateDescription()
        {
            if (Description.Trim().Equals("")) throw new Exception("Description text should contain at least one letter!");
        }

        private void ValidateRoom()
        {
            if (SelectedRoom == null) throw new Exception("Room should be selected!");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SaveRenovationCommand : ICommand
    {
        private RenovationBasicViewModel renovationBasicViewModel;
        private RenovationController renovationController;

        public SaveRenovationCommand(RenovationBasicViewModel renovationBasicViewModel)
        {
            this.renovationBasicViewModel = renovationBasicViewModel;
            renovationController = (Application.Current as App)._renovationController;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                renovationBasicViewModel.Validate();
                Room room = renovationBasicViewModel.SelectedRoom;
                DateTime start = renovationBasicViewModel.StartDate;
                DateTime end = renovationBasicViewModel.EndDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                TimeInterval schedule = new TimeInterval(start, end);
                string description = renovationBasicViewModel.Description;

                RenovationAppointment renovationAppointment = new RenovationAppointment(schedule, room, description, RenovationType.BASIC, renovationController.GenerateId());
                renovationController.MakeRenovationAppointment(renovationAppointment);
                MessageBox.Show("Successfully made appointment for renovation!");
                Commands.Commands.GoBackCommand.Execute("");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}