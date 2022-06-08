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
using XamlGeneratedNamespace;

namespace Sims_Hospital_Zdravo.Repository
{
    public class RequestForFreeDaysRepository:IRequestForFreeDaysRepository
    {
        private List<FreeDaysRequest> _requests;
        private RequestForFreeDaysDataHandler _requestForFreeDaysDataHandler;
        public RequestForFreeDaysRepository()
        {
            _requests = new List<FreeDaysRequest>();
            this._requestForFreeDaysDataHandler = new RequestForFreeDaysDataHandler();
            LoadDataFromFiles();

        }


        public void Create(FreeDaysRequest request)
        {
            LoadDataFromFiles();
            request.Id = GenerateId();
            _requests.Add(request);
            LoadDataToFile();
        }

        public void Update(FreeDaysRequest request)
        {
            LoadDataFromFiles();
            foreach(FreeDaysRequest req in _requests)
            {
                if(request.Id == req.Id)
                {
                    req.Status = request.Status;
                }
            }
            LoadDataToFile();
        }

        public void DeleteById(int id)
        {
            LoadDataFromFiles();
            foreach (var request in _requests.Where(request => request.Id == id))
            {
                _requests.Remove(request);
                LoadDataToFile();
                return;
            }
        }

        public void Delete(FreeDaysRequest request)
        {
            LoadDataToFile();
            _requests.Remove(request);
            LoadDataToFile();
        }

        public List<FreeDaysRequest> FindAll()
        {
            LoadDataFromFiles();
            return _requests;

        }
        public List<FreeDaysRequest> ReadAllByDoctor(int doctorId)
        {
            List<FreeDaysRequest> doctorrequests = new List<FreeDaysRequest>();
            LoadDataFromFiles();
            foreach(FreeDaysRequest freeDaysRequest in _requests)
            {
                if(freeDaysRequest.Doctor.Id == doctorId)
                {
                    doctorrequests.Add(freeDaysRequest);
                }
            }
            return doctorrequests;
        }
        public List<FreeDaysRequest> FindRequestByDoctorSpecialty(Doctor doctor)
        {
            LoadDataFromFiles();
            List<FreeDaysRequest> requestsBySpecialty = new List<FreeDaysRequest>();
            foreach(FreeDaysRequest request in _requests)
            {
                if(request.Doctor.Specialty.Equals(doctor.Specialty))
                {
                    requestsBySpecialty.Add(request);
                }
            }
            return requestsBySpecialty;
        }
        public List<FreeDaysRequest> RequestPendingOrApproved(Doctor doctor)
        {
            LoadDataFromFiles();
            List<FreeDaysRequest> requestsPendingOrAccepted = new List<FreeDaysRequest>();
            List<FreeDaysRequest> requests = FindRequestByDoctorSpecialty(doctor);
            foreach(FreeDaysRequest request in requests)
            {
                if(request.Status == RequestStatus.ACCEPTED || request.Status == RequestStatus.PENDING)
                {
                    requestsPendingOrAccepted.Add(request);
                }
            }
            return requestsPendingOrAccepted;
        }

        public int GenerateId()
        {
            LoadDataFromFiles();
            List<int> ids = new List<int>();
            int id = 0;
            foreach (FreeDaysRequest request in _requests)
            {
                ids.Add(request.Id);
            }
            while (ids.Contains(id))
            {
                id++;
            }
            return id;
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
