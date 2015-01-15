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
    /// Interaktionslogik für ArgumentControl.xaml
    /// </summary>
    public partial class ArgumentControl : UserControl
    {
        public ArgumentControl()
        {
            InitializeComponent();
        }



        public bool IsConst
        {
            get { return (bool)GetValue(IsConstProperty); }
            set { SetValue(IsConstProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsConst.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsConstProperty =
            DependencyProperty.Register("IsConst", typeof(bool), typeof(ArgumentControl), new PropertyMetadata(false));



        public bool IsStatic
        {
            get { return (bool)GetValue(IsStaticProperty); }
            set { SetValue(IsStaticProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsStatic.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStaticProperty =
            DependencyProperty.Register("IsStatic", typeof(bool), typeof(ArgumentControl), new PropertyMetadata(false));



        public string Type
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Type.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(string), typeof(ArgumentControl), new PropertyMetadata(""));



        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(ArgumentControl), new PropertyMetadata(0));


    }
}
