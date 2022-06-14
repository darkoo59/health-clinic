using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Interfaces;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.ViewModel.Commands;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;

namespace Sims_Hospital_Zdravo.Utils.PDF
{
    public class SecretaryAppointmentsPdfCreator
    {
        private SecretaryAppointmentController _appointmentsController;

        public SecretaryAppointmentsPdfCreator()
        {
            _appointmentsController = new SecretaryAppointmentController();
        }


        public void PrintSurvey()
        {
            try
            {
                List<Appointment> weeklyAppointments = _appointmentsController.GetAppointmentsForThisWeek();
                weeklyAppointments.Sort((app1, app2) => DateTime.Compare(app1.Time.Start, app2.Time.Start));
                PdfDocument document = new PdfDocument();
                DrawAllGrids(document, weeklyAppointments);
                FileStream stream = CreateAndSaveDocument(document);
                CloseStreams(stream, document);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void CloseStreams(FileStream stream, PdfDocument document)
        {
            stream.Close();
            document.Close(true);
        }

        private FileStream CreateAndSaveDocument(PdfDocument document)
        {
            FileStream stream = File.Create(@"..\..\Resources\weeklyAppointments.pdf");
            document.Save(stream);
            return stream;
        }

        private void DrawAllGrids(PdfDocument document, List<Appointment> appointments)
        {
            PdfPage page = document.Pages.Add();
            if (appointments.Count == 0) return;
            PdfLayoutResult result = CreateFirstGrid(page, (Appointment)appointments[0]);
            result.Page.Graphics.DrawString("Weekly appointments ("+DateTime.Now+"-"+DateTime.Now.AddDays(7)+")", new 
                PdfStandardFont(PdfFontFamily.Helvetica, 10), PdfBrushes.Black, new PointF(120, 0));
            int counter = 1;
            foreach (Appointment appointment in appointments)
            {
                if (appointments.First().Equals(appointment)) continue;
                result = DrawNextGrid(result.Page, counter++, appointment);
            }
        }

        private PdfLayoutResult CreateFirstGrid(PdfPage page, Appointment appointment)
        {
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;
            PdfGrid firstGrid = CreateGridFromAppointment(appointment);
            PdfLayoutResult result = firstGrid.Draw(page, new PointF(10, 30), layoutFormat);
            return result;
        }

        private PdfLayoutResult DrawNextGrid(PdfPage page, int counter, Appointment appointment)
        {
            PdfGrid grid = CreateGridFromAppointment(appointment);
            return grid.Draw(page, new PointF(10, 30 + counter * 90));
        }

        private PdfGrid CreateGridFromAppointment(Appointment appointment)
        {
            PdfGrid pdfGrid = new PdfGrid();
            DataTable table = CreateDataTableForAppointment(appointment);
            pdfGrid.DataSource = table;
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable6Colorful);
            return pdfGrid;
        }


        private DataTable CreateDataTableForAppointment(Appointment appointment)
        {

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Patient");
            dataTable.Columns.Add("Doctor");
            dataTable.Columns.Add("Date");

            dataTable.Rows.Add(new object[]
            {
                appointment.Patient.Name + " " + appointment.Patient.Surname,
                appointment.Doctor.Name + " " + appointment.Doctor.Surname,
                appointment.Time.ToString()
            });

            return dataTable;
        }
    }
}
