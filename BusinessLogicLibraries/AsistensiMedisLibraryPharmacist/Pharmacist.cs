using AsistensiMedisDatabase;
using AsistensiMedisLibraryMain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryPharmacist
{
    public class Pharmacist
    {
        public static List<MedicineRequest> StartListeningForMedicineRequests(UserToken userToken)
        {
            try
            {
                using (var db = new DatabaseMedAssistEntities())
                {
                    var query = from request in db.DatabaseRequests
                                where request.ReceiverID == userToken.userInfo.UserID
                                select request;
                    var medicine = from requests in query
                                   group requests by requests.SenderID;
                    List<Medicine> medicines = new List<Medicine>();
                    List<MedicineRequest> medicineRequests = new List<MedicineRequest>();
                    int id = 1;
                    foreach (var item in medicine)
                    {
                        var sender = db.DatabaseUsers.SingleOrDefault(k => k.UserID == item.Key);
                        MedicineRequest medicineRequest = new MedicineRequest
                        {
                            ID = id,
                            IsLocked = true,
                            SenderName = sender.Nama,
                        };
                        int currentItem = 0;
                        foreach (var request in item)
                        {
                            if (currentItem == 0)
                            {
                                GeoCoordinate requestLocation = new GeoCoordinate(Convert.ToDouble(request.LocationLatitude), Convert.ToDouble(request.LocationLongitude));
                                medicineRequest.Location = requestLocation;
                                medicineRequest.LocationDetail = request.LocationDetail;
                            }
                            string[] details = request.RequestDetail.Split(',');
                            Medicine order = new Medicine
                            {
                                MedicineName = request.Request,
                                Quantity = Convert.ToInt32(details[0]),
                                Price = Convert.ToDouble(details[1])
                            };
                            medicines.Add(order);
                            currentItem++;
                            var result = db.DatabaseRequests.SingleOrDefault(k => k.SenderID == item.Key && k.Request == request.Request);
                            result.IsLocked = true;
                        }
                        medicineRequest.Medicines = medicines;
                        medicineRequests.Add(medicineRequest);
                    }
                    db.SaveChanges();
                    return medicineRequests;
                }
            }
            catch (Exception ex)
            {
                //ui error happened
                return null;
            }
        }
    }
}
