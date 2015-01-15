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
using Microsoft.WindowsAPICodePack.Dialogs;
using Unreal_Class_Wizard.ViewModel;
using System.ComponentModel;
using System.Diagnostics;


namespace Unreal_Class_Wizard.View
{
    /// <summary>
    /// Interaktionslogik für GeneralInformationView.xaml
    /// </summary>
    public partial class UserInformationView : Page
    {
        UserInformationViewModel viewModel;

        public UserInformationView()
        {
            InitializeComponent();
            viewModel = new UserInformationViewModel(DesignerProperties.GetIsInDesignMode(this));
            this.DataContext = viewModel;
        }

        void NavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            Application.Current.Shutdown();
        }


        private void GetSavePathHeader(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog("Choose Folder") { IsFolderPicker = true };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                viewModel.HeaderPath = dialog.FileName;
            }
        }

        private void GetSavePathCpp(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog("Choose Folder") { IsFolderPicker = true };
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                viewModel.CppPath = dialog.FileName;
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void userFileLink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(viewModel.UserFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }



    }
}
