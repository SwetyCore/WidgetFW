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
using WidgetBase;
using static WpfWidgetsFramework.VM.WidgetsManage;

namespace WpfWidgetsFramework
{
    /// <summary>
    /// WidgetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetWindow : Window
    {

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

            Width = widget.Size.X*1.25;
            Height = widget.Size.Y*1.25;
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
            var mw=Application.Current.MainWindow;
            if (!mw.IsLoaded) 
            {
                mw = new MainWindow();
                mw.Show();
            }
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
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
