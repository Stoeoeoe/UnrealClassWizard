using System;
using System.Collections.Generic;
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
using Unreal_Class_Wizard.ViewModel;

namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// Interaktionslogik für ClassInformation.xaml
    /// </summary>
    public partial class MetaInformation : UserControl
    {
        public UnrealClassViewModel viewModel;

        public MetaInformation()
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

    }
}
