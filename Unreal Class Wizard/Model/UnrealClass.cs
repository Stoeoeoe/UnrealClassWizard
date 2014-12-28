﻿using System;
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

        #endregion

        public void GenerateHeader()
        {
            StringBuilder sb = new StringBuilder();

            string tempClassName = ClassName == "" ? "XXXXX" : ClassName;                               // Use XXXXX as a substitute as long as there is no class name
            string prefix = IsActor ? "A" : "U";
            // TODO: Add support for F prefix

            sb.AppendLine("//" + App.CurrentUser.CompanyInformation.CopyrightText + "\r\n\r\n");        // Copyright


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

            sb.AppendLine(String.Format("#include \"{0}\"", tempClassName + ".generated.h"));                // Generated header
            sb.AppendLine();                                                                             // Empty line
            
            // Description
            sb.AppendLine("/**");
            if (Description != "")
            {
                string[] lines = Description.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    sb.AppendLine(" * " + line);
                }
            }
            else
            {
                sb.AppendLine(" *");

            }
                sb.AppendLine(" */");

            sb.Append("UCLASS(");                                                                       // UClass definition start
                if (IsAbstract) sb.Append("abstract");                                                  // Abstract
            sb.Append(")\r\n");

            sb.Append(String.Format("class {0} {1}{2} ", App.CurrentUser.CompanyInformation.API, prefix, tempClassName));      // Class declaration
            
            // Only inherit if there is a base class
            if(BaseClass.ClassName != "")
            {
                sb.Append(String.Format(": {0} {1}", Access.ToLower(), BaseClass.ClassName));
            }

            sb.AppendLine("{");                                                                             // Curly braces
            sb.AppendLine("    GENERATED_BODY()");                                                          // GENERATED_UCLASS_BODY() macro
            sb.AppendLine("}");                                                                             // Curly braces


            HeaderText = sb.ToString();
        }

        public string HeaderText { get; set; }


    }
}
