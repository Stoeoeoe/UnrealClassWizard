using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Unreal_Class_Wizard.Model
{
    public class UnrealClass
    {

        #region Properties

        private string className;

        public string ClassName
        {
            get { return className; }
            set 
            {
                if (className == value) return;
                className = value;
                //NotifyPropertyChanged("ClassName");

                GenerateHeader();
                //NotifyPropertyChanged("HeaderText");
            }
        }

        private string headerText;

        public string HeaderText
        {
            get { return headerText; }
            set { headerText = value; }
        }

        private BaseClass baseClass;

        public BaseClass BaseClass
        {
            get { return baseClass; }
            set
            {
                baseClass = value;
                //NotifyPropertyChanged("BaseClass");

                GenerateHeader();
                //NotifyPropertyChanged("HeaderText");
            }        
        }


        #endregion

        private void GenerateHeader()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine("#pragma once");                                                               // Pragma once
            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine();

            sb.AppendLine("//" + App.CurrentUser.CompanyInformation.CopyrightText + "\r\n\r\n");

            headerText = sb.ToString();
        }

        public string GetPreview()
        {

            return "";
        }

    }
}
