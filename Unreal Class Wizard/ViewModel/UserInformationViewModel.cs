using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    public class UserInformationViewModel : INotifyPropertyChanged
    {

        public UserInformation UserInformationModel { get; set; }

        public UserInformationViewModel(bool isDesignMode)
        {
            // Model
            this.UserInformationModel = App.CurrentUser.UserInformation;
            
            // ViewModel
            this.UserFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Data\\User.xml";
            this.CppPath = UserInformationModel.CppPath;
            this.HeaderPath = UserInformationModel.HeaderPath;
            this.ProjectName = UserInformationModel.ProjectName;
            this.CopyrightText = UserInformationModel.CopyrightText;

            //// Run application (not design time)
            //if (isDesignMode)
            //{
            //}
            //// Design time
            //else
            //{
            //}
        }

        private string headerPath;

        public string HeaderPath
        {
            get { return headerPath; }
            set
            {
                headerPath = value;
                UserInformationModel.HeaderPath = headerPath;
                NotifyPropertyChanged("HeaderPath");

            }
        }

        private string copyrightText;

        public string CopyrightText
        {
            get { return copyrightText; }
            set
            {
                copyrightText = value;
                UserInformationModel.CopyrightText = copyrightText;
                NotifyPropertyChanged("CopyrightText");
            }
        }


        private string cppPath;

        public string CppPath
        {
            get { return cppPath; }
            set
            {
                cppPath = value;
                UserInformationModel.CppPath = cppPath;
                NotifyPropertyChanged("CppPath");
            }
        }

        private string userFile;

        public string UserFilePath
        {
            get { return userFile; }
            set { userFile = value; }
        }

        private string projectName;

        public string ProjectName
        {
            get { return projectName; }
            set
            {
                projectName = value;
                UserInformationModel.ProjectName = projectName;
                NotifyPropertyChanged("ProjectName");                
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public UnrealClass ClassModel { get; set; }

        protected void NotifyPropertyChanged(string propertyName, bool updatePreviews = true)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }

    }
}
