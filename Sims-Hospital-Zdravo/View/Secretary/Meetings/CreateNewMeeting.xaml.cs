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
        private MeetingCreatedNotifications _createdNotification;
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
                if (!ListOptional.Items.Contains(user) && !ListRequired.Items.Contains(user))
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
                if (!ListOptionalOthers.Items.Contains(user) && !ListRequired.Items.Contains(user))
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
                if (!ListRequired.Items.Contains(user) && !ListOptional.Items.Contains(user))
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
                if (!ListRequiredOthers.Items.Contains(user) && !ListOptional.Items.Contains(user))
                    ListRequired.Items.Add(user);
            }
        }

        private void ListViewItem_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (TgButton.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_profile.Visibility = Visibility.Collapsed;
                tt_about.Visibility = Visibility.Collapsed;
                tt_meetings.Visibility = Visibility.Collapsed;
                tt_accounts.Visibility = Visibility.Collapsed;
                tt_equipment.Visibility = Visibility.Collapsed;
                tt_appointments.Visibility = Visibility.Collapsed;
                tt_contacts.Visibility = Visibility.Collapsed;
                tt_medical_records.Visibility = Visibility.Collapsed;
                tt_settings.Visibility = Visibility.Collapsed;
                tt_sign_out.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_profile.Visibility = Visibility.Visible;
                tt_about.Visibility = Visibility.Visible;
                tt_meetings.Visibility = Visibility.Visible;
                tt_accounts.Visibility = Visibility.Visible;
                tt_equipment.Visibility = Visibility.Visible;
                tt_appointments.Visibility = Visibility.Visible;
                tt_contacts.Visibility = Visibility.Visible;
                tt_medical_records.Visibility = Visibility.Visible;
                tt_settings.Visibility = Visibility.Visible;
                tt_sign_out.Visibility = Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Home_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }

        private void Equipment_Click(object sender, MouseButtonEventArgs e)
        {
            SuppliesHome window = new SuppliesHome();
            window.Show();
            this.Close();
        }

        private void MedicalRecord_Click(object sender, MouseButtonEventArgs e)
        {
            SecretaryWindow window = new SecretaryWindow();
            window.Show();
            this.Close();
        }

        private void Appointment_Click(object sender, MouseButtonEventArgs e)
        {
            ExaminationWindow window = new ExaminationWindow();
            window.Show();
            this.Close();
        }

        private void FreeDays_Click(object sender, MouseButtonEventArgs e)
        {
            FreeDaysWindow window = new FreeDaysWindow();
            window.Show();
            this.Close();
        }

        private void ScheduleMeeting_Click(object sender, RoutedEventArgs e)
        {
            if (comboRoom.SelectedValue == null)
            {
                MessageBox.Show("Please select room first!", "Select room", MessageBoxButton.OK);
            }
            else if (ListRequired.Items == null || ListOptional.Items == null)
            {
                MessageBox.Show("Please select at least one attendee!", "Select who will attend", MessageBoxButton.OK);
            }
            else if (startDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select date!", "Select date!", MessageBoxButton.OK);
            }
            else
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

                    Meeting meeting = new Meeting((DateTime)startDatePicker.SelectedDate, (Room)comboRoom.SelectedValue,
                        optional, required);
                    //this._CreatedNotification = new MedicineCreatedNotification("Medicine " + name + " added!", doctor._Id, this.Medicine, notificationController.GenerateId());
                    List<Notification> notificationsToAdd = new List<Notification>();
                    foreach (User user in meeting.RequiredAttendees)
                    {
                        notificationsToAdd.Add(new MeetingCreatedNotifications("You have new meeting on " + meeting.Start.ToString(), meeting.Start,
                            user._Role, user.Id, (new NotificationController()).GenerateId()));
                    }

                    _meetingController.CreateMeetingWithNotifying(meeting, notificationsToAdd);
                    MessageBox.Show("Meeting successfully created!", "Successfully created!", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Notify(Notification notification)
        {
            MeetingCreatedNotifications meetingCreatedNotification = notification as MeetingCreatedNotifications;
            if (meetingCreatedNotification is null) return;

            _notificationManager.Show(
                new NotificationContent { Title = "Meeting notification", Message = "You have new meeting at " + meetingCreatedNotification.MeetingStart.ToString() },
                areaName: "WindowArea", expirationTime: TimeSpan.FromSeconds(10));

            (new NotificationController()).Delete(notification);
        }
    }
}