using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using AsistensiMedisDatabase;

namespace AsistensiMedisLibraryMain
{
    public static class AuthenticationManager
    {
        public static List<UserToken> SavedUsers { get; set; }

        public static UserToken Authenticate(string username, string password)
        {
            using (var db = new DatabaseMedAssistEntities())
            {
                var query = from user in db.DatabaseUsers
                            where string.Compare(username, user.Username, true) == 0 && user.Password == password
                            select user;
                if(!query.Any())
                {
                    throw new Exception("Incorrect username or password");
                }
            }
            UserToken userToken = new UserToken
            {
                ID = Guid.NewGuid()
            };
            userToken.userInfo.Username = username;
            userToken.userInfo.Password = password;
            var findDupe = from users in SavedUsers
                           where string.Compare(users.userInfo.Username, username, true) == 0
                           select users;
            foreach (var item in findDupe)
            {
                SavedUsers.Remove(item);
            }
            SavedUsers.Add(userToken);
            return userToken;
        }
        public static void RecoverPassword(string emailAddress)
        {
            using (var db = new DatabaseMedAssistEntities())
            {
                var query = from user in db.DatabaseUsers
                            where string.Compare(emailAddress, user.Email, true) == 0
                            select user;
                if (!query.Any())
                {
                    //ui email is not registered
                    return;
                }
            }
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("mochipandan2@gmail.com");
                mail.To.Add(emailAddress);
                mail.Subject = "Recover Password Asistensi Medis";
                var recoveryRandomizer = new int[6];
                Random random = new Random();
                for (int i = 0; i < 6; i++)
                {
                    recoveryRandomizer[i] = random.Next(10);
                }
                string recoveryCode = Convert.ToString(recoveryRandomizer);
                mail.Body = string.Format("Your recovery code for your account in Asistensi Medis is {0}", recoveryCode);
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential("mochipandan2@gmail.com", "myNAME...isCHANG!!1");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
                //pop up with textbox for recovery and recoveryCode to check
                //if success(code match), open textbox to type new password, then update database
                //else reset textbox and allow next attempt(no max, wont send another code)
            }
            catch (Exception ex)
            {
                //ui error happened
            }
        }
    }
}
