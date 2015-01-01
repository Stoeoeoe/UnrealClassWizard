using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Unreal_Class_Wizard.Helpers
{
    [ValueConversion(typeof(Nullable<Boolean>), typeof(String))]
    public class ClassSpecifierValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if(value == null)
            {
                if (targetType == typeof(string))
                {
                    return "";
                }
                else if (targetType == typeof(Nullable<Boolean>))
                {
                    return false;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
