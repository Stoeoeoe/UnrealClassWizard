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
            this.baseClasses = new ObservableCollection<BaseClass>(BaseClass.LoadBaseClasses());



            this.classModel = new UnrealClass();
            this.classModel.BaseClass = BaseClasses.FirstOrDefault();

            //classModel.ClassName = "XXXXXXXX";
            this.Access = "Public";
            NotifyPropertyChanged("Access");

            this.currentBaseClass = baseClasses.FirstOrDefault();
            NotifyPropertyChanged("CurrentBaseClass");

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

        private BaseClass currentBaseClass;
        public BaseClass CurrentBaseClass
        {
            get { return currentBaseClass; }
            set {

                // In case of a new class
                if(value == null)
                {
                    currentBaseClass = new BaseClass("blah");
                }
                else
                {
                    currentBaseClass = value;
                }
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
            set
            {
                isAbstract = value;
                classModel.IsAbstract = isAbstract;
                NotifyPropertyChanged("IsAbstract");
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

        
        private ObservableCollection<BaseClass> baseClasses;

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




        //public event PropertyChangedEventHandler PropertyChanged;

        //private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        //{

        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
    }
}
