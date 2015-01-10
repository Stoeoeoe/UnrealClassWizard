using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;
using System.Xml.Serialization;
using Unreal_Class_Wizard.Model;
using Unreal_Class_Wizard.ViewModel;
using Xceed.Wpf.Toolkit;

namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        /// <summary>
        ///  Controls window restore (snap functionality)
        /// </summary>
        private bool restoreIfMove = false;
        private int numberOfPages;
        private int currentPage;

        public MainPage()
        {
            InitializeComponent();
            this.SourceInitialized += Window_SourceInitialized;
            
            numberOfPages = navigationListBox.Items.Count;
            currentPage = 1;
            navigationFrame.Navigate(new Uri("View/UserInformationView.xaml", UriKind.Relative));

        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)e.AddedItems[0];
            currentPage = navigationListBox.SelectedIndex + 1;
            string content = (string)selectedItem.Content;

            switch (currentPage)
            {
                case 1: navigationFrame.Navigate(new Uri("View/UserInformationView.xaml", UriKind.Relative)); break;
                case 2: navigationFrame.Navigate(new Uri("View/ClassInformationView.xaml", UriKind.Relative)); break;
                case 3: navigationFrame.Navigate(new Uri("View/ConstructorView.xaml", UriKind.Relative)); break;
                default: break;
            }
        }


        #region Forward/Backward Logic

        private void PreviousPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(currentPage == 1)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void PreviousPage_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            currentPage--;
            navigationListBox.SelectedIndex = navigationListBox.SelectedIndex - 1;
        }

        private void NextPage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (currentPage == numberOfPages)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void NextPage_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            currentPage++;
            navigationListBox.SelectedIndex = navigationListBox.SelectedIndex + 1;
        }

    #endregion

        private void CloseApplication()
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("This class has not yet been generated.\r\nDo you really want to quit?", "Exit Program", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
