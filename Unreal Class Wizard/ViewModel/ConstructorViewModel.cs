using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    public class ConstructorViewModel : INotifyPropertyChanged
    {
        public UnrealClass ClassModel { get; set; }


        public ConstructorViewModel()
        {
            this.ClassModel = App.CurrentClass;

            this.AddConstructor = true;
            this.AddDestructor = false;
            this.ConstructorSignature = "";

            NotifyPropertyChanged("PreviewHeader");

        }

        private string constructorSignature;

        public string ConstructorSignature
        {
            get { return constructorSignature; }
            set
            {
                constructorSignature = value;
                ClassModel.ConstructorSignature = constructorSignature;
                NotifyPropertyChanged("ConstructorSignature");
            }
        }

        /// <summary>
        /// If true, a destructor is added to the class/header
        /// </summary>
        private bool addDestructor;

        public bool AddDestructor
        {
            get { return addDestructor; }
            set
            {
                addDestructor = value;
                ClassModel.AddDestructor = addDestructor;
                NotifyPropertyChanged("AddDestructor");
            }
        }


        /// <summary>
        /// If true, a constructor is added to the class/header
        /// </summary>
        private bool addConstructor;

        public bool AddConstructor
        {
            get { return addConstructor; }
            set
            {
                addConstructor = value;
                ClassModel.AddConstructor = addConstructor;
                NotifyPropertyChanged("AddConstructor");            
            }
        }



        private string previewHeader;
        public string PreviewHeader
        {
            get { return ClassModel.HeaderText; }
            set
            {
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName, bool updatePreviews = true)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                if (updatePreviews == true)
                {
                    ClassModel.GeneratePreviews();
                }
            }
        }

    }
}
