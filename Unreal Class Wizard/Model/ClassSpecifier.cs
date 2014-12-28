using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Unreal_Class_Wizard.Model
{
    public class ClassSpecifier
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public bool DoNotUse { get; set; }

        public ClassSpecifier()
        {

        }

        public ClassSpecifier(string name, string url)
        {

        }

        // https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Abstract/index.html
    }

    

    [XmlRoot("ClassSpecifierList")]
    public class ClassSpecifierList
    {
        [XmlArray]
        public List<ClassSpecifier> ClassSpecifiers { get; set; }
    }
}
