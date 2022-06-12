using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Sims_Hospital_Zdravo.ViewModel.Commands;

namespace Sims_Hospital_Zdravo.View.Manager.Surveys
{
    public partial class PdfViewer : Page
    {
        public PdfViewer()
        {
            InitializeComponent();
            pdfWebViewer.KeyDown += new KeyEventHandler(GoBack);
            pdfWebViewer.Focusable = false;
            pdfWebViewer.Navigate(Path.GetFullPath(@"..\..\Resources\output.pdf"));
        }

        private void GoBack(Object o, KeyEventArgs args)
        {
            if (args.Key == Key.Escape)
            {
                pdfWebViewer.Navigate("about:blank");
                // GoBackCommand goBackCommand = new GoBackCommand();
                // goBackCommand.Execute(null);
            }
        }
    }
}