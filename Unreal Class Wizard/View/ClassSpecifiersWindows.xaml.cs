﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        private const int itemsPerColumn = 18;
        private const int heightRow = 38;
        private const int widthBufferColumn = 20;
        private const int widthLabelColumn = 170;
        private const int widthValueColumn = 180;
        private const int widthButton = 24;


        private int numberOfLogicalColumns = 0;
        private int numberOfActualColumns = 0;
        private int numberOfRows = 0;

        private ClassSpecifierViewModel viewModel;
        public event RoutedEventHandler OKButtonEvent;
        private bool restoreIfMove;
        

        public ClassSpecifiersWindow(List<ClassSpecifier> ClassSpecifierValues)
        {
            InitializeComponent();
            //this.SourceInitialized += Window_SourceInitialized;

            // TODO: Should already work? To be investigated...
            this.titleBar.Type = Unreal_Class_Wizard.CustomControls.UE4StyleTitleBarType.CloseOnly;

            viewModel = new ClassSpecifierViewModel(ClassSpecifierValues);
            this.DataContext = viewModel;

            DetermineNumberOfColumnsAndRows();
            ResizeWindow();
        }


        public void ResizeWindow()
        {
            int totalWidth = numberOfLogicalColumns * (widthBufferColumn + widthLabelColumn + widthValueColumn + widthButton) + 12;

            int header = (int)baseGrid.RowDefinitions[0].Height.Value;
            int footer = (int)baseGrid.RowDefinitions[2].Height.Value;
            int totalHeight = header + ((numberOfRows + 1) * heightRow) + footer;
            this.Width = totalWidth;
            this.Height = totalHeight;
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

        public void DetermineNumberOfColumnsAndRows()
        {
            int numberOfClassSpecifiers = viewModel.AllSpecifiers.Count;

            numberOfLogicalColumns = numberOfClassSpecifiers / itemsPerColumn;
            numberOfLogicalColumns = numberOfClassSpecifiers % itemsPerColumn > 0 ? numberOfLogicalColumns + 1 : numberOfLogicalColumns;      // Add one if there is a partially filled column
            numberOfActualColumns = numberOfLogicalColumns * 3;                                                                               // Every "logical" column consists of three columns: buffer, label, value

            numberOfRows = itemsPerColumn;                                     // Divide the number of total entries by the number of logical columns
        }

        protected void On_OKButtonPressed()
        {
            if (this.OKButtonEvent != null)
            {
                ClassSpecifierEventArgs args = new ClassSpecifierEventArgs();
                List<ClassSpecifier> newSpecifiers = ReadSpecifiers();
                args.ClassSpecifiers = newSpecifiers;
                this.OKButtonEvent(this, args);
                Close();
            }
        }

        private List<ClassSpecifier> ReadSpecifiers()
        {

            // Get all specifiers, needed to match the elements and the corresponding values
            List<ClassSpecifier> allSpecifiers = viewModel.AllSpecifiers.ToList<ClassSpecifier>();


            // Only get Checkboxes and Textboxes
            List<UIElement> relevantUIElements = new List<UIElement>();
            foreach (UIElement element in itemGrid.Children)
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
                        allSpecifiers[i].Value = true;              
                    }
                }
                else if (element is TextBox)
                {
                    TextBox textBoxElement = element as TextBox;
                    if (textBoxElement.Text != "")
                    {
                        allSpecifiers[i].Value = textBoxElement.Text;
                    }
                }
            }
            return allSpecifiers;
        }


        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                try
                {
                    this.DragMove();
                }
                catch (Exception)
                {
                    // TODO: Handle
                }
            }
        }

        #region Resize/Maximize/Minimize stuff

        void Window_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr mWindowHandle = (new WindowInteropHelper(this)).Handle;
            HwndSource.FromHwnd(mWindowHandle).AddHook(new HwndSourceHook(WindowProc));
        }


        private static System.IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    break;
            }

            return IntPtr.Zero;
        }


        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {
            POINT lMousePosition;
            GetCursorPos(out lMousePosition);

            IntPtr lPrimaryScreen = MonitorFromPoint(new POINT(0, 0), MonitorOptions.MONITOR_DEFAULTTOPRIMARY);
            MONITORINFO lPrimaryScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
            {
                return;
            }

            IntPtr lCurrentScreen = MonitorFromPoint(lMousePosition, MonitorOptions.MONITOR_DEFAULTTONEAREST);

            MINMAXINFO lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            if (lPrimaryScreen.Equals(lCurrentScreen) == true)
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcWork.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcWork.Right - lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcWork.Bottom - lPrimaryScreenInfo.rcWork.Top;
            }
            else
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcMonitor.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcMonitor.Right - lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcMonitor.Bottom - lPrimaryScreenInfo.rcMonitor.Top;
            }

            Marshal.StructureToPtr(lMmi, lParam, true);
        }


        private void SwitchWindowState()
        {
            switch (WindowState)
            {
                case System.Windows.WindowState.Normal:
                    {
                        WindowState = System.Windows.WindowState.Maximized;
                        break;
                    }
                case System.Windows.WindowState.Maximized:
                    {
                        WindowState = System.Windows.WindowState.Normal;
                        break;
                    }
            }
        }


        private void rctHeader_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if ((ResizeMode == ResizeMode.CanResize) || (ResizeMode == ResizeMode.CanResizeWithGrip))
                {
                    SwitchWindowState();
                }

                return;
            }

            else if (WindowState == System.Windows.WindowState.Maximized)
            {
                restoreIfMove = true;
                return;
            }

            DragMove();
        }


        private void rctHeader_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            restoreIfMove = false;
        }


        private void rctHeader_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (restoreIfMove)
            {
                restoreIfMove = false;

                double percentHorizontal = e.GetPosition(this).X / ActualWidth;
                double targetHorizontal = RestoreBounds.Width * percentHorizontal;

                double percentVertical = e.GetPosition(this).Y / ActualHeight;
                double targetVertical = RestoreBounds.Height * percentVertical;

                WindowState = System.Windows.WindowState.Normal;

                POINT lMousePosition;
                GetCursorPos(out lMousePosition);

                Left = lMousePosition.X - targetHorizontal;
                Top = lMousePosition.Y - targetVertical;

                DragMove();
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        enum MonitorOptions : uint
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }


        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }

        #endregion 

        private void CloseWindow()
        {
            Close();
        }


    }

    
}
