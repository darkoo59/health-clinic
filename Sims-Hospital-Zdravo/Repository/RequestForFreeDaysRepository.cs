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
        private ObservableCollection<FreeDaysRequest> _requests;
        private RequestForFreeDaysDataHandler _requestForFreeDaysDataHandler;
        
        public RequestForFreeDaysRepository(RequestForFreeDaysDataHandler requestForFreeDaysDataHandler)
        {
            _requests = new ObservableCollection<FreeDaysRequest>();
            this._requestForFreeDaysDataHandler = requestForFreeDaysDataHandler;
            LoadDataFromFiles();

        }


        public void Create(FreeDaysRequest request)
        {
            _requests.Add(request);
            LoadDataToFile();
        }

        public void Delete(FreeDaysRequest request)
        {
            _requests.Remove(request);
            LoadDataToFile();
        }

        public ref ObservableCollection<FreeDaysRequest> ReadAll()
        {

            return ref _requests;

        }
        
        public  ObservableCollection<FreeDaysRequest> ReadAllByDoctor(int doctorId)
        {
            ObservableCollection<FreeDaysRequest> doctorRequests = new ObservableCollection<FreeDaysRequest>();
            foreach(FreeDaysRequest request in ReadAll())
            {
                if(request.Doctor.Id == doctorId)
                {
                    doctorRequests.Add(request);
                }
            }
            return  doctorRequests;
        }
        public ObservableCollection<FreeDaysRequest> FindRequestByDoctorSpecialty(Doctor doctor)
        {
            ObservableCollection<FreeDaysRequest> requestsBySpecialty = new ObservableCollection<FreeDaysRequest>();
            foreach(FreeDaysRequest request in _requests)
            {
                if(request.Doctor._specialty.Equals(doctor._specialty))
                {
                    requestsBySpecialty.Add(request);
                }
            }
            return requestsBySpecialty;
        }
        public ObservableCollection<FreeDaysRequest> RequestPendingOrApproved(Doctor doctor)
        {
            ObservableCollection<FreeDaysRequest> requestsPendingOrAccepted = new ObservableCollection<FreeDaysRequest>();
            foreach(FreeDaysRequest request in FindRequestByDoctorSpecialty(doctor))
            {
                if(request.Status == RequestStatus.ACCEPTED || request.Status == RequestStatus.PENDING)
                {
                    requestsPendingOrAccepted.Add(request);
                }
            }
            return requestsPendingOrAccepted;
        }
        public void LoadDataFromFiles()
        {
            _requests = _requestForFreeDaysDataHandler.ReadAll();
        }
        public void LoadDataToFile()
        {
            _requestForFreeDaysDataHandler.Write(_requests);
        }
    }
}
