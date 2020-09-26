using AsistensiMedisDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryHealthcare
{
    public class EditDoctor
    {
        public static DatabaseDoctor FindDoctor(string name, string hospital)
        {
            try
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = db.DatabaseDoctors.SingleOrDefault(k => k.DoctorName.Contains(name) && k.DoctorHospital == hospital);
                    return query;
                }
            }
            catch (Exception ex)
            {
                //ui error happened
                return null;
            }
        }
        public static void ChangeDoctorInfo(DatabaseDoctor doctor, string name, string hospital)
        {
            try
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = db.DatabaseDoctors.SingleOrDefault(k => k.DoctorName.Contains(name) && k.DoctorHospital == hospital);
                    query = doctor;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //ui error happened
            }
        }
    }
}
