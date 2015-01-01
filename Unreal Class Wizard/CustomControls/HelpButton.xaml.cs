using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaktionslogik für HelpButton.xaml
    /// </summary>
    public partial class HelpButton : UserControl
    {
        public static readonly DependencyProperty URLProperty = DependencyProperty.Register("URL", typeof(string), typeof(HelpButton));

        

        public HelpButton()
        {
            InitializeComponent();
        }


        public string URL
        {
            get { return base.GetValue(URLProperty) as string; }
            set { base.SetValue(URLProperty, value); }
        } 


        public HelpButton(string url)
        {
            this.URL = url;
            InitializeComponent();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            if(URL != null)
            {
                System.Diagnostics.Process.Start(URL);
            }
        }


    }

}
