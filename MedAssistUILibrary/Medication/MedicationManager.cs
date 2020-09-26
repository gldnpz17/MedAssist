using UILibrary.Authorization;
using UILibrary.Events;
using UILibrary.Models;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Media.Imaging;
using MedAssistUILibrary.Authorization;
using MedAssistUILibrary;
using System.Collections.ObjectModel;
using NewDataAccessLibrary;
using System.Linq;
using NewDataAccessLibrary.Models;
using System.CodeDom.Compiler;
using System.Device.Location;
using System.Data.Entity.Infrastructure;

namespace UILibrary.Medication
{
    public class MedicationManager : ManagerBase
    {
        private ILocationService _locationService;

        public MedicationManager(IEventAggregator eventAggregator, ILocationService locationService) : base(eventAggregator)
        {

        }

        public ObservableCollection<Models.MedicationModel> GetMedicationsList()
        {
            ObservableCollection<Models.MedicationModel> outputs = new ObservableCollection<Models.MedicationModel>();

            using (var context = new MedAssistContext())
            {
                var query = from medication in context.Medications
                            select medication;

                foreach(var item in query)
                {
                    outputs.Add(new Models.MedicationModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        Image = ConvertByteArrayToBitmapImage(item.RawImage),
                        Price = item.Price
                    });
                }
            }

            return outputs;
        }
        public List<PharmacyModel> GetPharmacyByMedication(int id)
        {
            return new List<PharmacyModel>() { 
                new PharmacyModel { 
                    Id = 0, 
                    Name = "PharmacyA" 
                },
                new PharmacyModel {
                    Id = 1,
                    Name = "PharmacyB"
                },
                new PharmacyModel {
                    Id = 2,
                    Name = "PharmacyC"
                }
            };
        }

        public void Checkout(CartModel cart)
        {
            //check request validity
            //check if the pharmacy has enough in stock
            using (var context = new MedAssistContext())
            {
                //compare pharmacy stocks
                foreach(var item in cart.Items)
                {
                    //get the pharmacy's stock
                    var query = from stock in context.MedicationStocks
                                where (stock.PharmacyId == item.Pharmacy.Id) && (stock.Medication.Id == item.Item.Id)
                                select stock;
                    int pharmacyAmount = query.ToList().First().Amount;

                    if(pharmacyAmount < item.Amount)
                    {
                        //request invalid
                        throw new Exception("Insufficient Stocks");
                    }
                }
            }
            //check if the user has enough balance
            decimal cartTotal = 0m;
            foreach(var item in cart.Items)
            {
                cartTotal += item.Amount * item.Item.Price;
            }
            using (var context  = new MedAssistContext())
            {
                var endUser = context.EndUsers.FirstOrDefault(e => (e.UserId == _loggedInUser.Id));

                decimal medWalletBalance = endUser.MedWalletBalance;

                if(medWalletBalance < cartTotal)
                {
                    throw new Exception("Insufficient funds.");
                }
            }

            //perform transaction
            foreach(var item in cart.Items)
            {
                using (var context = new MedAssistContext())
                {
                    //subtract pharmacy stocks
                    var stock = context.MedicationStocks.FirstOrDefault(s => (s.PharmacyId == item.Pharmacy.Id) && (s.Medication.Id == item.Item.Id));
                    stock.Amount -= item.Amount;
                    context.SaveChanges();
                    //notify pharmacy
                    GeoCoordinate currentCoords = _locationService.GetLocation();
                    context.MedicationRequests.Add(new MedicationRequest()
                    {
                        PharmacyId = item.Pharmacy.Id,
                        UserId = _loggedInUser.Id,

                        MedicationId = item.Item.Id,
                        Amount = item.Amount,
                        Latitude = currentCoords.Latitude,
                        Longitude = currentCoords.Longitude
                    });
                    context.SaveChanges();

                    //subtract endUser medWallet
                    var endUser = context.EndUsers.FirstOrDefault(e => (e.UserId == _loggedInUser.Id));
                    endUser.MedWalletBalance -= item.Amount * item.Item.Price;
                    //log medWallet transaction
                    context.MedWalletTransactions.Add(new MedWalletTransaction()
                    {
                        EndUserId = endUser.Id,
                        ChangeInAmount = -1m * item.Amount * item.Item.Price,
                        Description = $"purchased {item.Amount} {item.Item.Name} for {item.Item.Price} each.",
                    });
                    context.SaveChanges();
                }
            }
        }

        private BitmapImage ConvertByteArrayToBitmapImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0) return null;
            BitmapImage image = new BitmapImage();
            using (var mem = new MemoryStream(imageBytes))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
