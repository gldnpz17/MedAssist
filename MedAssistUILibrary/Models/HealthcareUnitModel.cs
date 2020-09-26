using MedAssistUILibrary.Authorization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistUILibrary.Models
{
    public class HealthcareUnitModel
    {
        public int Id;
        public string Name;

        public ObservableCollection<DoctorModel> Doctors { get; set; }
        public ObservableCollection<HealthcareStaffModel> HealthcareStaffs { get; set; }
    }
}
