using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Unreal_Class_Wizard.Model
{
    public class UserInformation
    {
        public string ProjectName {get; set;}
        public string CopyrightText { get; set; }

        public string API { get; set; }

        public string GameplayClass { get; set; }

        public string HeaderPath { get; set; }

        public string CppPath { get; set; }

        public string EnginePath { get; set; }
    }
}
