using BusinessLogicV3.DataBindingModel;
using BusinessLogicV3.Models;
using DataAccessV3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BusinessLogicV3.BindingModelMapper
{
    public class MedicationRequestMapper
    {
        public MedicationRequestBindingModel MapToBindingModel(MedicationRequestModel model)
        {
            return new MedicationRequestBindingModel()
            {
                Id = model.Id,
                Medication = GetMedicationModelById(model.MedicationId),
                Amount = model.Amount,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                IsDone = model.IsDone
            };
        }
        public MedicationRequestModel MapFromBindingModel(MedicationRequestBindingModel bindingModel)
        {
            return new MedicationRequestModel()
            {
                Id = bindingModel.Id,
                MedicationId = bindingModel.Medication.Id,
                Amount = bindingModel.Amount,
                Latitude = bindingModel.Latitude,
                Longitude = bindingModel.Longitude,
                IsDone = bindingModel.IsDone
            };
        }

        private MedicationModel GetMedicationModelById(int id)
        {
            using (var context = new MedAssistContext())
            {
                var medication = context.Medications.FirstOrDefault(m => m.Id == id);

                return new MedicationModel()
                {
                    Id = medication.Id,
                    Name = medication.Name,
                    Description = medication.Description,
                    Image = ConvertByteArrayToBitmapImage(medication.RawImage),
                    Price = medication.Price
                };
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
