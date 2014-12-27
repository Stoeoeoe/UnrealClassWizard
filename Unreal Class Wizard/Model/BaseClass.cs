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
    public class BaseClass : NotifyPropertyChangedBase
    {

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
            FileStream fs = new FileStream(Directory.GetCurrentDirectory() + "/Data/BaseClasses.xml", FileMode.OpenOrCreate);

            List<BaseClass> baseClasses = new List<BaseClass>();
            if (fs.Length >= 0)
            {
                BaseClassList list = xs.Deserialize(fs) as BaseClassList;
                baseClasses = new List<BaseClass>();
                baseClasses.AddRange(list.BaseClasses);

                //DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(BaseClassList), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = false });
                //FileStream fs2 = new FileStream(Directory.GetCurrentDirectory()  + "/Data/BaseClasses.json", FileMode.OpenOrCreate);
                //s.WriteObject(fs2, list);

                return baseClasses;
            }
            else
            {
                // TODO: Create new Template
                return baseClasses;
            }
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

        private string className;

        public string ClassName
        {
            get { return className; }
            set
            {
                if (className == value) return;
                className = value;
                NotifyPropertyChanged("ClassName");
            }
        }

        private string readableName;

        public string ReadableName
        {
            get { return readableName; }
            set
            {
                if (className == value) return;
                readableName = value;
                NotifyPropertyChanged("ReadableName");
            }
        }

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
