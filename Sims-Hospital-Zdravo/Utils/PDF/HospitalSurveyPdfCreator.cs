using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Manager.Surveys;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Sims_Hospital_Zdravo.ViewModel.Commands;

namespace Sims_Hospital_Zdravo.Utils
{
    public class HospitalSurveyPdfCreator
    {
        private SurveyController surveyController;

        public HospitalSurveyPdfCreator()
        {
            surveyController = new SurveyController();
        }


        public void PrintSurvey()
        {
            try
            {
                List<ISurveyStatistic> statistics = surveyController.GetHospitalSurveys();
                PdfDocument document = new PdfDocument();
                DrawAllGrids(document, statistics);
                FileStream stream = CreateAndSaveDocument(document);
                CloseStreams(stream, document);
                PdfViewer viewer = new PdfViewer();
                NavigateWithParameterCommand navigateWithParameterCommand = new NavigateWithParameterCommand(viewer);
                navigateWithParameterCommand.Execute(null);
                // Process.Start(@"..\..\Resources\output.pdf");
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
            PdfLayoutResult result = CreateFirstGrid(page, (HospitalSurvey)statistics[0]);
            result.Page.Graphics.DrawString("Hospital Surveys", new PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new PointF(230, 0));
            int counter = 1;
            result = statistics.Aggregate(result, (current, statistic) => DrawNextGrid(current.Page, counter++, statistic));
        }

        private PdfLayoutResult CreateFirstGrid(PdfPage page, HospitalSurvey survey)
        {
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;
            PdfGrid firstGrid = CreateGridFromSurvey(survey);
            PdfLayoutResult result = firstGrid.Draw(page, new PointF(10, 30), layoutFormat);
            return result;
        }

        private PdfLayoutResult DrawNextGrid(PdfPage page, int counter, ISurveyStatistic statistic)
        {
            PdfGrid grid = CreateGridFromSurvey((HospitalSurvey)statistic);
            return grid.Draw(page, new PointF(10, 30 + counter * 90));
        }

        private PdfGrid CreateGridFromSurvey(HospitalSurvey hospitalSurvey)
        {
            PdfGrid pdfGrid = new PdfGrid();
            DataTable table = CreateDataTableForSurveys(hospitalSurvey);
            pdfGrid.DataSource = table;
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable6Colorful);
            return pdfGrid;
        }


        private DataTable CreateDataTableForSurveys(HospitalSurvey survey)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Content");
            dataTable.Columns.Add("Rating");

            foreach (QuestionAndRate question in survey.QuestionsAndRates)
            {
                dataTable.Rows.Add(new object[] { question.Question, question.Rate.ToString() });
            }

            return dataTable;
        }
    }
}