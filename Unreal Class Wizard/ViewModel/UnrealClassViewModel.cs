using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    public class UnrealClassViewModel : BaseViewModel
    {
        public UnrealClassViewModel(bool isDesignMode)
        {
            // Run application (not design time)
            if (isDesignMode == false)
            {

                // Fill BaseClass dropdown
                this.BaseClasses = new ObservableCollection<BaseClass>(BaseClass.AllBaseClasses);              

                this.ClassModel = App.CurrentClass;

                this.Access = ClassModel.Access;
                this.ClassName = ClassModel.ClassName;
                this.ClassSpecifiers = new ObservableCollection<ClassSpecifier>(ClassModel.ClassSpecifiers);

                this.Description = ClassModel.Description;
                this.IncludedClasses = new ObservableCollection<string>(ClassModel.IncludedClasses);

                this.CurrentBaseClass = ClassModel.BaseClass;
                this.CurrentBaseClassIndex = BaseClasses.IndexOf(ClassModel.BaseClass);
                this.CurrentBaseClassText = CurrentBaseClass.ReadableName;

                this.UseAPI = ClassModel.UseAPI;
                this.API = ClassModel.API;
                this.IsActor = ClassModel.IsActor;

                this.ClassSpecifiers = new ObservableCollection<ClassSpecifier>(ClassModel.ClassSpecifiers);
                UpdateClassSpecifiers(ClassModel.ClassSpecifiers);

                this.ClassModel.GeneratePreviews(this.GetType());
            }
            // Design time
            else
            {

            }

        }




        private string className;
        public string ClassName
        {
            get { return className; }
            set
            {
                className = value.Trim();
                ClassModel.ClassName = className;
                NotifyPropertyChanged("ClassName");
            }
        }

        
        private string currentBaseClassText;
        public string CurrentBaseClassText
        {
            get
            {
                return currentBaseClassText;
            }
            set
            {
                if(value != null)
                {
                    currentBaseClassText = value.Trim();
                    NotifyPropertyChanged("CurrentBaseClassText");
                    if (CurrentBaseClassIndex == -1)
                    {
                        CurrentBaseClass = new BaseClass(currentBaseClassText, true);
                        NotifyPropertyChanged("CurrentBaseClass");
                    }

                }
            }
        }

        private int currentBaseClassIndex;

        public int CurrentBaseClassIndex
        {
            get { return currentBaseClassIndex; }
            set
            {
                currentBaseClassIndex = value;
                // Get existing class if a value has been selected, otherwise create a new one
                if (CurrentBaseClassIndex > -1)
                {
                    CurrentBaseClass = BaseClasses[currentBaseClassIndex];
                    NotifyPropertyChanged("CurrentBaseClass");
                }

            }
        }


        private BaseClass currentBaseClass;
        public BaseClass CurrentBaseClass
        {
            get { return currentBaseClass; }
            set {
                    currentBaseClass = value;
                    ClassModel.BaseClass = currentBaseClass;

                    // Read the "IsActor" property from base class only if the class is not generated (not in XML) and has not been overwritten
                    if(ClassModel.BaseClass.IsGenerated == false && ClassModel.BaseClass.IsActorClass == ClassModel.IsActor)
                    {
                        IsActor = currentBaseClass.IsActorClass;
                        ClassModel.IsActor = IsActor;
                        NotifyPropertyChanged("IsActor");
                    }
                    

                    NotifyPropertyChanged("CurrentBaseClass");
                    
                    // If the base class is "Actor", activate the checkbox
            }
        }

        private string access;
        public string Access
        {
            get { return access; }
            set
            {
                access = value;
                ClassModel.Access = access;
                NotifyPropertyChanged("Access");
            }
        }

        private string api;

        public string API
        {
            get { return api; }
            set
            {
                api = value.Trim();
                ClassModel.API = api;
                NotifyPropertyChanged("API");

            }
        }


        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value.Trim(new char[' ']);
                ClassModel.Description = description;
                NotifyPropertyChanged("Description");
            }
        }

        private string includedClassesString;
        public string IncludedClassesString
        {
            get { return includedClassesString; }
            set
            {
                includedClassesString = value.Trim();
                ClassModel.IncludedClasses.Clear();
                // Split up string
                string[] splitUpIncludes = includedClassesString.Split(new string[]{";"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string include in splitUpIncludes)
                {
                    string includeH = include.EndsWith(".h") ? include.Trim() : include.Trim() + ".h" ;
                    ClassModel.IncludedClasses.Add(includeH.Trim());
                    
                }

                NotifyPropertyChanged("AdditionalIncludedClasses");
            }
        }

        


        private bool isActor;
        public bool IsActor
        {
            get { return isActor; }
            set
            {
                isActor = value;
                ClassModel.IsActor = isActor;
                if (ClassModel.ConstructorArguments.Contains("const FObjectInitializer& ObjectInitializer") == true && isActor == false)
                {
                    ClassModel.ConstructorArguments.Remove("const FObjectInitializer& ObjectInitializer");
                }
                else if (ClassModel.ConstructorArguments.Contains("const FObjectInitializer& ObjectInitializer")  == false && isActor == true)
                {
                    ClassModel.ConstructorArguments.Add("const FObjectInitializer& ObjectInitializer");
                }

                NotifyPropertyChanged("IsActor");
            }
        }

        private bool isAbstract;
        public bool IsAbstract
        {
            get { return isAbstract; }
                // isBlueprintable = value;
             set{
                 isAbstract = value;

                 ClassModel.ClassSpecifiers.SingleOrDefault(specifier => specifier.Name.ToLower() == "abstract").Value = isAbstract;

                 ClassModel.GeneratePreviews(this.GetType());
                 NotifyPropertyChanged("ClassSpecifiers");
                 NotifyPropertyChanged("IsAbstract");
                }
        }

        private bool isBlueprintable;
        public bool IsBlueprintable
        {
            get { return isBlueprintable; }
            set
                {
                isBlueprintable = value;

                ClassModel.ClassSpecifiers.SingleOrDefault(specifier => specifier.Name.ToLower() == "blueprintable").Value = isBlueprintable;

                ClassModel.GeneratePreviews(this.GetType());
                NotifyPropertyChanged("ClassSpecifiers");
                NotifyPropertyChanged("IsBlueprintable");
                }
        }

        private bool useAPI;

        public bool UseAPI
        {
            get { return useAPI; }
            set 
            {
                useAPI = value;
                ClassModel.UseAPI = useAPI;
                NotifyPropertyChanged("UseAPI");
            }
        }





        private ObservableCollection<string> includedClasses;
        public ObservableCollection<string> IncludedClasses
        {
            get
            {
                return includedClasses;
            }
            set
            {
                includedClasses = value;
                NotifyPropertyChanged("IncludedClasses");
            
                IncludedClassesString = string.Join(";", includedClasses.ToArray());
            }
        }

        private ObservableCollection<ClassSpecifier> classSpecifiers;
        public ObservableCollection<ClassSpecifier> ClassSpecifiers
        {
            get
            {
                return classSpecifiers;
            }
            set
            {
                classSpecifiers = value;
                ClassModel.ClassSpecifiers = classSpecifiers.ToList<ClassSpecifier>();
                NotifyPropertyChanged("ClassSpecifiers");
            }
        }

        
        private ObservableCollection<BaseClass> baseClasses = new ObservableCollection<BaseClass>();

        public ObservableCollection<BaseClass> BaseClasses
        {
            get 
            {
                return baseClasses;
            }
            set
            {
                baseClasses = value;
                NotifyPropertyChanged("BaseClasses");
            }
        }





        public void UpdateClassSpecifiers(List<ClassSpecifier> newSpecifiers)
        {
            ClassSpecifiers = new ObservableCollection<ClassSpecifier>(newSpecifiers);
            NotifyPropertyChanged("ClassSpecifiers", true);

            // There are two special cases, abstract and blueprintable which are handled separately
            ClassSpecifier abstractClassSpecifier = ClassSpecifiers.SingleOrDefault(specifier => specifier.Name.ToLower() == "abstract");
            ClassSpecifier blueprintableClassSpecifier = ClassSpecifiers.SingleOrDefault(specifier => specifier.Name.ToLower() == "blueprintable");

            if (blueprintableClassSpecifier != null && blueprintableClassSpecifier.Value != null)
            {
                IsBlueprintable = (bool)blueprintableClassSpecifier.Value;
            }
            if (abstractClassSpecifier != null && abstractClassSpecifier.Value != null)
            {
                IsAbstract = (bool)abstractClassSpecifier.Value;
            }

        }
    }
}
