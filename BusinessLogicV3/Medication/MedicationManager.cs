using MedAssistBusinessLogic.Authorization;
using MedAssistBusinessLogic.Events;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Media.Imaging;
using MedAssistBusinessLogic;
using System.Collections.ObjectModel;
using System.Linq;
using System.CodeDom.Compiler;
using System.Device.Location;
using System.Data.Entity.Infrastructure;
using MedAssistBusinessLogic.LocationService;
using BusinessLogicV3.Models;
using DataAccessV3;
using DataAccessV3.Models;

namespace MedAssistBusinessLogic.Medication
{
    public class MedicationManager : ManagerBase
    {
        private ILocationService _locationService;

        public MedicationManager(IEventAggregator eventAggregator, ILocationService locationService) : base(eventAggregator)
        {
            _locationService = locationService;
        }

        public ObservableCollection<MedicationModel> GetMedicationsList()
        {
            ObservableCollection<MedicationModel> output = new ObservableCollection<MedicationModel>();
            using (var context = new MedAssistContext())
            {
                var medications = context.Medications.ToList();

                foreach(var medication in medications)
                {
                    output.Add(new MedicationModel()
                    {
                        Id = medication.Id,
                        Name = medication.Name,
                        Description = medication.Description,
                        Image = ConvertByteArrayToBitmapImage(medication.RawImage),
                        Price = medication.Price
                    });
                }
            }
            return output;
        }
        public void Checkout(ObservableCollection<MedicationRequestModel> requests)
        {
            GeoCoordinate coordinates = _locationService.GetLocation();
            using (var context = new MedAssistContext())
            {
                decimal total = 0;
                foreach(var request in requests)
                {
                    decimal medPrice = context.Medications.FirstOrDefault(m => m.Id == request.MedicationId).Price;
                    total += request.Amount * medPrice;
                }
                //check end user's medWallet Balance
                decimal endUserBalance = context.Accounts.FirstOrDefault(a => a.Id == _loggedInUser.Id).MedWalletBalance;
                if(endUserBalance < total)
                {
                    throw new Exception("Insufficient funds.");
                }
                //deduct end user balance
                context.Accounts.FirstOrDefault(a => a.Id == _loggedInUser.Id).MedWalletBalance -= total;
                //add pharmacy balance
                context.Accounts.FirstOrDefault(a => a.IsPharmacy).MedWalletBalance += total;
                //send request & log transaction
                foreach (var request in requests)
                {
                    //log end user transaction
                    decimal medPrice = context.Medications.FirstOrDefault(m => m.Id == request.MedicationId).Price;
                    context.MedWalletTransactions.Add(new MedWalletTransaction
                    {
                        AccountId = _loggedInUser.Id,
                        ChangeInBalance = -1 * request.Amount * medPrice,
                        Description = $"purchased {request.Amount} {context.Medications.FirstOrDefault(m => m.Id == request.MedicationId).Name}(s) for {medPrice} each."
                    });
                    //log pharmacy transaction
                    context.MedWalletTransactions.Add(new MedWalletTransaction
                    {
                        AccountId = context.Accounts.FirstOrDefault(a => a.IsPharmacy).Id,
                        ChangeInBalance = request.Amount * medPrice,
                        Description = $"{_loggedInUser.Username} purchased {request.Amount} {context.Medications.FirstOrDefault(m => m.Id == request.MedicationId).Name}(s) for {medPrice} each."
                    });

                    //add request to the DB
                    context.MedicationRequests.Add(new MedicationRequest() 
                    {
                        AccountId = _loggedInUser.Id,
                        MedicationId = request.MedicationId,
                        Amount = request.Amount,
                        Latitude = coordinates.Latitude,
                        Longitude = coordinates.Longitude,
                        IsDone = false
                    });
                }
                context.SaveChanges();
            }
        }

        public ObservableCollection<MedicationRequestModel> GetMedicationRequestsList()
        {
            ObservableCollection<MedicationRequestModel> output = new ObservableCollection<MedicationRequestModel>();
            using (var context = new MedAssistContext())
            {
                var medicationRequests = from medicationRequest in context.MedicationRequests
                                        where medicationRequest.IsDone == false
                                        select medicationRequest;

                //map models
                foreach (var medicationRequest in medicationRequests)
                {
                    output.Add(new MedicationRequestModel()
                    {
                        Id = medicationRequest.Id,
                        MedicationId = medicationRequest.MedicationId,
                        Amount = medicationRequest.Amount,
                        Latitude = medicationRequest.Latitude,
                        Longitude = medicationRequest.Longitude,
                        IsDone = medicationRequest.IsDone
                    });
                }
            }
            return output;
        }
        public void MarkMedicationRequestAsDone(int medicationRequestId)
        {
            using (var context = new MedAssistContext())
            {
                var request = context.MedicationRequests.FirstOrDefault(a => a.Id == medicationRequestId);
                if (request.IsDone == true)
                {
                    throw new Exception("Request already marked as done.");
                }
                request.IsDone = true;

                context.SaveChanges();
            }
        }

        public void AddMedication(string name, string description, byte[] rawImage, decimal price)
        {
            using (var context = new MedAssistContext())
            {
                context.Medications.Add(new DataAccessV3.Models.Medication() 
                {
                    Name = name,
                    Description = description,
                    RawImage = rawImage,
                    Price = price
                });
                context.SaveChanges();
            }
        }

        public string GetMedicationNameById(int id)
        {
            using (var context = new MedAssistContext())
            {
                return context.Medications.FirstOrDefault(m => m.Id == id)?.Name;
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
