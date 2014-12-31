using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.Helpers
{
    public class ClassSpecifierEventArgs : RoutedEventArgs
    {
        public List<ClassSpecifier> ClassSpecifiers { get; set; }
    }
}
