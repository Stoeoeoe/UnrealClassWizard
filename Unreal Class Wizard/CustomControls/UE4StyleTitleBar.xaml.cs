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

        public UE4StyleTitleBarType Type
        {
            get { return (UE4StyleTitleBarType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register("Type", typeof(UE4StyleTitleBarType), typeof(UE4StyleTitleBar), new PropertyMetadata(UE4StyleTitleBarType.Full));



        public UE4StyleTitleBar()
        {
            InitializeComponent();
            switch (Type)
            {
                case UE4StyleTitleBarType.Full:
                    break;
                case UE4StyleTitleBarType.CloseOnly:
                    restoreButton.Visibility = System.Windows.Visibility.Collapsed;
                    minimizeButton.Visibility = System.Windows.Visibility.Collapsed;
                    maximizeButton.Visibility = System.Windows.Visibility.Collapsed;
                    break;
                default:
                    break;
            }
        }


        private void CloseWindow(object sender, RoutedEventArgs args)
        {
            MessageBoxResult result = MessageBox.Show("This class has not yet been generated.\r\nDo you really want to quit?", "Exit Program", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if(result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        private void Maximize(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Maximized;
            maximizeButton.Visibility = System.Windows.Visibility.Collapsed;
            restoreButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Restore(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Normal;
            maximizeButton.Visibility = System.Windows.Visibility.Visible;
            restoreButton.Visibility = System.Windows.Visibility.Collapsed;

        }


        public enum UE4StyleTitleBarType
        {
            Full,
            CloseOnly
        }
    }
}
