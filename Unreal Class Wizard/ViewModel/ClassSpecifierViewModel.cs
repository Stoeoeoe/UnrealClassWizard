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

        public ClassSpecifierViewModel(List<ClassSpecifier> ClassSpecifiersValues)
        {


            AllSpecifiers = new ObservableCollection<ClassSpecifier>(ClassSpecifiersValues);

            //SpecifierValues = new ObservableCollection<object>();

            //foreach (ClassSpecifier specifier in loadedList)
            //{
            //    SpecifierValues.Add(new object());
            //}

        }
    }
}
