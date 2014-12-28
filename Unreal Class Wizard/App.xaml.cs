using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using Unreal_Class_Wizard.Model;
using Unreal_Class_Wizard.View;
using Unreal_Class_Wizard.ViewModel;

namespace Unreal_Class_Wizard
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


            //Check if all required folders exist
            if(Directory.Exists(Directory.GetCurrentDirectory() + "/Data/") == false)
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Data/");
            }

//            (MainWindow as MainPage).LayoutRoot.DataContext = new UnrealClassViewModel(); 
            
            CurrentUser = User.LoadUser();
            HeaderParser.ParseHeader("blah");

            ClassSpecifierList list = new ClassSpecifierList();
            list.ClassSpecifiers = new List<ClassSpecifier>();
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "AdvancedClassDisplay",      URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/AdvancedClassDisplay/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "AutoCollapseCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/AutoCollapseCategories/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "AutoExpandCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/AutoExpandCategories/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Blueprintable", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Blueprintable/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "BlueprintType", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/BlueprintType/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "ClassGroup", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/ClassGroup/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "CollapseCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/CollapseCategories/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Config", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Config/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Const", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Const/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "ConversionRoot", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/ConversionRoot/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "CustomConstructor", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/CustomConstructor/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "DefaultToInstanced", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/DefaultToInstanced/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "DependsOn", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/DependsOn/index.html" });       // Blah
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Deprecated", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Deprecated/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "DontAutoCollapseCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/DontAutoCollapseCategories/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "DontCollapseCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/DontCollapseCategories/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "EditInlineNew", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/EditInlineNew/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "HideCategories", URL = "" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "HideCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/HideCategories/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "HideDropdown", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/HideDropdown/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "HideFunctions", URL = "" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "AutoCollapseCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/HideFunctions/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Intrinsic", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Intrinsic/index.html", DoNotUse = true });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "MinimalAPI", URL = "" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "AutoCollapseCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/MinimalAPI/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "NoExport", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/NoExport/index.html", DoNotUse=true });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "NonTranisent", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/NonTransient/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "NotBlueprintable", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/NotBlueprintable/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "NotPlaceable", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/NotPlaceable/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "PerObjectConfig", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/PerObjectConfig/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Placeable", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Placeable/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "ShowCategories", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/ShowCategories/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Transient", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Transient/index.html" });
            list.ClassSpecifiers.Add(new ClassSpecifier() { Name = "Within", URL = "https://docs.unrealengine.com/latest/INT/Programming/UnrealArchitecture/Reference/Classes/Specifiers/Within/index.html" });
            XmlSerializer xs = new XmlSerializer(typeof(ClassSpecifierList));
            FileStream fs = new FileStream(@"C:\Users\Matthias\Documents\Visual Studio 2013\Projects\Unreal Class Wizard\Unreal Class Wizard\Data\ClassSpecifiers.xml", FileMode.OpenOrCreate);
            xs.Serialize(fs, list);

        }
    }
}
