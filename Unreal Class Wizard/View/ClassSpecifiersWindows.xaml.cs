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
using System.Windows.Shapes;
using Unreal_Class_Wizard.Helpers;
using Unreal_Class_Wizard.Model;
using Unreal_Class_Wizard.ViewModel;

namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// Interaktionslogik für ClassSpecifiers.xaml
    /// </summary>
    public partial class ClassSpecifiersWindow : Window
    {
        private ClassSpecifierViewModel viewModel;

        public event RoutedEventHandler OKButtonEvent;

        // TODO: Move this out of CodeBehind...
        protected void On_OKButtonPressed()
        {
            if (this.OKButtonEvent != null)
            {
                ClassSpecifierEventArgs args = new ClassSpecifierEventArgs();                
                List<string> cleanedSpecifiers = ReadAllFields();
                args.ClassSpecifiers = cleanedSpecifiers;
                this.OKButtonEvent(this, args);
                Close();
            }
        }
        public ClassSpecifiersWindow()
        {
            InitializeComponent();
            viewModel = new ClassSpecifierViewModel();
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Sends the class specifier to the class model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            On_OKButtonPressed();
        }

        /// <summary>
        /// TODO: This is sooo ugly... It assumes that all UI Elements are in the right order. Probably a programmatic generation would help.
        /// </summary>
        private List<string> ReadAllFields()
        {
            // Container for all new specifier values (in case of checkboxes only their names, otherwise the content of the textbox in parenthesis
            List<string> allSpecifierValues = new List<string>();

            // Get all specifiers, needed to match the elements and the corresponding values
            List<ClassSpecifier> allSpecifiers = viewModel.AllSpecifiers.ToList<ClassSpecifier>();

            // Only get Checkboxes and Textboxes
            List<UIElement> relevantUIElements = new List<UIElement>();
            foreach (UIElement element in baseGrid.Children)
            {
                if(element is CheckBox || element is TextBox)
                {
                    relevantUIElements.Add(element);
                }
            }

            // Iterate over all Checkboxes and Textboxes and extract their values
            for (int i = 0; i < relevantUIElements.Count; i++)
	        {
                UIElement element = relevantUIElements[i];

                if(element is CheckBox)
                {
                    CheckBox checkBoxElement = element as CheckBox;
                    if((bool)checkBoxElement.IsChecked)
                    {
                        // In case the control is a checkbox and it's ticked (true), write the name of the corresponding specifier into the list
                        allSpecifierValues.Add(allSpecifiers[i].Name);              
                    }
                }
                else if (element is TextBox)
                {
                    TextBox textBoxElement = element as TextBox;
                    if (textBoxElement.Text != "")
                    {
                        // In case the control is a textbox and there is a value, write the name of the corresponding specifier plus the value of the textbox into the list
                        allSpecifierValues.Add(allSpecifiers[i].Name + "=(" + textBoxElement.Text + ")");
                    }
                }
            }
            return allSpecifierValues;
        }
    }
}
