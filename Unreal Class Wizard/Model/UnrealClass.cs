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

        public UnrealClass()
        {
            
        }

        #region Properties

        private string className = "";

        public string ClassName
        {
            get { return className; }
            set 
            {
                if (className == value) return;
                className = value;
                GenerateHeader();
            }
        }

        private string description = "";

        public string Description
        {
            get { return description; }
            set
            {
                if (description == value) return;
                description = value.Trim();
                GenerateHeader();
            }
        }


        private BaseClass baseClass = new BaseClass();

        public BaseClass BaseClass
        {
            get { return baseClass; }
            set
            {
                baseClass = value;
                GenerateHeader();
            }        
        }


        private string access = "Public";

        public string Access
        {
            get { return access; }
            set
            {
                access = value;
                GenerateHeader();
            }
        }


        private List<string> includedClasses = new List<string>();

        public List<string> IncludedClasses
        {
            get { return includedClasses; }
            set
            {
                includedClasses = value;
                GenerateHeader();
            }
        }

        private List<ClassSpecifier> classSpecifiers = new List<ClassSpecifier>();

        public List<ClassSpecifier> ClassSpecifiers
        {
            get { return classSpecifiers; }
            set
            {
                classSpecifiers = value;
                GenerateHeader();
            }
        }

        private bool isActor = false;

        public bool IsActor {
            get { return isActor; }
            
            set
            {
                isActor = value;
                GenerateHeader();
            }
        }

        private bool isAbstract = false;
        public bool IsAbstract
        {
            get { return isAbstract; }

            set
            {
                isAbstract = value;
                GenerateHeader();
            }
        }

        private string copyrightText = "";
        public string CopyrightText
        {
            get { return copyrightText; }

            set
            {
                copyrightText = value;
                GenerateHeader();
            }
        }


        private string constructorText = "";
        public string ConstructorText
        {
            get { return constructorText; }

            set
            {
                constructorText = value;
                GenerateHeader();
            }
        }


        #endregion

        public void GenerateHeader()
        {
            StringBuilder sb = new StringBuilder();

            // Preparation
            string tempClassName = ClassName == "" ? "XXXXX" : ClassName;                               // Use XXXXX as a substitute as long as there is no class name
            string prefix = IsActor ? "A" : "U";
            // TODO: Add support for F prefix
            string copyRightText = CopyrightText == "" ? App.CurrentUser.CompanyInformation.CopyrightText : CopyrightText;

            // Start writing header
            sb.AppendLine("//" + App.CurrentUser.CompanyInformation.CopyrightText + "\r\n\r\n");         // Copyright
            sb.AppendLine("#pragma once");                                                               // Pragma once
            
            // TODO: Included classes
            for (int i = 0; i < includedClasses.Count; i++ )
            {
                string includedClass = includedClasses[i];
                if (includedClass.EndsWith(".h") == false)
                {
                    includedClass += ".h";
                }
                sb.AppendLine(String.Format("#include \"{0}\"", includedClass));                                                                           
            }

            sb.AppendLine(String.Format("#include \"{0}\"", tempClassName + ".generated.h"));            // Generated header
            sb.AppendLine();                                                                             // Empty line
            

            sb.AppendLine("/**");                                                                        // Start description
            if (Description != "")
            {
                string[] lines = Description.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.RemoveEmptyEntries);  // Get all description lines
                foreach (string line in lines)
                {
                    sb.AppendLine(" * " + line);                                                        // Add description line
                }
            }
            else { sb.AppendLine(" *"); }
            sb.AppendLine(" */");                                                                       // End description

            sb.Append("UCLASS(");                                                                       // UClass definition start
                if (IsAbstract) sb.Append("abstract");                                                  // Abstract
            sb.Append(")\r\n");                                                                         // UClass definition end

            sb.Append(String.Format("class {0} {1}{2} ", App.CurrentUser.CompanyInformation.API, prefix, tempClassName));      // Class declaration
            
            // Only inherit if there is a base class
            if(BaseClass.ClassName != "")
            {
                sb.Append(String.Format(": {0} {1}", Access.ToLower(), BaseClass.ClassName));
            }

            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine("{");                                                                          // Start class body
            sb.AppendLine("    GENERATED_BODY()");                                                       // GENERATED_BODY() macro
        
            if (ConstructorText != "")
            {
                sb.AppendLine(String.Format("    {0}{1}({2});", prefix, ClassName, ConstructorText));                                                                       // Add constructor
            }

            sb.AppendLine("}");                                                                             // End class body

            HeaderText = sb.ToString();
        }

        public string HeaderText { get; set; }



    }
}
