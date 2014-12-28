using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    class SAMPLE_UnrealClassViewModel : UnrealClassViewModel
    {
        public SAMPLE_UnrealClassViewModel() : base()
        {
            this.BaseClasses = new ObservableCollection<BaseClass>
                (
                    new List<BaseClass>()
                    {
                        new BaseClass() { ClassName = "UObject", HeaderFiles = new string[]{"Object.h"}, ReadableName = "Object"},
                        new BaseClass() { ClassName = "Character", HeaderFiles = new string[]{"GameFramework/Character.h"}, ReadableName = "Character"},
                        new BaseClass() { ClassName = "Pawn", HeaderFiles = new string[]{"GameFramework/Pawn.h"}, ReadableName = "Pawn"},
                    }
                );
            this.ClassName = "MyNewClass";
            this.CurrentBaseClass = this.BaseClasses.FirstOrDefault();
            this.Access = "Public";


            this.ClassModel = new UnrealClass();
            this.ClassModel.Access = "Public";
            this.ClassModel.ClassName = ClassName;
            this.ClassModel.GenerateHeader();

            NotifyPropertyChanged("ClassName");
            NotifyPropertyChanged("IsPrivate");
            NotifyPropertyChanged("IsPublic");
            NotifyPropertyChanged("CurrentBaseClass");
        }
    }
}
