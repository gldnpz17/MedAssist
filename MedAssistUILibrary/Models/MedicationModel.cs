using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace UILibrary.Models
{
    public class MedicationModel
    {
        public int Id { get; set; }
        public BitmapImage Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
