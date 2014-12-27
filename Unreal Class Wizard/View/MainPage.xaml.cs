using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using Unreal_Class_Wizard.Model;
using Unreal_Class_Wizard.ViewModel;
using Xceed.Wpf.Toolkit;

namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        public MainPage()
        {

            InitializeComponent();
            this.DataContext = new UnrealClassViewModel();

            //BaseClassList test = new BaseClassList();
            //test.BaseClasses = new List<BaseClass>();
            //test.BaseClasses.Add(new BaseClass() { ClassName = "Character", ReadableName = "Character" });
            //test.BaseClasses.Add(new BaseClass() { ClassName = "Pawn", ReadableName = "Pawn" });

            //XmlSerializer xs = new XmlSerializer(typeof(BaseClassList));
            //xs.Serialize(new FileStream("C:/Temp/test.xml", FileMode.OpenOrCreate), test);

        }

        private void Wizard_PageChanged(object sender, RoutedEventArgs e)
        {
            WizardPage currentPage = (sender as Wizard).CurrentPage;
            if (currentPage == IntroPage)
            {

            }
            else if(currentPage == MetaInfosPage)
            {
                //(MetaInfosPage.Content as MetaInformation).SetFocusOnNameBox();
            }

        }

                public UnrealClassViewModel viewModel;



        public void SetFocusOnNameBox()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Input,
            new Action(delegate()
            {
                nameBox.Focus();         // Set Logical Focus
                Keyboard.Focus(nameBox); // Set Keyboard Focus
            }));

        }
    }
}
