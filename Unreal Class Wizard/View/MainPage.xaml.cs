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
          //  this.DataContext = new UnrealClassViewModel();
        }

        public UnrealClassViewModel viewModel;

        private void Wizard_PageChanged(object sender, RoutedEventArgs e)
        {
            WizardPage currentPage = (sender as Wizard).CurrentPage;
            if (currentPage == IntroPage)
            {

            }
            else if(currentPage == MetaInfosPage)
            {
                (MetaInfosPage.Content as MetaInformation).SetFocusOnNameBox();
            }

        }

    }
}
