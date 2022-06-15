using Controller;
using Model;
using Sims_Hospital_Zdravo.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Sims_Hospital_Zdravo.ViewModel.Secretary
{
    public class MedicalRecordViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private App app;
        private MedicalRecordController _medicalController;
        
        public List<MedicalRecord> MedicalRecords { get; set; }
        public ICommand InsertRecordCommand => new InsertRecordCommand();
        public ICommand UpdateRecordCommand => new UpdateRecordCommand();
        public ICommand DeleteRecordCommand => new DeleteRecordCommand(_medicalController);
        public MedicalRecordViewModel()
        {
            app = Application.Current as App;
            _medicalController = new MedicalRecordController();
            MedicalRecords = _medicalController.FindAll().ToList();
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class InsertRecordCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MedicalRecordController _recordController;
        public InsertRecordCommand()
        {
            _recordController = new MedicalRecordController();
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                InsertRecordWindow insertDialog = new InsertRecordWindow();
                if (insertDialog.ShowDialog() == false)
                {
                    MedicalRecord medicalRecord = insertDialog.MedicalRecord;
                    Patient patient = insertDialog.Patient;
                    if (medicalRecord == null) return;
                    _recordController.Create(medicalRecord, patient);
                    MessageBox.Show("Successfully created medical record!", "Successfully created!", MessageBoxButton.OK);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

    public class UpdateRecordCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                MedicalRecord medicalRecord = parameter as MedicalRecord;
                if (medicalRecord == null) throw new Exception("Medical record not selected");
                UpdateRecordWindow updateRecordWindow = new UpdateRecordWindow()
                {
                    DataContext = medicalRecord
                };
                updateRecordWindow.Show();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }

    public class DeleteRecordCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private MedicalRecordController _medicalRecordController;

        public DeleteRecordCommand(MedicalRecordController controller)
        {
            this._medicalRecordController = controller;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            try
            {
                MedicalRecord medicalRecord = parameter as MedicalRecord;
                if (medicalRecord == null) throw new Exception("Medical record not selected");
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to delete this item?", "Delete", MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                    _medicalRecordController.Delete(medicalRecord);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}