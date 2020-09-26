using AsistensiMedisDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    public class EditUser
    {
        public static DatabaseUser FindUser(UserToken userToken)
        {
            try
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = db.DatabaseUsers.SingleOrDefault(k => k.UserID == userToken.userInfo.UserID);
                    return query;
                }
            }
            catch (Exception ex)
            {
                //ui error happened
                return null;
            }
        }
        public static void ChangeUserInfo(UserToken userToken, DatabaseUser user)
        {
            try
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = db.DatabaseUsers.SingleOrDefault(k => k.UserID == userToken.userInfo.UserID);
                    query = user;
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
