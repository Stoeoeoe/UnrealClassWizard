using System;
using System.Collections.Generic;
using System.Text;

namespace Unreal_Class_Wizard.Model
{
    public class UnrealClass
    {
        bool isInitialized;

        public UnrealClass()
        {
            this.ClassName = "MyClass";
            this.Description = "";
            this.BaseClass = new BaseClass("", true);
            this.Access = "Public";
            this.IncludedClasses = new List<string>();
            this.ClassSpecifiers = ClassSpecifier.LoadClassSpecifiers();
            this.IsActor = BaseClass.IsActorClass;
            this.CopyrightText = "";
            this.ConstructorSignature = "";
            this.API = App.CurrentUser.UserInformation.ProjectName + "_API";
            this.UseAPI = false;

            

            this.isInitialized = true;
            
        }

        private string GetPrefix()
        {
            return (bool)IsActor ? "A" : "U";
        }


        #region Properties

        public string ClassName { get; set; }
        public string Description { get; set; }
        public BaseClass BaseClass { get; set; }
        public string Access { get; set; }
        public List<string> IncludedClasses { get; set; }
        public List<ClassSpecifier> ClassSpecifiers {get;set;}
        public bool IsActor {get; set;}
        public string CopyrightText { get; set; }
        public bool UseAPI { get; set; }
        public string API { get; set; }
        public string ConstructorSignature { get; set; }
        public string HeaderText { get; set; }
        public string CPPText { get; set; }
        public bool AddDestructor { get; set; }
        public bool AddConstructor { get; set; }

        #endregion

        public void GeneratePreviews()
        {

            if (isInitialized)            
            {
                GenerateHeader();
                GenerateCPP();
            }
        }

        public void GenerateHeader()
        {
            StringBuilder sb = new StringBuilder();
            WriteHeaderUntilConstructor(sb);
            WriteConstructor(sb);

            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine();                                                                             // Empty line

            sb.AppendLine("}");                                                                          // End class body

            HeaderText = sb.ToString();
        }

        private void WriteHeaderUntilConstructor(StringBuilder sb)
        {
            string copyRightText = CopyrightText == "" ? App.CurrentUser.UserInformation.CopyrightText : CopyrightText;

            // Concat all Class Specifiers
            StringBuilder sbClassSpecifiers = new StringBuilder();


            foreach (ClassSpecifier classSpecifierValue in ClassSpecifiers)
            {
                string name = classSpecifierValue.Name;
                object value = classSpecifierValue.Value;
                if (value is bool && value as bool? == true)
                {
                    sbClassSpecifiers.Append(name + ", ");
                }
                else if (value is string && value as string != "")
                {
                    sbClassSpecifiers.Append(name + "(" + value + "),");
                }
            }

            // remove final comma
            string classSpecifierString = sbClassSpecifiers.ToString().TrimEnd(new char[] { ',', ' ' });

            // Start writing header
            sb.AppendLine("//" + App.CurrentUser.UserInformation.CopyrightText + "\r\n\r\n");         // Copyright
            sb.AppendLine("#pragma once");                                                            // Pragma once

            // Included classes
            for (int i = 0; i < IncludedClasses.Count; i++)
            {
                string includedClass = IncludedClasses[i];
                if (includedClass.EndsWith(".h") == false)
                {
                    includedClass += ".h";
                }
                sb.AppendLine(String.Format("#include \"{0}\"", includedClass));
            }

            sb.AppendLine(String.Format("#include \"{0}\"", ClassName + ".generated.h"));                // Generated header
            sb.AppendLine();                                                                             // Empty line


            sb.AppendLine("/**");                                                                        // Start description
            if (Description != "")
            {
                string[] lines = Description.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);  // Get all description lines
                foreach (string line in lines)
                {
                    sb.AppendLine(" * " + line);                                                        // Add description line
                }
            }
            else { sb.AppendLine(" *"); }
            sb.AppendLine(" */");                                                                       // End description

            sb.Append("UCLASS(");                                                                       // UClass definition start
            sb.Append(classSpecifierString);
            sb.Append(")\r\n");                                                                         // UClass definition end

            sb.Append("class");                                                                         // Class declaration start
            if (UseAPI)
            {
                sb.Append(String.Format(" {0}", API));                                                 // API
            }
            sb.Append(String.Format(" {0}{1} ", GetPrefix(), ClassName));                                    // Class declaration

            // Only inherit if there is a base class
            if (BaseClass.ClassName != "")
            {
                sb.Append(String.Format(": {0} {1}", Access.ToLower(), BaseClass.ClassName));
            }

            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine("{");                                                                          // Start class body
            sb.AppendLine("    GENERATED_BODY()");                                                       // GENERATED_BODY() macro

        }

        private void WriteConstructor(StringBuilder sb)
        {

            if (ConstructorSignature != "")
            {
                sb.AppendLine(String.Format("    {0}{1}({2});", GetPrefix(), ClassName, ConstructorSignature));   // Add constructor
            }
        }

        private void GenerateCPP()
        {
            string gamePlayClass = App.CurrentUser.UserInformation.GameplayClass;
            gamePlayClass = gamePlayClass.EndsWith(".h")? gamePlayClass : gamePlayClass + ".h";
            string tempClassName = ClassName == "" ? "XXXXX" : ClassName;                               // Use XXXXX as a substitute as long as there is no class name
            string prefix = (bool)IsActor ? "A" : "U";                                                        // Set prefix, A for Actors and U for everything else, wasn't there F too?

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("//" + App.CurrentUser.UserInformation.CopyrightText);                      // Copyright
            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine(String.Format("#include \"{0}\"", gamePlayClass));                             // Gameplay class
            sb.AppendLine();                                                                             // Empty line
            
            // TODO: Constructor support
            //if(ConstructorText != "")
            //{
            //    sb.AppendLine("{0}{1}::{0}{1}");                                                             // Empty line
            //}

            // TODO: Method support


            CPPText = sb.ToString();
        }


    }
}
