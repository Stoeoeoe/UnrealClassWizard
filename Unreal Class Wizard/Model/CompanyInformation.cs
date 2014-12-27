using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Unreal_Class_Wizard.Model
{
    public class CompanyInformation
    {
        #region Property Boilerplate
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Properties

        private string copyrightText;

        public string CopyrightText
        {
            get { return copyrightText; }
            set
            {
                if (copyrightText == value) return;
                copyrightText = value;
                NotifyPropertyChanged("CopyrightText");
            }
        }

        #endregion

        public void LoadCompanyInformation()
        {

        }
    }
}
