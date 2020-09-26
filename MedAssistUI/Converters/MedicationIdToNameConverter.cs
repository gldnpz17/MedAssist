using MedAssistBusinessLogic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MedAssistUI.Converters
{
    public class MedicationIdToNameConverter : IValueConverter
    {
        private MedAssistBLLibrary lib = new MedAssistBLLibrary();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return lib.Medication.GetMedicationNameById((int)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }
    }
}
