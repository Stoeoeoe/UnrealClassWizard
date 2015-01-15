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
        public static List<BaseClass> AllBaseClasses { get; set; }

        public BaseClass(string name, bool isGenerated)
        {
            this.ClassName = name;
            this.ReadableName = name;
            this.IsActorClass = false;
            this.HeaderFiles = new string[]{ "MyClass/" + name + ".h" };
            this.IsGenerated = isGenerated;
        }

        public BaseClass()
        {

        }

        public static void LoadBaseClasses()
        {
            string method = Properties.Settings.Default.SerializationMethod.Trim();
            List<BaseClass> list = new List<BaseClass>();
            switch(method)
            {
                case "XML":  list = LoadFromXML();  break;
                //case "JSON": list = LoadFromJSON(); break;
                default:     list = LoadFromXML();  break;
            }
            AllBaseClasses = list;
        }

        private static List<BaseClass> LoadFromXML()
        {
            XmlSerializer xs = new XmlSerializer(typeof(BaseClassList));
            List<BaseClass> baseClasses = new List<BaseClass>();


            if (Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/"))
            {
                try
                {
                    using (FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/BaseClasses.xml", FileMode.OpenOrCreate))
                    {
                        BaseClassList list = xs.Deserialize(fs) as BaseClassList;
                        baseClasses = new List<BaseClass>();
                        baseClasses.AddRange(list.BaseClasses);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    // TODO: Handle.
                }
            }
            else
            {
                // Create new Template
                BaseClassList list = new BaseClassList();
                list.BaseClasses = new List<BaseClass>();
                list.BaseClasses.Add(new BaseClass() { ClassName = "", HeaderFiles = new string[] { "" }, ReadableName = "" });
                try
                {
                    using (FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/BaseClasses.xml", FileMode.OpenOrCreate))
                    {
                        xs.Serialize(fs, list);
                    }
                    return baseClasses;

                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return baseClasses;
        }

        //private static List<BaseClass> LoadFromJSON()
        //{

        //    DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(BaseClassList), new DataContractJsonSerializerSettings() { UseSimpleDictionaryFormat = false });
        //    FileStream fs = new FileStream(System.AppDomain.CurrentDomain.BaseDirectory + "/Data/BaseClasses.json", FileMode.OpenOrCreate);
        //    List<BaseClass> baseClasses = new List<BaseClass>();

        //    if (fs.Length >= 0)
        //    {
        //        BaseClassList list = s.ReadObject(fs) as BaseClassList;
        //        baseClasses = new List<BaseClass>();
        //        baseClasses.AddRange(list.BaseClasses);
        //        return baseClasses;
        //    }
        //    else
        //    {
        //        return baseClasses;
        //    }

        //}

        public string ClassName { get; set; }

        public string ReadableName { get; set; }

        public bool IsActorClass { get; set; }

        public bool IsGenerated { get; set; }

        [XmlArray("HeaderFiles")]
        [XmlArrayItem("HeaderFile")]
        public string[] HeaderFiles { get; set; }


        public override string ToString()
        {
            return this.ClassName;
          //  return ClassName + " (" + ReadableName + ")";
        }
        
    }

    [XmlRoot("BaseClassList")]
    public class BaseClassList
    {


        [XmlArray]
        public List<BaseClass> BaseClasses { get; set; }
    }
}
