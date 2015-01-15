using System;
using System.Collections.Generic;
using System.Text;
using Unreal_Class_Wizard.ViewModel;

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
            this.ConstructorArguments = new List<string>();
            if(IsActor)
            {
                this.ConstructorArguments.Add("const FObjectInitializer& ObjectInitializer");
            }
            this.API = App.CurrentUser.UserInformation.ProjectName + "_API";
            this.UseAPI = false;


            this.AddConstructor = true;

            this.isInitialized = true;
            
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
        public List<string> ConstructorArguments { get; set; }
        public string HeaderText { get; set; }
        public string CPPText { get; set; }
        public bool AddDestructor { get; set; }
        public bool AddConstructor { get; set; }

        private Type currentTriggerClass;

        #endregion

        public void GeneratePreviews(Type triggerClass)
        {
            this.currentTriggerClass = triggerClass;
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
            // TODO allow for customized Copyright
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
            foreach (string includedClass in IncludedClasses)
            {
                sb.AppendFormat("#include \"{0}\"\r\n", includedClass);                       // Included classes
            }

            sb.AppendFormat("#include \"{0}.generated.h\"\r\n\r\n", ClassName);                        // Generated class file


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
                sb.AppendFormat(" {0}", API);                                                 // API
            }
            sb.AppendFormat(" {0}{1} ", GetPrefix(), ClassName);                                    // Class declaration

            // Only inherit if there is a base class
            if (BaseClass.ClassName != "")
            {
                sb.AppendFormat(": {0} {1}", Access.ToLower(), BaseClass.ClassName);
            }

            sb.AppendLine();                                                                             // Empty line
            sb.AppendLine("{");                                                                          // Start class body
            sb.AppendLine("    GENERATED_BODY()");                                                       // GENERATED_BODY() macro

        }

        private void WriteConstructor(StringBuilder sb)
        {
            if (currentTriggerClass == typeof(ConstructorViewModel))
            {
                if (AddConstructor)
                {
                    // Constructor
                    if (AddConstructor)
                    {
                        sb.AppendFormat("\r\n    {0}{1}::{0}{1}(", GetPrefix(), ClassName);             // First part of constructor
                        sb.Append(string.Join(",", ConstructorArguments));
                        sb.Append(");");

                        if(AddDestructor)
                        {
                            sb.AppendFormat("\r\n    {0}{1}::~{0}{1}();", GetPrefix(), ClassName);         // First part of constructor
                        }
                    }
                }

            }
        }

        private void GenerateCPP()
        {
            string gamePlayClass = App.CurrentUser.UserInformation.GameplayClass;

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("//" + App.CurrentUser.UserInformation.CopyrightText);                      // Copyright
            sb.AppendLine();                                                                             // Empty line
            sb.AppendFormat("#include \"{0}.h\"", gamePlayClass);                             // Gameplay class
            sb.AppendLine();                                                                             // Empty line


            // TODO: That's wrong, to be decoupled
            if (currentTriggerClass == typeof(ConstructorViewModel))
            {
                // Constructor/Destructor
                if (AddConstructor == true)
                {
                    sb.AppendFormat("\r\n{0}{1}::{0}{1}(", GetPrefix(), ClassName);                                                      // First part of constructor
                    sb.Append(string.Join(",", ConstructorArguments));
                    sb.Append(")\r\n{\r\n\r\n}");

                    if(AddDestructor == true)
                    {
                        sb.AppendFormat("\r\n\r\n{0}{1}::~{0}{1}()\r\n{{\r\n\r\n}}", GetPrefix(), ClassName);                                                      // First part of constructor

                    }

                }

            }

            // TODO: Method support


            CPPText = sb.ToString();
        }

        private string GetPrefix()
        {
            return (bool)IsActor ? "A" : "U";
        }

        //private string GetL


    }
}
