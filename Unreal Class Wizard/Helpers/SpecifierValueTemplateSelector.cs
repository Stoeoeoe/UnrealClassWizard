using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.Helpers
{
    public class SpecifierValueTemplateSelector : DataTemplateSelector
    {

        public DataTemplate TextBoxDataTemplate { get; set; }
        public DataTemplate BooleanDataTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ClassSpecifier specifier = item as ClassSpecifier;
            if (specifier.Type == "Boolean")
            {
                return BooleanDataTemplate;
            }
            if (specifier.Type == "String")
            {
                return TextBoxDataTemplate;
            }

            return BooleanDataTemplate;
        }
    
    }
}
