using System;
using System.Collections.Generic;
using System.Text;

namespace UILibrary.Models
{
    public class CartItemModel
    {
        public MedicationModel Item { get; set; } = new MedicationModel();
        public int Amount { get; set; }
        public PharmacyModel Pharmacy { get; set; }
    }
}
