using Sims_Hospital_Zdravo.Controller;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.View.Secretary.FreeDays;
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
    public class FreeDaysViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private RequestForFreeDaysController _freeDaysController;
        private List<FreeDaysRequest> _freeDaysRequests;
        public ICommand UpdateRequestCommand => new UpdateRequestCommand();
        public FreeDaysViewModel()
        {
            this._freeDaysController = new RequestForFreeDaysController();
            _freeDaysRequests = this._freeDaysController.FindAll();
        }
    }

    public class UpdateRequestCommand : ICommand
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
                FreeDaysRequest request = parameter as FreeDaysRequest;
                if (request == null) throw new Exception("Free days request not selected");
                EditRequestWindow editRequestWindow = new EditRequestWindow()
                {
                    DataContext = request
                };
                editRequestWindow.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
