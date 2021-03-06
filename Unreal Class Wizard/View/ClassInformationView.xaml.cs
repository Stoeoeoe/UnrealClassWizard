﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class ClassInformationView : Page
    {
        public UnrealClassViewModel ViewModel {get;set;}


        public ClassInformationView()
        {
            InitializeComponent();

            ViewModel = new UnrealClassViewModel(DesignerProperties.GetIsInDesignMode(this));
            this.DataContext = ViewModel;
            //SetFocusOnNameBox();

        }

        // TODO: Could not be handled via DataBinding due to Nullpointer Exceptions, to be corrected.
        private void BaseClassChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.CurrentBaseClass = (sender as ComboBox).SelectedItem as BaseClass;
        }

        /// <summary>
        /// Opens the ClassSpecifiers window.
        /// </summary>
        private void otherClassSpecifiersButton_Click(object sender, RoutedEventArgs e)
        {
            ClassSpecifiersWindow classSpecifierWindow = new ClassSpecifiersWindow(ViewModel.ClassModel.ClassSpecifiers);
            classSpecifierWindow.OKButtonEvent += new RoutedEventHandler(RouteClassSpecifiers);
            classSpecifierWindow.Show();
        }

        /// <summary>
        /// Gets called when the ClassSpecifiers windows is closed, routes the new ClassSpecifiers to the current class.
        /// </summary>
        private void RouteClassSpecifiers(object sender, RoutedEventArgs e)
        {
            ClassSpecifierEventArgs classSpecifierEventArgs = (ClassSpecifierEventArgs)e;
            ViewModel.UpdateClassSpecifiers(classSpecifierEventArgs.ClassSpecifiers);
        }




    }
}
