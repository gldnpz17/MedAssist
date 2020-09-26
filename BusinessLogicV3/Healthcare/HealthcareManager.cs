using BusinessLogicV3.Models;
using DataAccessV3;
using DataAccessV3.Models;
using EventAggregatorLibrary;
using MedAssistBusinessLogic.Authorization;
using MedAssistBusinessLogic.LocationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MedAssistBusinessLogic.Healthcare
{
    public class HealthcareManager : ManagerBase
    {
        private ILocationService _locationService;

        public HealthcareManager(IEventAggregator eventAggregator, ILocationService locationService) : base(eventAggregator)
        {
            _locationService = locationService;
        }

        public ObservableCollection<DoctorModel> GetDoctorsList()
        {
            ObservableCollection<DoctorModel> output = new ObservableCollection<DoctorModel>();
            using (var context = new MedAssistContext())
            {
                var doctors = context.Doctors.ToList();

                foreach(var doctor in doctors)
                {
                    output.Add(new DoctorModel()
                    {
                        Id = doctor.Id,
                        Name = doctor.Name,
                        Specialization = doctor.Specialization,
                        Status = doctor.Status
                    });
                }
            }
            return output;
        }

        public void RequestAmbulance()
        {
            GeoCoordinate coordinates = _locationService.GetLocation();
            using (var context = new MedAssistContext())
            {
                context.AmbulanceRequests.Add(new AmbulanceRequest()
                {
                    Sender = _loggedInUser.Id,
                    Latitude = coordinates.Latitude,
                    Longitude = coordinates.Longitude,
                    IsDone = false
                });
                context.SaveChanges();
            }
        }
        public void ArrangeAppointment(int doctorId)
        {
            GeoCoordinate coordinates = _locationService.GetLocation();
            using(var context = new MedAssistContext())
            {
                context.AppointmentRequests.Add(new AppointmentRequest()
                {
                    AccountId = _loggedInUser.Id,
                    DoctorId = doctorId,
                    Latitude = coordinates.Latitude,
                    Longitude = coordinates.Longitude,
                    IsDone = false
                });
                context.SaveChanges();
            }
        }

        public ObservableCollection<AmbulanceRequestModel> GetAmbulanceRequestsList()
        {
            ObservableCollection<AmbulanceRequestModel> output = new ObservableCollection<AmbulanceRequestModel>();
            using (var context = new MedAssistContext())
            {
                var ambulanceRequests = from ambulanceRequest in context.AmbulanceRequests
                                        where ambulanceRequest.IsDone == false
                                        select ambulanceRequest;

                //map models
                foreach(var ambulanceRequest in ambulanceRequests)
                {
                    output.Add(new AmbulanceRequestModel()
                    {
                        Id = ambulanceRequest.Id,
                        Latitude = ambulanceRequest.Latitude,
                        Longitude = ambulanceRequest.Longitude,
                        IsDone = ambulanceRequest.IsDone
                    });
                }
            }
            return output;
        }
        public void MarkAmbulanceRequestAsDone(int ambulanceRequestId)
        {
            using (var context = new MedAssistContext())
            {
                var request = context.AmbulanceRequests.FirstOrDefault(a => a.Id == ambulanceRequestId);
                if(request.IsDone == true)
                {
                    throw new Exception("Request already marked as done.");
                }
                request.IsDone = true;

                context.SaveChanges();
            }
        }

        public ObservableCollection<AppointmentRequestModel> GetAppointmentRequestsList()
        {
            ObservableCollection<AppointmentRequestModel> output = new ObservableCollection<AppointmentRequestModel>();
            using (var context = new MedAssistContext())
            {
                var appointmentRequests = from appointmentRequest in context.AppointmentRequests
                                          where appointmentRequest.IsDone == false
                                          select appointmentRequest;

                //map models
                foreach (var appointmentRequest in appointmentRequests)
                {
                    output.Add(new AppointmentRequestModel()
                    {
                        Id = appointmentRequest.Id,
                        AccountId = appointmentRequest.AccountId,
                        DoctorId = appointmentRequest.DoctorId,
                        Latitude = appointmentRequest.Latitude,
                        Longitude = appointmentRequest.Longitude,
                        IsDone = appointmentRequest.IsDone
                    });
                }
            }
            return output;
        }
        public void MarkAppointmentRequestAsDone(int appointmentRequestId)
        {
            using (var context = new MedAssistContext())
            {
                var request = context.AppointmentRequests.FirstOrDefault(a => a.Id == appointmentRequestId);
                if (request.IsDone == true)
                {
                    throw new Exception("Request already marked as done.");
                }
                request.IsDone = true;

                context.SaveChanges();
            }
        }

        public void SetDoctorStatus(int doctorId, string status)
        {
            using(var context = new MedAssistContext())
            {
                var doctor = context.Doctors.FirstOrDefault(d => d.Id == doctorId);

                doctor.Status = status;

                context.SaveChanges();
            }
        }
    }
}
