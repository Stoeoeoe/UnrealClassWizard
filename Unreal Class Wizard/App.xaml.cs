using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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
        }
    }
}
