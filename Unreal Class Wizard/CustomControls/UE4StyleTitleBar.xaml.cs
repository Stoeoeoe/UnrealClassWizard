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
        public delegate void CloseEventHandler();
        public event CloseEventHandler CloseEvent;


        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(UE4StyleTitleBar), new PropertyMetadata("Title"));

        public string Title
        {
            get { return (string)base.GetValue(TitleProperty); }
            set { base.SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("Type", typeof(UE4StyleTitleBarType), typeof(UE4StyleTitleBar));

        public UE4StyleTitleBarType Type
        {
            get { return (UE4StyleTitleBarType)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); CollapseButtons(value); }
        }




        public UE4StyleTitleBar()
        {
            InitializeComponent();

        }


        private void CloseWindowButtonPressed(object sender, RoutedEventArgs args)
        {
            CloseEvent();
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

        private void CollapseButtons(UE4StyleTitleBarType Type)
        {
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
    }

    public enum UE4StyleTitleBarType
    {
        Full,
        CloseOnly
    }
}
