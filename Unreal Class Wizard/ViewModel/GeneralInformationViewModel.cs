using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.ViewModel
{
    public class GeneralInformationViewModel : BaseViewModel
    {



        public GeneralInformationViewModel(bool isDesignMode)
        {
            UserFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Data\\User.xml";

            // Run application (not design time)
            if (isDesignMode)
            {

            }
            // Design time
            else
            {

            }
        }

        private string headerPath;

        public string HeaderPath
        {
            get { return headerPath; }
            set
            {
                headerPath = value;
                NotifyPropertyChanged("HeaderPath");
            }
        }

        private string cppPath;

        public string CppPath
        {
            get { return cppPath; }
            set
            {
                cppPath = value;
                NotifyPropertyChanged("CppPath");
            }
        }

        private string userFile;

        public string UserFilePath
        {
            get { return userFile; }
            set { userFile = value; }
        }



        public void SaveChangesToModel(string companyName, string headerPath, string cppPath, bool useAPI)
        {
            App.CurrentUser.CompanyInformation.HeaderPath = HeaderPath;
            App.CurrentUser.CompanyInformation.CppPath = CppPath;
        }
    }
}
