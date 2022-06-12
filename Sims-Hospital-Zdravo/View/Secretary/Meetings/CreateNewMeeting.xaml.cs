using Model;
using Notifications.Wpf;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.Examination;
using Sims_Hospital_Zdravo.View.Secretary.FreeDays;
using Sims_Hospital_Zdravo.View.Secretary.Supplies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sims_Hospital_Zdravo.View.Secretary.Meetings
{
    /// <summary>
    /// Interaction logic for CreateNewMeeting.xaml
    /// </summary>
    public partial class CreateNewMeeting : Window, INotificationObserver
    {
        private MeetingController _meetingController;
        private NotificationManager _notificationManager;
        private App app;

        public CreateNewMeeting()
        {
            app = Application.Current as App;
            InitializeComponent();
            app._taskScheduleTimer.AddObserver(this);
            this._meetingController = new MeetingController();
            this._notificationManager = new NotificationManager();
            comboRoom.ItemsSource = _meetingController.ReadAllRooms();
            foreach (User user in _meetingController.ReadAllPotentionalAttendees())
            {
                ListOptionalOthers.Items.Add(user);
                ListRequiredOthers.Items.Add(user);
            }

            //Images listeners

            ImageToLeftOptional.MouseLeftButtonDown += (s, e) => { imageToLeftOptionalFunctionality(); };

            ImageToRightOptional.MouseLeftButtonDown += (s, e) => { ImageToRightOptionalFunctionality(); };

            ImageToLeftRequired.MouseLeftButtonDown += (s, e) => { imageToLeftRequiredFunctionality(); };

            ImageToRightRequired.MouseLeftButtonDown += (s, e) => { ImageToRightRequiredFunctionality(); };
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void imageToLeftOptionalFunctionality()
        {
            foreach (User user in ListOptionalOthers.SelectedItems)
            {
                ListOptional.Items.Add(user);
            }

            ListOptionalOthers.Items.Clear();
            ListRequiredOthers.Items.Clear();
            foreach (User user in _meetingController.ReadAllPotentionalAttendees())
            {
                Console.WriteLine(user.Name + "nestooo");
                if (!CompareUsersById(ListOptional.Items, user) && !CompareUsersById(ListRequired.Items, user))
                {
                    ListOptionalOthers.Items.Add(user);
                    ListRequiredOthers.Items.Add(user);
                }
            }
        }

        private void ImageToRightOptionalFunctionality()
        {
            foreach (User user in ListOptional.SelectedItems)
            {
                ListOptionalOthers.Items.Add(user);
                ListRequiredOthers.Items.Add(user);
            }

            ListOptional.Items.Clear();
            foreach (User user in _meetingController.ReadAllPotentionalAttendees())
            {
                if (!CompareUsersById(ListOptionalOthers.Items, user) && !CompareUsersById(ListRequired.Items, user))
                    ListOptional.Items.Add(user);
            }
        }

        private void imageToLeftRequiredFunctionality()
        {
            foreach (User user in ListRequiredOthers.SelectedItems)
            {
                ListRequired.Items.Add(user);
            }

            ListRequiredOthers.Items.Clear();
            ListOptionalOthers.Items.Clear();
            foreach (User user in _meetingController.ReadAllPotentionalAttendees())
            {
                if (!CompareUsersById(ListRequired.Items, user) && !CompareUsersById(ListOptional.Items, user))
                {
                    ListRequiredOthers.Items.Add(user);
                    ListOptionalOthers.Items.Add(user);
                }
            }
        }

        private void ImageToRightRequiredFunctionality()
        {
            foreach (User user in ListRequired.SelectedItems)
            {
                ListRequiredOthers.Items.Add(user);
                ListOptionalOthers.Items.Add(user);
            }

            ListRequired.Items.Clear();
            foreach (User user in _meetingController.ReadAllPotentionalAttendees())
            {
                if (!CompareUsersById(ListRequiredOthers.Items, user) && !CompareUsersById(ListOptional.Items, user))
                    ListRequired.Items.Add(user);
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void ScheduleMeeting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<User> optional = new List<User>();
                foreach (User user in ListOptional.Items)
                {
                    optional.Add(user);
                }

                List<User> required = new List<User>();
                foreach (User user in ListRequired.Items)
                {
                    required.Add(user);
                }

                DateTime startDate;
                if (startDatePicker.SelectedDate == null)
                    startDate = DateTime.MinValue;
                else
                    startDate = (DateTime)startDatePicker.SelectedDate;

                Meeting meeting = new Meeting(startDate, (Room)comboRoom.SelectedValue,optional, required);
                List<Notification> notificationsToAdd = new List<Notification>();
                foreach (User user in meeting.RequiredAttendees)
                {
                    notificationsToAdd.Add(new MeetingCreatedNotifications(
                        "You have new meeting on " + meeting.Start.ToString(), meeting.Start,
                        user.Role, user.Id, new NotificationController().GenerateId()));
                }

                _meetingController.CreateMeetingWithNotifying(meeting, notificationsToAdd);
                MessageBox.Show("Meeting successfully created!", "Successfully created!", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Notify(Notification notification)
        {
            MeetingCreatedNotifications meetingCreatedNotification = notification as MeetingCreatedNotifications;
            if (meetingCreatedNotification is null) return;

            _notificationManager.Show(
                new NotificationContent
                {
                    Title = "Meeting notification",
                    Message = "You have new meeting at " + meetingCreatedNotification.MeetingStart.ToString()
                },
                areaName: "WindowArea", expirationTime: TimeSpan.FromSeconds(10));

            (new NotificationController()).Delete(notification);
        }

        private bool CompareUsersById(ItemCollection users, User userToFind)
        {
            foreach (User user in users)
            {
                if (user.Id == userToFind.Id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}