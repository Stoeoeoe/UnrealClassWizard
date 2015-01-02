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
    public class UnrealClassViewModel : NotifyPropertyChangedBase
    {
        public UnrealClassViewModel()
        {
            // Fill BaseClass dropdown
            BaseClasses = new ObservableCollection<BaseClass>(BaseClass.AllBaseClasses);

            ClassModel = App.CurrentClass;


            CurrentBaseClass = ClassModel.BaseClass;
            if (BaseClasses.Count > 0)
            {
                this.CurrentBaseClassText = BaseClasses.FirstOrDefault().ClassName;
                NotifyPropertyChanged("CurrentBaseClass");
                NotifyPropertyChanged("CurrentBaseClassText");

            }
            else
            {
                this.CurrentBaseClass = new BaseClass();
                this.CurrentBaseClassText = "";
                NotifyPropertyChanged("CurrentBaseClass");
                NotifyPropertyChanged("CurrentBaseClassText");
            }


            UpdateClassSpecifiers(ClassSpecifier.LoadClassSpecifiers());

            this.Access = "Public";
            NotifyPropertyChanged("Access");

            this.classModel.IncludedClasses = new List<string>();

        }

        private UnrealClass classModel;
        public UnrealClass ClassModel
        {
            get { return classModel; }
            set { classModel = value; }
        }



        private string className;
        public string ClassName
        {
            get { return className; }
            set
            {
                className = value.Trim();
                classModel.ClassName = className;
                NotifyPropertyChanged("ClassName");
                NotifyPropertyChanged("PreviewHeader");
            }
        }

        //private string currentBaseClassText;

        //public string CurrentBaseClassText
        //{
        //    get { return currentBaseClassText; }
        //    set
        //    {
        //        currentBaseClassText = value;
        //        // Search for the base class if it exists, otherwise create a new one
        //        BaseClass existingBaseClass = BaseClasses.FirstOrDefault(baseClass => baseClass.ReadableName == currentBaseClassText);
        //        if(existingBaseClass != null)
        //        {
        //            CurrentBaseClass = existingBaseClass;
        //        }
        //        else
        //        {
        //            CurrentBaseClass = new BaseClass();
        //            CurrentBaseClass.ClassName = currentBaseClassText;
        //        }
        //        NotifyPropertyChanged("CurrentBaseClassText");
        //        NotifyPropertyChanged("CurrentBaseClass");
        //    }
        //}
        
        private string currentBaseClassText;
        public string CurrentBaseClassText
        {
            get {
                if(currentBaseClassText == null)
                {
                    return currentBaseClassText;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                currentBaseClassText = value.Trim();
                NotifyPropertyChanged("CurrentBaseClassText");
            }
        }

        private BaseClass currentBaseClass;
        public BaseClass CurrentBaseClass
        {
            get { return currentBaseClass; }
            set {
                    // TODO: Not pretty at all
                    // The BaseClassConverted created an empty BaseClass which lacks information, so it must be retrieved from the collection again
                    if(value == null)
                    {
                        currentBaseClass = new BaseClass();
                        currentBaseClass.ClassName = CurrentBaseClassText;
                    }
                    else
                    {
                        currentBaseClass = value;
                        CurrentBaseClassText = currentBaseClass.ToString();
                    }
                    //BaseClass existingBaseClass = BaseClasses.FirstOrDefault(b => b.ClassName == (value as BaseClass).ClassName);

                    //currentBaseClass = existingBaseClass == null ? value : existingBaseClass;
                    //classModel.BaseClass = currentBaseClass;

                    classModel.BaseClass = currentBaseClass;    

                    NotifyPropertyChanged("CurrentBaseClass");
                    NotifyPropertyChanged("PreviewHeader");
                    // If the base class is "Actor", activate the checkbox
                    IsActor = currentBaseClass.IsActorClass;
            }
        }

        private string access;
        public string Access
        {
            get { return access; }
            set
            {
                access = value;
                classModel.Access = access;
                NotifyPropertyChanged("Access");
                NotifyPropertyChanged("PreviewHeader");
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                classModel.Description = description;
                NotifyPropertyChanged("Description");
                NotifyPropertyChanged("PreviewHeader");
            }
        }

        private string additionalIncludedClasses;
        public string AdditionalIncludedClasses
        {
            get { return additionalIncludedClasses; }
            set
            {
                additionalIncludedClasses = value;
                classModel.IncludedClasses.Clear();
                // Split up string
                string[] splitUpIncludes = additionalIncludedClasses.Split(new string[]{";"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (string include in splitUpIncludes)
                {
                    classModel.IncludedClasses.Add(include.Trim());
                }

                classModel.GeneratePreviews();                                // Trigger manual generation because we do not set the value
                NotifyPropertyChanged("AdditionalIncludedClasses");
                NotifyPropertyChanged("PreviewHeader");
            }
        }

        


        private bool isActor;
        public bool IsActor
        {
            get { return isActor; }
            set
            {
                isActor = value;
                classModel.IsActor = isActor;
                NotifyPropertyChanged("IsActor");
                NotifyPropertyChanged("PreviewHeader");
            }
        }

        private bool isAbstract;
        public bool IsAbstract
        {
            get { return isAbstract; }
                // isBlueprintable = value;
             set{
                 isAbstract = value;

                 classModel.ClassSpecifiersValues.SingleOrDefault(specifier => specifier.Name.ToLower() == "abstract").Value = isAbstract;
                 
                 classModel.GeneratePreviews();
                 NotifyPropertyChanged("ClassSpecifiers");
                 NotifyPropertyChanged("IsAbstract");
                 NotifyPropertyChanged("PreviewHeader");
                }
        }

        private bool isBlueprintable;
        public bool IsBlueprintable
        {
            get { return isBlueprintable; }
            set
                {
                isBlueprintable = value;

                classModel.ClassSpecifiersValues.SingleOrDefault(specifier => specifier.Name.ToLower() == "blueprintable").Value = isBlueprintable;
                
                classModel.GeneratePreviews();
                NotifyPropertyChanged("ClassSpecifiers");
                NotifyPropertyChanged("IsBlueprintable");
                NotifyPropertyChanged("PreviewHeader");
                }
        }

        private string previewHeader;
        public string PreviewHeader
        {
            get { return ClassModel.HeaderText; }
            set {
                previewHeader = value;
                NotifyPropertyChanged("PreviewHeader");            
            }
        }

        private string previewCPP;
        public string PreviewCPP
        {
            get { return ClassModel.CPPText; }
            set
            {
                previewCPP = value;
                NotifyPropertyChanged("PreviewCPP");
            }
        }

        private ObservableCollection<string> includedClasses;
        public ObservableCollection<string> IncludedClasses
        {
            get { return new ObservableCollection<string>(ClassModel.IncludedClasses); }
            set
            {
                includedClasses = value;

                NotifyPropertyChanged("IncludedClasses");
                NotifyPropertyChanged("PreviewHeader");
            }
        }

        private ObservableCollection<ClassSpecifier> classSpecifiers;
        public ObservableCollection<ClassSpecifier> ClassSpecifiers
        {
            get { return new ObservableCollection<ClassSpecifier>(ClassModel.ClassSpecifiersValues); }
            set
            {
                classSpecifiers = value;
                classModel.ClassSpecifiersValues = classSpecifiers.ToList<ClassSpecifier>();
                NotifyPropertyChanged("ClassSpecifiers");
                NotifyPropertyChanged("PreviewHeader");
                classModel.GeneratePreviews();
            }
        }

        
        private ObservableCollection<BaseClass> baseClasses;

        public ObservableCollection<BaseClass> BaseClasses
        {
            get 
            {
                if(baseClasses == null)
                {
                    BaseClasses = new ObservableCollection<BaseClass>(BaseClass.AllBaseClasses);
                }
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
            NotifyPropertyChanged("ClassSpecifiers");

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
