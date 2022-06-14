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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Sims_Hospital_Zdravo.ViewModel.dd;
using Model;

namespace Sims_Hospital_Zdravo.View.ViewDoctor
{
    /// <summary>
    /// Interaction logic for TherapyPage.xaml
    /// </summary>
    public partial class TherapyPage : Page
    {
        private int _medId;
        private MedicalRecord _medicalRecord;
        private Frame _frame;
        public TherapyPage(int id,MedicalRecord medicalRecord,Frame frame)
        {
            InitializeComponent();
            this._medId = id;
            this._medicalRecord = medicalRecord;
            this._frame = frame;
            this.DataContext = new TherapyViewModel(_medId,frame);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           // PrescriptionList prescriptionList = new PrescriptionList(medicalRecord, _medId, frame);
        }
    }
}
