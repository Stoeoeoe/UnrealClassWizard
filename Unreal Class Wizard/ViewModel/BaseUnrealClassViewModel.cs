using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Model for UnrealClass
        private UnrealClass classModel;
        public UnrealClass ClassModel
        {
            get { return classModel; }
            set { classModel = value; }
        }

        private string previewHeader;
        public string PreviewHeader
        {
            get { return ClassModel.HeaderText; }
            set
            {
                previewHeader = value;
                NotifyPropertyChanged("PreviewHeader", false);
            }
        }

        private string previewCPP;
        public string PreviewCPP
        {
            get { return ClassModel.CPPText; }
            set
            {
                previewCPP = value;
                NotifyPropertyChanged("PreviewCPP", false);
            }
        }



        protected void NotifyPropertyChanged(string propertyName, bool updatePreviews = true)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

                if (updatePreviews == true)
                {
                    Type originType = this.GetType();
                    ClassModel.GeneratePreviews(originType);
                    NotifyPropertyChanged(previewHeader, false);
                    NotifyPropertyChanged(PreviewCPP, false);

                }
            }
        }



    }
}
