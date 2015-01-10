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
    /// Interaktionslogik für GroupControl.xaml
    /// </summary>
    public partial class GroupControl : UserControl
    {

        public List<FrameworkElement> Labels
        {
            get { return (List<FrameworkElement>)GetValue(LabelsProperty); }
            set { SetValue(LabelsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Labels.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelsProperty =
            DependencyProperty.Register("Labels", typeof(List<FrameworkElement>), typeof(GroupControl), new PropertyMetadata(new List<FrameworkElement>()));



        public List<FrameworkElement> Fields
        {
            get { return (List<FrameworkElement>)GetValue(FieldsProperty); }
            set { SetValue(FieldsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Fields.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FieldsProperty =
            DependencyProperty.Register("Fields", typeof(List<FrameworkElement>), typeof(GroupControl), new PropertyMetadata(new List<FrameworkElement>()));

        

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(GroupControl), new PropertyMetadata("Title"));




        public int GridSpace
        {
            get { return (int)GetValue(GridSpaceProperty); }
            set { SetValue(GridSpaceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GridSpace.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GridSpaceProperty =
            DependencyProperty.Register("GridSpace", typeof(int), typeof(GroupControl), new PropertyMetadata(3));



        public int LineHeight
        {
            get { return (int)GetValue(LineHeightProperty); }
            set { SetValue(LineHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LineHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineHeightProperty =
            DependencyProperty.Register("LineHeight", typeof(int), typeof(GroupControl), new PropertyMetadata(34));

        
        

        public GroupControl()
        {
            InitializeComponent();

            // Re-Initialize list properties, otherwise they will be shared accross all instances of GroupControl
            SetValue(FieldsProperty, new List<FrameworkElement>());
            SetValue(LabelsProperty, new List<FrameworkElement>()); 
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (Fields.Count != Labels.Count)
            {
                throw new Exception("The number of Fields and Labels must be equal.");
            }

            for (int i = 0; i < Fields.Count; i++)
            {
                itemGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(LineHeight) });
                

                FrameworkElement label = Labels[i];
                label.Style = Resources["GroupLabelStyle"] as Style;
                Grid.SetRow(label, i);
                Grid.SetColumn(label, 1);
                itemGrid.Children.Add(label);


                FrameworkElement field = Fields[i];
                field.Margin = new Thickness(field.Margin.Left, field.Margin.Top + GridSpace, field.Margin.Right, field.Margin.Bottom + GridSpace);

                Grid.SetRow(field, i);
                Grid.SetColumn(field, 2);
                itemGrid.Children.Add(field);



                // Add spacing at the end
                
            } 
        }



    }
}
