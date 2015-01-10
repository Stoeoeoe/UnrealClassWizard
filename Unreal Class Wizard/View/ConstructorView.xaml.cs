using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unreal_Class_Wizard.ViewModel;

namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// Interaktionslogik für ConstructorView.xaml
    /// </summary>
    public partial class ConstructorView : Page
    {
        public ConstructorViewModel ViewModel { get; set; }

        public ConstructorView()
        {
            InitializeComponent();
            ViewModel = new ConstructorViewModel();
            this.DataContext = ViewModel;
        }


        private void reloadConstructor(object sender, RoutedEventArgs e)
        {

        }
    }
}
