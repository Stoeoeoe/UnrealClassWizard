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

namespace Unreal_Class_Wizard.CustomControls
{
    /// <summary>
    /// Interaktionslogik für UE4StyleTitleBar.xaml
    /// </summary>
    public partial class UE4StyleTitleBar : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(UE4StyleTitleBar), new PropertyMetadata("Title"));

        public string Title
        {
            get { return (string)base.GetValue(TitleProperty); }
            set { base.SetValue(TitleProperty, value); }
        }

        public UE4StyleTitleBar()
        {
            InitializeComponent();
        }


        private void titleBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void CloseWindow(object sender, RoutedEventArgs args)
        {
            MessageBox.Show("This class has not been generated.\r\nDo you really want to quit?");
        }

    }
}
