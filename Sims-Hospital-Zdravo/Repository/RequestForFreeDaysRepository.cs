using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.DataHandler;
using Model;

namespace Sims_Hospital_Zdravo.Repository
{
    public class RequestForFreeDaysRepository
    {
        private ObservableCollection<FreeDaysRequest> requests;
        private RequestForFreeDaysDataHandler requestForFreeDaysDataHandler;
        public RequestForFreeDaysRepository(RequestForFreeDaysDataHandler requestForFreeDaysDataHandler)
        {
            requests = new ObservableCollection<FreeDaysRequest>();
            this.requestForFreeDaysDataHandler = requestForFreeDaysDataHandler;
            LoadDataFromFiles();

        }


        public void Create(FreeDaysRequest request)
        {
            requests.Add(request);
            LoadDataToFile();
        }

        public void Delete(FreeDaysRequest request)
        {
            requests.Remove(request);
            LoadDataToFile();
        }

        public ref ObservableCollection<FreeDaysRequest> ReadAll()
        {

            return ref requests;

        }

        public ObservableCollection<FreeDaysRequest> FindRequestByDoctorSpecialty(Doctor doctor)
        {
            ObservableCollection<FreeDaysRequest> requestsBySpecialty = new ObservableCollection<FreeDaysRequest>();
            foreach(FreeDaysRequest request in requests)
            {
                if(request._Doctor.Specialty.Equals(doctor.Specialty))
                {
                    requestsBySpecialty.Add(request);
                }
            }
            return requestsBySpecialty;
        }
        public ObservableCollection<FreeDaysRequest> RequestPendingOrApproved(Doctor doctor)
        {
            ObservableCollection<FreeDaysRequest> requestsPendingOrAccepted = new ObservableCollection<FreeDaysRequest>();
            ObservableCollection<FreeDaysRequest> requests = FindRequestByDoctorSpecialty(doctor);
            foreach(FreeDaysRequest request in requests)
            {
                if(request._Status == RequestStatus.ACCEPTED || request._Status == RequestStatus.PENDING)
                {
                    requestsPendingOrAccepted.Add(request);
                }
            }
            return requestsPendingOrAccepted;
        }
        public void LoadDataFromFiles()
        {
            requests = requestForFreeDaysDataHandler.ReadAll();
        }
        public void LoadDataToFile()
        {
            requestForFreeDaysDataHandler.Write(requests);
        }
    }
}
