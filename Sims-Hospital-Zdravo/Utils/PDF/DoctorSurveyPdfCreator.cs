using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace Sims_Hospital_Zdravo.Utils
{
    public class DoctorSurveyPdfCreator
    {
        private SurveyController surveyController;
        private Doctor doctor;

        public DoctorSurveyPdfCreator(Doctor doctor)
        {
            App app = Application.Current as App;
            surveyController = app._surveyController;
            this.doctor = doctor;
        }

        public void PrintSurvey()
        {
            try
            {
                List<ISurveyStatistic> statistics = surveyController.GetDoctorSurveys(doctor._Id);
                PdfDocument document = new PdfDocument();
                DrawAllGrids(document, statistics);
                FileStream stream = CreateAndSaveDocument(document);
                CloseStreams(stream, document);
                Process.Start(@"..\..\Resources\output.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                MessageBox.Show("Resource shouldn't be opened in another view!");
            }
        }

        private void CloseStreams(FileStream stream, PdfDocument document)
        {
            stream.Close();
            document.Close(true);
        }

        private FileStream CreateAndSaveDocument(PdfDocument document)
        {
            FileStream stream = File.Create(@"..\..\Resources\output.pdf");
            document.Save(stream);
            return stream;
        }

        private void DrawAllGrids(PdfDocument document, List<ISurveyStatistic> statistics)
        {
            PdfPage page = document.Pages.Add();
            PdfLayoutResult result = CreateFirstGrid(page, (DoctorSurvey)statistics[0]);
            int counter = 1;
            result = statistics.Aggregate(result, (current, statistic) => DrawNextGrid(current.Page, counter++, statistic));
        }

        private PdfLayoutResult CreateFirstGrid(PdfPage page, DoctorSurvey survey)
        {
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;
            PdfGrid firstGrid = CreateGridFromSurvey(survey);
            PdfLayoutResult result = firstGrid.Draw(page, new PointF(10, 10), layoutFormat);
            return result;
        }

        private PdfLayoutResult DrawNextGrid(PdfPage page, int counter, ISurveyStatistic statistic)
        {
            PdfGrid grid = CreateGridFromSurvey((DoctorSurvey)statistic);
            return grid.Draw(page, new PointF(10, 10 + counter * 90));
        }

        private PdfGrid CreateGridFromSurvey(DoctorSurvey hospitalSurvey)
        {
            PdfGrid pdfGrid = new PdfGrid();
            DataTable table = CreateDataTableForSurveys(hospitalSurvey);
            pdfGrid.DataSource = table;
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable6Colorful);
            return pdfGrid;
        }


        private DataTable CreateDataTableForSurveys(DoctorSurvey survey)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Doctor");
            dataTable.Columns.Add("Content");
            dataTable.Columns.Add("Rating");

            foreach (QuestionAndRate question in survey.QuestionsAndRates)
            {
                dataTable.Rows.Add(new object[] { doctor._Name + doctor._Surname, question.Question, question.Rate.ToString() });
            }

            return dataTable;
        }
    }
}