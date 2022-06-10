using System;
using System.Windows.Controls;

namespace Sims_Hospital_Zdravo.View.Manager.Surveys
{
    public partial class PdfViewer : Page
    {
        public string PDFPath { get; set; }

        public PdfViewer(string PDFPath)
        {
            this.PDFPath = PDFPath;
            InitializeComponent();
            pdfWebViewer.Navigate("C:/Fakultet/SIMS-PROJEKAT/Sims-Hospital-Zdravo/Resources/output.pdf");
        }
    }
}