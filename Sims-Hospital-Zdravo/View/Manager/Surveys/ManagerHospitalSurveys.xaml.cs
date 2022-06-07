using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.ViewModel;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Tables;
using Page = System.Windows.Controls.Page;

namespace Sims_Hospital_Zdravo.View.Manager.Surveys
{
    public partial class ManagerHospitalSurveys : Page
    {
        private SurveyController surveyController;

        public ManagerHospitalSurveys()
        {
            App app = Application.Current as App;
            surveyController = app._surveyController;
            InitializeComponent();
            DataContext = new ManagerHospitalSurveyViewModel();
        }
    }
}