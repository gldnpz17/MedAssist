using BusinessLogicV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicV3.DataBindingModel
{
    public class MedicationRequestBindingModel
    {
        public int Id { get; set; }
        public MedicationModel Medication { get; set; }
        public int Amount { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsDone { get; set; }
    }
}
