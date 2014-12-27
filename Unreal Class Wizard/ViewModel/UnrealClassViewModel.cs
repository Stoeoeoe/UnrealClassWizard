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
            classModel = new UnrealClass();
            className = "";
        }

        private UnrealClass classModel;
        public UnrealClass ClassModel
        {
            get { return classModel; }
            set { classModel = value; }
        }


        private bool isPublic = true;

        public bool IsPublic
        {
            get { return isPublic; }
            set { isPublic = value; }
        }
        

        private bool isPrivate = false;
        public bool IsPrivate
        {
            get { return isPrivate; }
            set { isPrivate = value; }
        }


        private string className;
        public string ClassName
        {
            get { return className; }
            set
            {
                className = value.Trim();
                classModel.ClassName = className;
            }
        }

        private BaseClass currentBaseClass;
        public BaseClass CurrentBaseClass
        {
            get { return currentBaseClass; }
            set {
                currentBaseClass = value;
                classModel.BaseClass = currentBaseClass;
            }
        }


        private string previewHeader;
        public string PreviewHeader
        {
            get { return ClassModel.HeaderText; }
            set { previewHeader = value; }
        }



        
        private ObservableCollection<BaseClass> baseClasses;

        public ObservableCollection<BaseClass> BaseClasses
        {
            get 
            {
                // If it has not yet been loaded, load the template classes and set the current chosen class to the first in the list
                if (baseClasses == null || baseClasses.Count == 0)
                {
                    baseClasses = new ObservableCollection<BaseClass>(BaseClass.LoadBaseClasses());
                    if(baseClasses.Count > 0)
                    {
                        currentBaseClass = baseClasses.First();
                    }
                }

                return baseClasses;
            
            }
            set { baseClasses = value; }
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
