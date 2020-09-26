using AsistensiMedisDatabase;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    public static class Register
    {
        public static void RegisterUser(UserInfo userInfo, string alamat, string tempatLahir, DateTime tanggalLahir, string nomorTelepon, GeoCoordinate geoCoordinate)
        {
            using (var db = new DatabaseMedAssistEntities())
            {
                DatabaseUser databaseUser = new DatabaseUser
                {
                    UserID = userInfo.UserID,
                    Nama = userInfo.Name,
                    Username = userInfo.Username,
                    Password = userInfo.Password,
                    Email = userInfo.EmailAddress,
                    FotoProfil = userInfo.ProfileImage,
                    TipeUser = userInfo.tipeUser.ToString(),
                    Alamat = alamat,
                    TempatLahir = tempatLahir,
                    TanggalLahir = tanggalLahir,
                    NomorTelepon = Convert.ToInt32(nomorTelepon),
                };
                if(geoCoordinate!=null)
                {
                    databaseUser.LatitudeUser = Convert.ToSingle(geoCoordinate.Latitude);
                    databaseUser.LongitudeUser = Convert.ToSingle(geoCoordinate.Longitude);
                }
                db.DatabaseUsers.Add(databaseUser);
                if(databaseUser.TipeUser=="RumahSakit")
                {
                    DatabaseAmbulance databaseAmbulance = new DatabaseAmbulance
                    {
                        HealthcareID = userInfo.UserID,
                        JumlahAmbulans = 0,
                        HealthcareLatitude = Convert.ToSingle(geoCoordinate.Latitude),
                        HealthcareLongitude = Convert.ToSingle(geoCoordinate.Longitude)
                    };
                    db.DatabaseAmbulances.Add(databaseAmbulance);
                }
                else if(databaseUser.TipeUser == "Apotek")
                {
                    var query = from medicines in db.DatabaseMedicines
                                select medicines;
                    foreach(var item in query)
                    {
                        DatabasePharmacyStock databasePharmacyStock = new DatabasePharmacyStock
                        {
                            ApotekID = userInfo.UserID,
                            Obat = item.NamaObat,
                            StokObat = 0
                        };
                        db.DatabasePharmacyStocks.Add(databasePharmacyStock);
                    }
                }
                db.SaveChanges();
            }
        }
        public static void RegisterDoctor(DoctorInfo doctorInfo, bool[] workday)
        {
            using(var db = new DatabaseMedAssistEntities())
            {
                DatabaseDoctor databaseDoctor = new DatabaseDoctor
                {
                    DoctorID = doctorInfo.DoctorID,
                    DoctorName = doctorInfo.DoctorName,
                    DoctorAge = doctorInfo.DoctorAge,
                    DoctorHospital = doctorInfo.DoctorHospital,
                    DoctorPicture = doctorInfo.DoctorPicture,
                    DoctorType = doctorInfo.doctorType.ToString(),
                    AppointmentMonday = workday[0],
                    AppointmentTuesday = workday[1],
                    AppointmentWednesday = workday[2],
                    AppointmentThursday = workday[3],
                    AppointmentFriday = workday[4],
                    AppointmentSaturday = workday[5],
                    AppointmentSunday = workday[6]
                };
                db.DatabaseDoctors.Add(databaseDoctor);
                db.SaveChanges();
            }
        }
    }
}
