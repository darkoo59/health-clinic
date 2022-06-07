using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sims_Hospital_Zdravo.Model;
using Sims_Hospital_Zdravo.DataHandler;
using Model;
using Sims_Hospital_Zdravo.Interfaces;

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

        public void Update(FreeDaysRequest request)
        {
            foreach(FreeDaysRequest req in _requests)
            {
                if(request.Doctor == req.Doctor && request.TimeInterval == req.TimeInterval)
                {
                    req.Status = request.Status;
                }
            }
            LoadDataToFile();
        }

        public void DeleteById(FreeDaysRequest request)
        {
            _requests.Remove(request);
            LoadDataToFile();
        }

        public ref  ObservableCollection<FreeDaysRequest> FindAll()
        {

            return  ref _requests;

        }
        
        public  ObservableCollection<FreeDaysRequest> ReadAllByDoctor(int doctorId)
        {
            ObservableCollection<FreeDaysRequest> doctorRequests = new ObservableCollection<FreeDaysRequest>();
            foreach(FreeDaysRequest request in FindAll())
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
