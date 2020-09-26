using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using System.Data.Entity;
using AsistensiMedisDatabase;
using AsistensiMedisLibraryMain;

namespace AsistensiMedisLibraryPatient
{
    public class Patient
    {
        public static void RequestAmbulance(UserToken userToken, GeoCoordinate location, string locationDetail)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.Username.Equals(userToken.userInfo.Username))))
            {
                try
                {
                    using (var db = new DatabaseMedAssistEntities())
                    {
                        var query = from ambulance in db.DatabaseAmbulances
                                    where ambulance.JumlahAmbulans > 0
                                    select ambulance;
                        var selectedAmbulance = query.OrderBy(item => GetDistance(Convert.ToDouble(item.HealthcareLatitude), Convert.ToDouble(item.HealthcareLongitude), location)).First();
                        DatabaseRequest databaseRequest = new DatabaseRequest
                        {
                            IsLocked = false,
                            SenderID = userToken.userInfo.UserID,
                            ReceiverID = selectedAmbulance.HealthcareID,
                            LocationLatitude = Convert.ToSingle(location.Latitude),
                            LocationLongitude = Convert.ToSingle(location.Longitude),
                            LocationDetail = locationDetail,
                            Request = "Ambulans"
                        };
                        selectedAmbulance.JumlahAmbulans--;
                        db.DatabaseRequests.Add(databaseRequest);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    //ui error happened
                }
            }
            else
            {
                //textbox login expired to UI
            }
        }
        public static void RequestDoctor(UserToken userToken, GeoCoordinate location, string locationDetail, DoctorType doctorType, DateTime appointmentDate, DateTime requestDate)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.Username.Equals(userToken.userInfo.Username))))
            {
                try
                {
                    using (var db = new DatabaseMedAssistEntities())
                    {
                        string day = appointmentDate.DayOfWeek.ToString();
                        var query = from dbDoctor in db.DatabaseDoctors
                                    where dbDoctor.DoctorType == doctorType.ToString() &&
                                    (day == "Monday" ? dbDoctor.AppointmentMonday :
                                    day == "Tuesday" ? dbDoctor.AppointmentTuesday :
                                    day == "Wednesday" ? dbDoctor.AppointmentWednesday :
                                    day == "Thursday" ? dbDoctor.AppointmentThursday :
                                    day == "Friday" ? dbDoctor.AppointmentFriday :
                                    day == "Saturday" ? dbDoctor.AppointmentSaturday : dbDoctor.AppointmentSunday) == false
                                    select dbDoctor;
                        int count = query.Count();
                        if (count == 0)
                        {
                            //ui doctor not available
                        }
                        else
                        {
                            var selectedDoctor = query.OrderBy(item => GetDistance(Convert.ToDouble(item.HospitalLatitude), Convert.ToDouble(item.HospitalLongitude), location)).ThenByDescending(freeday => Convert.ToInt16(freeday.AppointmentMonday) + Convert.ToInt16(freeday.AppointmentTuesday) + Convert.ToInt16(freeday.AppointmentWednesday) + Convert.ToInt16(freeday.AppointmentThursday) + Convert.ToInt16(freeday.AppointmentFriday) + Convert.ToInt16(freeday.AppointmentSaturday) + Convert.ToInt16(freeday.AppointmentSunday)).First();
                            DoctorInfo doctorInfo = new DoctorInfo(selectedDoctor.DoctorName, selectedDoctor.DoctorID.GetValueOrDefault(), Convert.ToInt32(selectedDoctor.DoctorAge), selectedDoctor.DoctorPicture, selectedDoctor.DoctorHospital,
                                selectedDoctor.DoctorType == "Psikolog" ? DoctorType.Psikolog : DoctorType.DokterUmum);
                            var healthcare = db.DatabaseUsers.SingleOrDefault(k => k.Nama == selectedDoctor.DoctorHospital);
                            DatabaseRequest databaseRequest = new DatabaseRequest
                            {
                                IsLocked = false,
                                SenderID = userToken.userInfo.UserID,
                                ReceiverID = healthcare.UserID,
                                LocationLatitude = Convert.ToSingle(location.Latitude),
                                LocationLongitude = Convert.ToSingle(location.Longitude),
                                LocationDetail = locationDetail,
                                Request = "Dokter",
                                RequestDetail = selectedDoctor.DoctorID.ToString() + "," + appointmentDate.ToShortDateString() + "," + requestDate.ToShortDateString()
                            };
                            switch (day)
                            {
                                case "Monday":
                                    selectedDoctor.AppointmentMonday = true;
                                    break;
                                case "Tuesday":
                                    selectedDoctor.AppointmentTuesday = true;
                                    break;
                                case "Wednesday":
                                    selectedDoctor.AppointmentWednesday = true;
                                    break;
                                case "Thursday":
                                    selectedDoctor.AppointmentThursday = true;
                                    break;
                                case "Friday":
                                    selectedDoctor.AppointmentFriday = true;
                                    break;
                                case "Saturday":
                                    selectedDoctor.AppointmentSaturday = true;
                                    break;
                                case "Sunday":
                                    selectedDoctor.AppointmentSunday = true;
                                    break;
                            }
                            db.DatabaseRequests.Add(databaseRequest);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //ui error happened
                }
            }
            else
            {
                //textbox login expired to UI
            }
        }
        public static void RequestMedicine(UserToken userToken, GeoCoordinate location, string locationDetail, IEnumerable<Medicine> medicines, Guid pharmacyID, bool onlinePayment)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.Username.Equals(userToken.userInfo.Username))))
            {
                try
                {
                    using (var db = new DatabaseMedAssistEntities())
                    {
                        double total = 0;
                        var selectedPharmacy = db.DatabaseUsers.SingleOrDefault(k => k.UserID == pharmacyID);
                        foreach (var item in medicines)
                        {
                            DatabaseRequest databaseRequest = new DatabaseRequest
                            {
                                IsLocked = false,
                                SenderID = userToken.userInfo.UserID,
                                ReceiverID = selectedPharmacy.UserID,
                                LocationLatitude = Convert.ToSingle(location.Latitude),
                                LocationLongitude = Convert.ToSingle(location.Longitude),
                                LocationDetail = locationDetail,
                                Request = item.MedicineName,
                                RequestDetail = item.Quantity.ToString() + "," + (item.Price * item.Quantity).ToString() + "," + (onlinePayment == true ? "OnlinePayment" : "CashPayment")
                            };
                            total += item.Price * item.Quantity;
                            var reduceStock = db.DatabasePharmacyStocks.SingleOrDefault(k => k.Obat == item.MedicineName);
                            reduceStock.StokObat -= item.Quantity;
                            db.DatabaseRequests.Add(databaseRequest);
                        }
                        if (onlinePayment == true)
                        {
                            var user = db.DatabaseUsers.SingleOrDefault(k => k.UserID == userToken.userInfo.UserID);
                            if (user.SaldoUser < Convert.ToDecimal(total))
                            {
                                //ui not enough balance
                                return;
                            }
                            else
                            {
                                user.SaldoUser -= Convert.ToDecimal(total);
                            }
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    //ui error happened
                }
            }
            else
            {
                //textbox login expired to UI
            }
        }
        public static List<DoctorInfo> GetListOfDoctors(UserToken userToken)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.Username.Equals(userToken.userInfo.Username))))
            {
                try
                {
                    using (var db = new DatabaseMedAssistEntities())
                    {
                        List<DoctorInfo> doctorInfos = new List<DoctorInfo>();
                        var query = from dbDoctor in db.DatabaseDoctors
                                    select dbDoctor;
                        foreach (var item in query)
                        {
                            DoctorInfo doctorInfo = new DoctorInfo(item.DoctorName, item.DoctorID.GetValueOrDefault(), Convert.ToInt32(item.DoctorAge), item.DoctorPicture, item.DoctorHospital,
                                item.DoctorType == "Psikolog" ? DoctorType.Psikolog : DoctorType.DokterUmum);
                            doctorInfos.Add(doctorInfo);
                        }
                        doctorInfos.OrderBy(item => item.DoctorHospital);
                        return doctorInfos;
                    }
                }
                catch (Exception ex)
                {
                    //ui error happened
                    return null;
                }
            }
            else
            {
                //textbox login expired to UI
                return null;
            }
        }
        public static List<Medicine> GetListOfMedicine(UserToken userToken, Guid pharmacyID)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.Username.Equals(userToken.userInfo.Username))))
            {
                try
                {
                    using (var db = new DatabaseMedAssistEntities())
                    {
                        List<Medicine> medicinesInPharmacy = new List<Medicine>();
                        var query = from stocks in db.DatabasePharmacyStocks
                                    where stocks.ApotekID == pharmacyID
                                    select stocks;
                        foreach (var item in query)
                        {
                            var medicineInfo = db.DatabaseMedicines.SingleOrDefault(k => k.NamaObat == item.Obat);
                            Medicine medicine = new Medicine
                            {
                                MedicineName = item.Obat,
                                Quantity = item.StokObat.GetValueOrDefault(),
                                MedicineImage = medicineInfo.GambarObat,
                                MedicineType = medicineInfo.JenisObat,
                                Benefit = medicineInfo.Manfaat,
                                Description = medicineInfo.Keterangan,
                                Price = Convert.ToDouble(medicineInfo.HargaObat)
                            };
                            medicinesInPharmacy.Add(medicine);
                        }
                        return medicinesInPharmacy;
                    }
                }
                catch (Exception ex)
                {
                    //ui error happened
                    return null;
                }
            }
            else
            {
                //textbox login expired to UI
                return null;
            }
        }
        private static double GetDistance(double latitude, double longitude, GeoCoordinate geo)
        {
            GeoCoordinate geoCoordinate = new GeoCoordinate(latitude, longitude);
            return geoCoordinate.GetDistanceTo(geo);
        }
        public static List<DatabaseUser> GetListOfPharmacy(UserToken userToken)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.Username.Equals(userToken.userInfo.Username))))
            {
                try
                {
                    using (var db = new DatabaseMedAssistEntities())
                    {
                        var query = from pharmacy in db.DatabaseUsers
                                    where pharmacy.TipeUser == "Apotek"
                                    select pharmacy;
                        List<DatabaseUser> pharmacies = query.ToList();
                        return pharmacies;
                    }
                }
                catch (Exception ex)
                {
                    //ui error happened
                    return null;
                }
            }
            else
            {
                //textbox login expired to UI
                return null;
            }
        }
    }
}
