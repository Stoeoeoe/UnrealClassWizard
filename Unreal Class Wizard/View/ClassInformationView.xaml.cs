using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Unreal_Class_Wizard.Helpers;
using Unreal_Class_Wizard.Model;
using Unreal_Class_Wizard.ViewModel;

namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// Interaktionslogik für ClassInformation.xaml
    /// </summary>
    public partial class ClassInformationView : UserControl
    {
        public UnrealClassViewModel viewModel;


        public ClassInformationView()
        {
            InitializeComponent();
            viewModel = new UnrealClassViewModel();
            this.DataContext = viewModel;

        }

        public void SetFocusOnNameBox()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Input,
            new Action(delegate()
            {
                nameBox.Focus();         // Set Logical Focus
                Keyboard.Focus(nameBox); // Set Keyboard Focus
            }));

        }

        // TODO: Ugly.
        private void BaseClassChanged(object sender, TextChangedEventArgs e)
        {
            // Instead of binding
//           viewModel.CurrentBaseClassText = (sender as ComboBox).Text;
            viewModel.CurrentBaseClass = (sender as ComboBox).SelectedItem as BaseClass;
            //viewModel.CurrentBaseClass.ClassName = (sender as ComboBox).Text;
        }

        private void otherClassSpecifiersButton_Click(object sender, RoutedEventArgs e)
        {
            ClassSpecifiersWindow classSpecifierWindow = new ClassSpecifiersWindow();
            classSpecifierWindow.OKButtonEvent += new RoutedEventHandler(RouteClassSpecifiers);
            classSpecifierWindow.Show();
        }

        private void RouteClassSpecifiers(object sender, RoutedEventArgs e)
        {
            //TODO: Ugly
            ClassSpecifierEventArgs classSpecifierEventArgs = (ClassSpecifierEventArgs)e;

            viewModel.ClassSpecifiers = new ObservableCollection<string>(classSpecifierEventArgs.ClassSpecifiers);
        }


    }
}
