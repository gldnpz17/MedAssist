using AsistensiMedisDatabase;
using AsistensiMedisLibraryMain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryPatient
{
    public static class RedeemVoucher
    {
        public static void RedeemCode(string voucherCode, UserToken userToken)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.Username.Equals(userToken.userInfo.Username))))
            {
                try
                {
                    using (var db = new DatabaseMedAssistEntities())
                    {
                        var query = db.DatabaseVouchers.SingleOrDefault(item => item.VoucherID.ToString() == voucherCode);
                        if(query!=null)
                        {
                            var user = db.DatabaseUsers.SingleOrDefault(item => item.UserID == userToken.userInfo.UserID);
                            user.SaldoUser += query.Nominal;
                            db.DatabaseVouchers.Remove(query);
                            db.SaveChanges();
                        }
                        else
                        {
                            //ui code not found
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
    }
}
