using EventAggregatorLibrary;
using MedAssistUILibrary.Authorization;
using MedAssistUILibrary.Models;
using NewDataAccessLibrary;
using NewDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Device.Location;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MedAssistUILibrary.Healthcare
{
    public class HealthcareManager : ManagerBase
    {
        protected HealthcareManager(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }

        public void RequestAmbulance(int hospitalId, GeoCoordinate coordinate)
        {
            using(MedAssistContext context = new MedAssistContext())
            {
                var query = from endUser in context.EndUsers
                            where endUser.UserId == _loggedInUser.Id
                            select endUser;
                EndUser selectedEndUser = query.ToList().First();

                context.AmbulanceRequests.Add(new AmbulanceRequest()
                {
                    HospitalId = hospitalId,
                    EndUserId = selectedEndUser.Id
                });
                context.SaveChanges();
            }
        }
        public void ArrangeAppointment(int doctorId)
        {
            EndUser selectedEndUser;
            using(MedAssistContext context = new MedAssistContext())
            {
                var endUserQuery = from endUser in context.EndUsers
                                   where endUser.UserId == _loggedInUser.Id
                                   select endUser;

                selectedEndUser = endUserQuery.ToList().First();

                context.AppointmentRequests.Add(new AppointmentRequest()
                {
                    DoctorId = doctorId,
                    EndUserId = selectedEndUser.Id
                });
                context.SaveChanges();
            }
        }

        public ObservableCollection<HealthcareUnitModel> GetHealthcareUnitsList()
        {
            ObservableCollection<HealthcareUnitModel> outputs = new ObservableCollection<HealthcareUnitModel>();

            using (var context = new MedAssistContext())
            {
                var healthcareUnits = context.HealthcareUnits;

                foreach(var item in healthcareUnits)
                {
                    ObservableCollection<DoctorModel> doctorModels = new ObservableCollection<DoctorModel>();
                    foreach(var doctor in item.Doctors)
                    {
                        ObservableCollection<DoctorScheduleModel> scheduleModels = new ObservableCollection<DoctorScheduleModel>();
                        foreach(var schedule in doctor.Schedules)
                        {
                            scheduleModels.Add(new DoctorScheduleModel()
                            {
                                Id = schedule.Id,
                                DoctorId = schedule.DoctorId,
                                DayOfTheWeek = schedule.DayOfTheWeek,
                                StartTimeInMinutesPastMidnight = schedule.StartTimeInMinutesPastMidnight,
                                EndTimeInMinutesPastMidnight = schedule.EndTimeInMinutesPastMidinight
                            });
                        }

                        doctorModels.Add(new DoctorModel()
                        {
                            Id = doctor.Id,
                            FullName = doctor.FullName,
                            HealthcareUnitId = doctor.HealthcareUnitId,
                            ShortBio = doctor.ShortBio,
                            Status = doctor.Status,
                            Specialization = doctor.Specialization,
                            ProfileImage = ConvertByteArrayToBitmapImage(doctor.RawProfileImage),
                            Schedules = scheduleModels
                        });
                    }
                    ObservableCollection<HealthcareStaffModel> staffModels = new ObservableCollection<HealthcareStaffModel>();
                    foreach (var staff in item.HealthcareStaffs)
                    {
                        staffModels.Add(new HealthcareStaffModel()
                        {
                            Id = staff.Id,
                            HealthcareUnitId = staff.HealthcareUnitId,
                            UserId = staff.UserId
                        });
                    }

                    HealthcareUnitModel output = new HealthcareUnitModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Doctors = doctorModels,
                        HealthcareStaffs = staffModels
                    };

                    outputs.Add(output);
                }
            }

            return outputs;
        }

        public ObservableCollection<DoctorModel> GetDoctorsByHealthcareUnitId(int healthcareUnitId)
        {
            var healthcareUnit = GetHealthcareUnitsList().FirstOrDefault(h => h.Id == healthcareUnitId);

            if(healthcareUnit == null)
            {
                throw new Exception("Healthcare Unit not found.");
            }

            return healthcareUnit.Doctors;
        }

        public void AddDoctor(int healthcareUnitId, DoctorModel doctorModel)
        {
            using (var context = new MedAssistContext())
            {
                var healthcareUnit = context.HealthcareUnits.FirstOrDefault(h => h.Id == healthcareUnitId);

                List<DoctorSchedule> doctorSchedules = new List<DoctorSchedule>();
                foreach(var doctorSchedule in doctorModel.Schedules)
                {
                    doctorSchedules.Add(new DoctorSchedule()
                    {
                        
                    });
                }
                healthcareUnit.Doctors.Add(new Doctor()
                {
                    FullName = doctorModel.FullName,
                    HealthcareUnitId = doctorModel.HealthcareUnitId,
                    RawProfileImage = ConvertPngToByteArray(doctorModel.ProfileImage),
                    Specialization = doctorModel.Specialization,
                    ShortBio = doctorModel.ShortBio,
                    Schedules = 
                });
            }
        }

        private BitmapImage ConvertByteArrayToBitmapImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0) return null;
            BitmapImage image = new BitmapImage();
            using (var mem = new MemoryStream(imageBytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        private byte[] ConvertPngToByteArray(BitmapImage image)
        {
            byte[] output;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                output = ms.ToArray();
            }

            return output;
        }
    }
}
