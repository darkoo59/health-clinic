using Sims_Hospital_Zdravo.View.Secretary.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.Form;

namespace Sims_Hospital_Zdravo.View.Secretary.Settings
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        App app;
        public SettingsWindow()
        {
            app = System.Windows.Application.Current as App;
            InitializeComponent();
            ComboTheme.Items.Add("Light");
            ComboTheme.Items.Add("Dark");

            ComboLanguage.Items.Add("English");
            ComboLanguage.Items.Add("Serbian");

            if (app._currentLanguage.Equals("en-US"))
                ComboLanguage.SelectedIndex = 0;
            else
                ComboLanguage.SelectedIndex = 1;

            if (Properties.Settings.Default.ColorMode.Equals("Light"))
                ComboTheme.SelectedIndex = 0;
            else
                ComboTheme.SelectedIndex = 1;
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && this.IsFocused == true)
                this.DragMove();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ComboLanguage.SelectedValue.ToString().Equals("Serbian") || ComboLanguage.SelectedValue.ToString().Equals("Srpski"))
            {
                TranslationSource.Instance.ChangeLanguage("sr-LATN");
                app._currentLanguage = "sr-LATN";

            }
            else
            {
                TranslationSource.Instance.ChangeLanguage("en-US");
                app._currentLanguage = "en-US";
            }

            if(ComboTheme.SelectedValue.ToString().Equals("Light"))
            {
                Properties.Settings.Default.ColorMode = "Light";
                Properties.Settings.Default.Save();
                app._currentTheme = "Light";
            }else if(ComboTheme.SelectedValue.ToString().Equals("Dark"))
            {
                Properties.Settings.Default.ColorMode = "Dark";
                Properties.Settings.Default.Save();
                app._currentTheme = "Dark";
            }
            SecretaryHome window = new SecretaryHome();
            window.Show();
            this.Close();
        }
    }
}
