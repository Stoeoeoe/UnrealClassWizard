using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Unreal_Class_Wizard.Model
{
    public class BaseClass
    {
        public BaseClass(string name)
        {
            this.ClassName = name;
            this.ReadableName = name;
            this.IsActorClass = false;
            this.HeaderFiles = new string[]{ "XXXXX/" + name + ".h" };
        }

        public BaseClass()
        {

        }

        public static List<BaseClass> LoadBaseClasses()
        {
            string method = Properties.Settings.Default.SerializationMethod.Trim();
            List<BaseClass> list = new List<BaseClass>();
            switch(method)
            {
                case "XML":  list = LoadFromXML();  break;
                case "JSON": list = LoadFromJSON(); break;
                default:     list = LoadFromXML();  break;
            }
            return list;
        }

        private static List<BaseClass> LoadFromXML()
        {
            XmlSerializer xs = new XmlSerializer(typeof(BaseClassList));
            List<BaseClass> baseClasses = new List<BaseClass>();


            if(Directory.Exists(Directory.GetCurrentDirectory() + "/Data/"))
            {
                using(FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/Data/BaseClasses.xml", FileMode.OpenOrCreate))
                {
                    BaseClassList list = xs.Deserialize(fs) as BaseClassList;
                    baseClasses = new List<BaseClass>();
                    baseClasses.AddRange(list.BaseClasses);
                }
                //DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(BaseClassList), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = false });
                //FileStream fs2 = new FileStream(Directory.GetCurrentDirectory()  + "/Data/BaseClasses.json", FileMode.OpenOrCreate);
                //s.WriteObject(fs2, list);
            }
            else
            {
                // Create new Template
                BaseClassList list = new BaseClassList();
                list.BaseClasses = new List<BaseClass>();
                list.BaseClasses.Add(new BaseClass() { ClassName = "", HeaderFiles = new string[] { "" }, ReadableName = "" });
                using(FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/Data/BaseClasses.xml", FileMode.OpenOrCreate))
                {
                    xs.Serialize(fs, list);
                }
                return baseClasses;
            }
            return baseClasses;
        }

        private static List<BaseClass> LoadFromJSON()
        {

            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(BaseClassList), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = false });
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/Data/BaseClasses.json", FileMode.OpenOrCreate);
            List<BaseClass> baseClasses = new List<BaseClass>();

            if (fs.Length >= 0)
            {
                BaseClassList list = s.ReadObject(fs) as BaseClassList;
                baseClasses = new List<BaseClass>();
                baseClasses.AddRange(list.BaseClasses);
                return baseClasses;
            }
            else
            {
                return baseClasses;
            }

        }

        public string ClassName { get; set; }

        public string ReadableName { get; set; }

        public bool IsActorClass { get; set; }

        [XmlArray("HeaderFiles")]
        [XmlArrayItem("HeaderFile")]
        public string[] HeaderFiles { get; set; }


        public override string ToString()
        {
            return ReadableName;
        }
        
    }

    [XmlRoot("BaseClassList")]
    public class BaseClassList
    {
        [XmlArray]
        public List<BaseClass> BaseClasses { get; set; }
    }
}
