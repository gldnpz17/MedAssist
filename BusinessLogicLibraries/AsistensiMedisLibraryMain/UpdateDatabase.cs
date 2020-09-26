using AsistensiMedisDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    public static class UpdateDatabase
    {
        public static void UpdateRequest(DatabaseRequest databaseRequest)
        {
            using(var db = new DatabaseMedAssistEntities())
            {
                var item = db.DatabaseRequests.SingleOrDefault(k => k == databaseRequest);
                string[] requestDetail = item.RequestDetail.Split(',');
                if (item.Request == "Dokter")
                {
                    var doctor = db.DatabaseDoctors.SingleOrDefault(k => k.DoctorName == requestDetail[0]);
                    string requestDay = DateTime.Parse(requestDetail[1]).DayOfWeek.ToString();
                    switch (requestDay)
                    {
                        case "Monday":
                            doctor.AppointmentMonday = false;
                            break;
                        case "Tuesday":
                            doctor.AppointmentTuesday = false;
                            break;
                        case "Wednesday":
                            doctor.AppointmentWednesday = false;
                            break;
                        case "Thursday":
                            doctor.AppointmentThursday = false;
                            break;
                        case "Friday":
                            doctor.AppointmentFriday = false;
                            break;
                        case "Saturday":
                            doctor.AppointmentSaturday = false;
                            break;
                        case "Sunday":
                            doctor.AppointmentSunday = false;
                            break;
                    }
                }
                else if(item.Request=="Ambulans")
                {
                    var ambulance = db.DatabaseAmbulances.SingleOrDefault(k => k.HealthcareID == item.ReceiverID);
                    ambulance.JumlahAmbulans++;
                }
                db.DatabaseRequests.Remove(item);
                db.SaveChanges();
            }
        }
        public static void AddStock(DatabasePharmacyStock stock)
        {
            try
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = db.DatabasePharmacyStocks.SingleOrDefault(k => k.ApotekID == stock.ApotekID && k.Obat == stock.Obat);
                    query.StokObat += stock.StokObat;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void AddAmbulance(DatabaseAmbulance ambulance)
        {
            try
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = db.DatabaseAmbulances.SingleOrDefault(k => k.HealthcareID == ambulance.HealthcareID);
                    query.JumlahAmbulans += ambulance.JumlahAmbulans;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
