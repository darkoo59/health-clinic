using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Controller;
using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Manager;
using Sims_Hospital_Zdravo.View.Manager.Custom;

namespace Sims_Hospital_Zdravo.ViewModel
{
    public class RenovationsViewModel
    {
        public List<RenovationAppointment> Renovations { get; set; }
        public DataGridSortingEventHandler SortingHandler { get; set; }


        private App app;
        private RenovationController renovationController;
        private RoomController roomController;

        public RenovationsViewModel()
        {
            app = Application.Current as App;
            renovationController = app._renovationController;
            roomController = new RoomController();

            Renovations = renovationController.FindAll().ToList();
            SortingHandler = new DataGridSortingEventHandler(SortHandler);
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


            ListCollectionView lcv = (ListCollectionView)CollectionViewSource.GetDefaultView((sender as DataGrid).ItemsSource);
            comparer = new CustomTimeIntervalSorter(direction);

            lcv.CustomSort = comparer;
        }
    }
}