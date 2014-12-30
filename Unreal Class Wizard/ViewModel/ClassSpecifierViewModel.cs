using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    public class ClassSpecifierViewModel : NotifyPropertyChangedBase
    {
        public ObservableCollection<ClassSpecifier> AllSpecifiers {get;set;}
        public ObservableCollection<object> SpecifierValues { get; set; } 

        public ClassSpecifierViewModel()
        {

            List<ClassSpecifier> loadedList = ClassSpecifier.LoadClassSpecifiers();
            AllSpecifiers = new ObservableCollection<ClassSpecifier>(loadedList);

            SpecifierValues = new ObservableCollection<object>();
            //// TODO: Hacky stuff. Preload list with empty values

            foreach (ClassSpecifier specifier in loadedList)
            {
                SpecifierValues.Add(new object());
            }

        }
    }
}
