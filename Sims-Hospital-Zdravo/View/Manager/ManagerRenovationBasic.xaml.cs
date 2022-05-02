﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerRenovationBasic : Window
    {
        private RenovationController renovationController;
        private RoomController roomController;
        private App app;

        public ManagerRenovationBasic()
        {
            app = Application.Current as App;
            this.renovationController = app._renovationController;
            this.roomController = app._roomController;
            InitializeComponent();

            RenovationRooms.ItemsSource = roomController.ReadAll();
        }


        private void UpdateTakenDates_Change(object sender, SelectionChangedEventArgs e)
        {
            List<TimeInterval> dates = renovationController.GetTakenDateIntervals((Room)RenovationRooms.SelectedItem);
            TakenDates.ItemsSource = new List<string>(dates.Select(x => x.toDateString()));
        }

        private void SaveRenovation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room room = (Room)RenovationRooms.SelectedItem;
                DateTime start = DateTime.Parse(StartDate.Text);
                DateTime end = DateTime.Parse(EndDate.Text).AddHours(23).AddMinutes(59).AddSeconds(59);

                TimeInterval schedule = new TimeInterval(start, end);
                string description = Description.Text;
                RenovationType type = RenovationType.BASIC;
                renovationController.MakeRenovationAppointment(schedule, room, type, description);
                MessageBox.Show("Successfully made appointment for renovation!");
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}