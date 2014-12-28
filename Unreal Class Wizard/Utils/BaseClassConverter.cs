using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.Utils
{
     /// <summary>
     /// Makes sure that new, not yet defined base classes do not lead to NPE.
     /// </summary>
    public class BaseClassConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BaseClass baseClass = new BaseClass();
            baseClass.ClassName = value as string;
            return baseClass;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BaseClass baseClass = new BaseClass();
            baseClass.ClassName = (value as BaseClass).ClassName;
            return baseClass;
        }
    }
}
