using System;
using System.Collections.Generic;
using System.IO;
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

        public object Value { get; set; }

        public string Type { get; set; }            // Possible types: String, Boolean

        public ClassSpecifier()
        {

        }

        public static List<ClassSpecifier> LoadClassSpecifiers()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ClassSpecifierList));
            List<ClassSpecifier> list = new List<ClassSpecifier>();

            if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/"))
            {
                using (FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/Data/ClassSpecifiers.xml", FileMode.OpenOrCreate))
                {
                    ClassSpecifierList csl = (ClassSpecifierList)xs.Deserialize(fs);
                    list.AddRange(csl.ClassSpecifiers);
                }
            }
            return list;
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
