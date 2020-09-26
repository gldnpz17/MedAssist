using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace MedAssistUI.Converters
{
    /// <summary>
    /// Combines two passwordbox into an array of passwordboxes
    /// </summary>
    class TwoPasswordBoxConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new PasswordBox[2] { (PasswordBox)values[0], (PasswordBox)values[1] };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            PasswordBox[] arr = (PasswordBox[])value;
            return new object[2] { arr[0], arr[1] };
        }
    }
}
