using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MedAssistUILibrary.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public int HealthcareUnitId { get; set; }
        public string FullName { get; set; }
        public BitmapImage ProfileImage { get; set; }
        public string ShortBio { get; set; }
        public string Specialization { get; set; }
        public int Status { get; set; }
        public ObservableCollection<DoctorScheduleModel> Schedules { get; set; }
    }
}
