using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.UI.Components.WindowTitleBar;
using ShopAIDesktop.UI.Pages.Login;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace ShopAIDesktop
{
    public partial class MainWindow : Window
    {
        private const int WM_GETMINMAXINFO = 0x0024;
        private const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

        public MainWindow()
        {
            InitializeComponent();

            SourceInitialized += MainWindow_SourceInitialized;

            var loginPage = ((App)Application.Current)
                .Services
                .GetRequiredService<LoginPage>();

            MainFrame.Navigate(loginPage);
        }


        public void SetTitle(string title)
        {
            MainTitleBar.Title = title;
        }

        public void IsMinimize(bool isMinimize)
        {
            MainTitleBar.IsMinimize = isMinimize;
        }

        public void IsMaximize(bool IsMaximize)
        {
            MainTitleBar.IsMaximize = IsMaximize;
        }

        private void MainWindow_SourceInitialized(
            object? sender,
            EventArgs e)
        {
            var handle = new WindowInteropHelper(this).Handle;

            HwndSource.FromHwnd(handle)?
                .AddHook(WindowProc);
        }

        private IntPtr WindowProc(
            IntPtr hwnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam,
            ref bool handled)
        {
            if (msg == WM_GETMINMAXINFO)
            {
                WmGetMinMaxInfo(hwnd, lParam);
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void WmGetMinMaxInfo(
            IntPtr hwnd,
            IntPtr lParam)
        {
            var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);

            var monitor = MonitorFromWindow(
                hwnd,
                MONITOR_DEFAULTTONEAREST);

            var monitorInfo = new MONITORINFO
            {
                cbSize = Marshal.SizeOf<MONITORINFO>()
            };

            GetMonitorInfo(
                monitor,
                ref monitorInfo);

            var workArea = monitorInfo.rcWork;
            var monitorArea = monitorInfo.rcMonitor;

            mmi.ptMaxPosition.X =
                workArea.Left - monitorArea.Left;

            mmi.ptMaxPosition.Y =
                workArea.Top - monitorArea.Top;

            mmi.ptMaxSize.X =
                workArea.Right - workArea.Left;

            mmi.ptMaxSize.Y =
                workArea.Bottom - workArea.Top;

            Marshal.StructureToPtr(
                mmi,
                lParam,
                true);
        }

        private void MainWindowStateCharge(
            object? sender,
            EventArgs e)
        {
            // Aquí mantenemos la lógica que ya tengas
            // relacionada con el cambio de estado.
        }


        #region Windows API

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(
            IntPtr hwnd,
            uint dwFlags);

        [DllImport("user32.dll")]
        private static extern bool GetMonitorInfo(
            IntPtr hMonitor,
            ref MONITORINFO lpmi);

        #endregion


        #region Native Structures

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(
            LayoutKind.Sequential,
            CharSet = CharSet.Auto)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        #endregion
    }
}