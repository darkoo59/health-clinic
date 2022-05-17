using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Controller;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Manager.Custom;

namespace Sims_Hospital_Zdravo.View.Manager
{
    public partial class ManagerRenovations : Page
    {
        private RenovationController renovationController;
        private App app;
        private Frame ManagerContent;

        public ManagerRenovations()
        {
            app = Application.Current as App;
            this.renovationController = app._renovationController;
            InitializeComponent();

            RenovationsTable.ItemsSource = renovationController.ReadAll();
            RenovationsTable.Sorting += new DataGridSortingEventHandler(SortHandler);
            RetrieveMainFrame();
        }

        private void SortHandler(object sender, DataGridSortingEventArgs e)
        {
            DataGridColumn column = e.Column;

            IComparer comparer = null;
            if (!column.Header.Equals("Time"))
            {
                return;
            }

            e.Handled = true;

            ListSortDirection dir = (column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
            int direction = dir == ListSortDirection.Ascending ? 1 : -1;
            column.SortDirection = dir;


            ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView(RenovationsTable.ItemsSource);
            comparer = new CustomTimeIntervalSorter(direction);

            lcv.CustomSort = comparer;
        }

        private void BasicRenovation_Click(object sender, RoutedEventArgs e)
        {
            ManagerContent.Source = new Uri("Renovations/ManagerRenovationBasic.xaml", UriKind.Relative);
        }


        private void RetrieveMainFrame()
        {
            foreach (Window win in Application.Current.Windows)
            {
                if (win.GetType() == typeof(ManagerMainWindow))
                {
                    ManagerContent = ((ManagerMainWindow)win).ManagerContent;
                }
            }
        }

        private void AdvancedRenovation_Click(object sender, RoutedEventArgs e)
        {
            ManagerContent.Source = new Uri("Renovations/ManagerRenovationAdvanced.xaml", UriKind.Relative);
        }
    }
}