using AsistensiMedisDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    public class Request
    {
        public int ID;
        public Boolean IsLocked = false;
        public string SenderName;
        public string ReceiverName;
        public string LocationDetail;

        public void Cancel(UserToken userToken, string selection, string detail)
        {
            if (userToken.ID.Equals(AuthenticationManager.SavedUsers.Find(item => item.userInfo.UserID.Equals(userToken.userInfo.UserID))))
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = db.DatabaseRequests.SingleOrDefault(k => k.SenderID == userToken.userInfo.UserID && k.Request == selection && k.RequestDetail == detail && k.IsLocked == false);
                    if (query != null)
                    {
                        if (query.Request != "Ambulans" && query.Request != "Dokter")
                        {
                            string[] medicineDetail = query.RequestDetail.Split(',');
                            var cancelledMedicine = db.DatabasePharmacyStocks.SingleOrDefault(k => k.ApotekID == query.ReceiverID && k.Obat == query.Request);
                            cancelledMedicine.StokObat += Convert.ToInt32(medicineDetail[0]);
                        }
                        UpdateDatabase.UpdateRequest(query);
                    }
                    else
                    {
                        //textbox request cannot be canceled to UI
                    }
                }
            }
            else
            {
                //textbox login expired to UI
            }
        }
    }
}
