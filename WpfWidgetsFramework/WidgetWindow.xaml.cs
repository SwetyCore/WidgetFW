using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using WidgetBase;
using static WpfWidgetsFramework.VM.WidgetsManage;

namespace WpfWidgetsFramework
{
    /// <summary>
    /// WidgetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetWindow : Window, IWidgetWindow
    {

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);


        IWidget widget;
        WidgetStatue ws;
        public WidgetWindow(WidgetStatue t)
        {
            InitializeComponent();
            ws = t;
            widget = ws.widget;
            frame.Content = widget;
            widget.WWindow = this;

            Top = ws.position.Y;
            Left = ws.position.X;

            Width = widget.Size.X * 1.25;
            Height = widget.Size.Y * 1.25;

            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            SetWindowLong(hWnd, (-20), 0x80);
        }

        public void UseDefaultCard(bool a=true)
        {
            if (a)
            {
                defaultCard.Background = new SolidColorBrush(Color.FromArgb(255,255,255,255));
            }
            else
            {
                defaultCard.Background = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));

            }
        }

        private void WindowClose(object sender, RoutedEventArgs e)
        {
            ws.enabled = false;

            ws.widget.WWindow = null;
            //App app = Application.Current as App;
            //app.wmvm.Status.FirstOrDefault().enabled = false;
            Close();
        }

        private void WindowTopMost(object sender, RoutedEventArgs e)
        {
            Topmost = !Topmost;
            if (Topmost)
            {
                topmostMI.Header = "取消置顶";
            }
            else
            {
                topmostMI.Header = "窗口置顶";

            }
        }

        private void ShowSetting(object sender, RoutedEventArgs e)
        {
            var mw = Application.Current.MainWindow;
            try
            {
                mw.Close();
            }
            catch { 

            }

            mw = new MainWindow();
            mw.Show();
            //if (!mw.IsLoaded)
            //{
            //    mw = new MainWindow();
            //    mw.Show();
            //}
            //else
            //{
            //    mw.WindowState = WindowState.Normal;
            //    mw.Topmost = true;
            //    mw.Topmost = false;
            //}
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Pages.WidgetsManage.SaveCfg();
            Environment.Exit(0);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            ws.position = new Point(Left, Top);
        }
    }
}
