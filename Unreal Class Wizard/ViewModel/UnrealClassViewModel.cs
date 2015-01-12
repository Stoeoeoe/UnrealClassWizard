﻿using System;
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
                this.IsActor = ClassModel.IsActor;

                this.CurrentBaseClass = ClassModel.BaseClass;
                this.CurrentBaseClassIndex = BaseClasses.IndexOf(ClassModel.BaseClass);
                this.CurrentBaseClassText = CurrentBaseClass.ReadableName;

                this.UseAPI = ClassModel.UseAPI;
                this.API = ClassModel.API;

                //this.CurrentBaseClassIndex = 0;

                UpdateClassSpecifiers(ClassSpecifier.LoadClassSpecifiers());
            }
            // Design time
            else
            {

            }

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
                    classModel.BaseClass = currentBaseClass;

                    if(classModel.BaseClass.IsGenerated == false)
                    {
                        IsActor = currentBaseClass.IsActorClass;
                        classModel.IsActor = IsActor;
                        NotifyPropertyChanged("IsActor");
                    }

                    NotifyPropertyChanged("CurrentBaseClass");
                    NotifyPropertyChanged("PreviewHeader");
                    
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
                classModel.Access = access;
                NotifyPropertyChanged("Access");
                NotifyPropertyChanged("PreviewHeader");
            }
        }

        private string api;

        public string API
        {
            get { return api; }
            set
            {
                api = value.Trim();
                classModel.API = api;
                NotifyPropertyChanged("API");
                NotifyPropertyChanged("PreviewHeader");

            }
        }


        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                description = value.Trim(new char[' ']);
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
                additionalIncludedClasses = value.Trim();
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

        


        private bool? isActor;
        public bool? IsActor
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

                 classModel.ClassSpecifiers.SingleOrDefault(specifier => specifier.Name.ToLower() == "abstract").Value = isAbstract;
                 
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

                classModel.ClassSpecifiers.SingleOrDefault(specifier => specifier.Name.ToLower() == "blueprintable").Value = isBlueprintable;
                
                classModel.GeneratePreviews();
                NotifyPropertyChanged("ClassSpecifiers");
                NotifyPropertyChanged("IsBlueprintable");
                NotifyPropertyChanged("PreviewHeader");
                }
        }

        private bool useAPI;

        public bool UseAPI
        {
            get { return useAPI; }
            set 
            {
                useAPI = value;
                classModel.UseAPI = useAPI;
                NotifyPropertyChanged("UseAPI");
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
            get
            {
                return classSpecifiers;
            }
            set
            {
                classSpecifiers = value;
                classModel.ClassSpecifiers = classSpecifiers.ToList<ClassSpecifier>();
                NotifyPropertyChanged("ClassSpecifiers");
                NotifyPropertyChanged("PreviewHeader");
                classModel.GeneratePreviews();
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
