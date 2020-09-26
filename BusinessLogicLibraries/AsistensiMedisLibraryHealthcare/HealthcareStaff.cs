using AsistensiMedisDatabase;
using AsistensiMedisLibraryMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryHealthcare
{
    public static class HealthcareStaff
    {
        public static void StartListeningForAmbulanceRequests(UserToken userToken)
        {
            using (var db = new DatabaseMedAssistEntities())
            {
                var authority = from check in db.DatabaseUsers
                                where check.UserID == userToken.userInfo.UserID && check.TipeUser == "RumahSakit"
                                select check;
                if(authority.Any())
                {
                    var query = from request in db.DatabaseRequests
                                where request.Request == "Ambulance" && request.ReceiverID == userToken.userInfo.UserID
                                select request;
                    var ambulanceCount = db.DatabaseAmbulances.SingleOrDefault(k => k.HealthcareID == userToken.userInfo.UserID);
                    int ambulanceLeft = ambulanceCount.JumlahAmbulans.GetValueOrDefault();
                    int count = query.Count();
                    foreach (var item in query)
                    {
                        if(ambulanceLeft > 0)
                        {
                            item.IsLocked = true;
                            ambulanceLeft--;
                            count--;
                        }
                        if(ambulanceLeft == 0 || count == 0)
                        {
                            //fill list on ui based on items in query
                            db.SaveChanges();
                            break;
                        }
                    }
                }
                else
                {
                    throw new UnauthorizedException("Staf Pusat Kesehatan");
                }
            }
        }
        public static void StartListeningForDoctorRequests(UserToken userToken)
        {
            using (var db = new DatabaseMedAssistEntities())
            {
                var query = from request in db.DatabaseRequests
                            where request.Request == "Dokter" && request.ReceiverID == userToken.userInfo.UserID
                            select request;
                foreach(var item in query)
                {
                    item.IsLocked = true;
                }
                db.SaveChanges();
                //fill list on ui based on query
            }
        }
    }
}
