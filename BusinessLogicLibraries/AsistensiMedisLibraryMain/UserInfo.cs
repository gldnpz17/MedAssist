using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsistensiMedisDatabase;

namespace AsistensiMedisLibraryMain
{
    public class UserInfo
    {
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public byte[] ProfileImage { get; set; }
        public TipeUser tipeUser { get; set; }

        public void ChangePassword(string newPassword)
        {
            using(var db = new DatabaseMedAssistEntities())
            {
                var query = from users in db.DatabaseUsers
                            where string.Compare(Username, users.Username, true) == 0
                            select users;
                foreach(var item in query)
                {
                    item.Password = newPassword;
                }
                db.SaveChanges();
            }
            Password = newPassword;
        }
        public void ChangeProfileImage(byte[] newProfileImage)
        {
            using (var db = new DatabaseMedAssistEntities())
            {
                var query = from users in db.DatabaseUsers
                            where string.Compare(Username, users.Username, true) == 0
                            select users;
                foreach (var item in query)
                {
                    item.FotoProfil = newProfileImage;
                }
                db.SaveChanges();
            }
            ProfileImage = newProfileImage;
        }
    }
}
