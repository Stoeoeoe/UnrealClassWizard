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
using Unreal_Class_Wizard.Model;

namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// TODO: Maybe re-implement this, it was actually prettier than the current situation...
    /// </summary>
    public partial class SpecifierControl : UserControl
    {
        public static readonly DependencyProperty BufferWidthProperty = DependencyProperty.Register("BufferWidth", typeof(Double), typeof(SpecifierControl), new PropertyMetadata(20.0));
        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register("LabelWidth", typeof(Double), typeof(SpecifierControl), new PropertyMetadata(165.0));
        public static readonly DependencyProperty ValueWidthProperty = DependencyProperty.Register("ValueWidth", typeof(Double), typeof(SpecifierControl), new PropertyMetadata(200.0));
        public static readonly DependencyProperty ValueTypeProperty = DependencyProperty.Register("ValueType", typeof(string), typeof(SpecifierControl));


        public Double BufferWidth
        {
            get { return (Double)base.GetValue(BufferWidthProperty); }
            set { base.SetValue(BufferWidthProperty, value); }
        }

        public Double LabelWidth
        {
            get { return (Double)base.GetValue(LabelWidthProperty); }
            set { base.SetValue(LabelWidthProperty, value); }
        }

        public Double ValueWidth
        {
            get { return (Double)base.GetValue(ValueWidthProperty); }
            set { base.SetValue(ValueWidthProperty, value); }
        }

        public string ValueType
        {
            get { return (string)base.GetValue(ValueTypeProperty); }
            set { base.SetValue(ValueTypeProperty, value); }
        }

        public SpecifierControl()
        {
            InitializeComponent();

            //ClassSpecifier specifier = (ClassSpecifier)DataContext;

            //if (specifier.Type == "String")
            //{
            //    TextBox textBox = new TextBox();
            //    textBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //    textBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //    textBox.Margin = new Thickness(10);
            //    textBox.Width = 150;
            //    textBox.Height = 18;
            //    Grid.SetRow(textBox, 2);

            //    // Fill value if there is one
            //    textBox.Text = (string)specifier.Value;

            //    itemGrid.Children.Add(textBox);
            //}
            //if (specifier.Type == "Boolean")
            //{
            //    CheckBox checkBox = new CheckBox();
            //    checkBox.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //    checkBox.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            //    checkBox.Margin = new Thickness(10, 12, 0, 12);
            //    checkBox.Width = 16;
            //    checkBox.Height = 16;

            //    itemGrid.Children.Add(checkBox);
            //}
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
        }

        // TODO: Extend this to all sorts of specifiers
        public SpecifierControl(int bufferWidth, int labelWidth, int valueWidth, ClassSpecifier specifier)
        {
            this.BufferWidth = bufferWidth;
            this.LabelWidth = labelWidth;
            this.ValueWidth = valueWidth;

            


            
            InitializeComponent();


        }

    }
}
