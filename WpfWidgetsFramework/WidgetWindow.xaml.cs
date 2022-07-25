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

namespace WpfWidgetsFramework
{
    /// <summary>
    /// WidgetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetWindow : Window
    {
        private Page CreateInstance(Type t)
        {
            return Activator.CreateInstance(t) as Page;

        }
        IWidget widget;
        public WidgetWindow(IWidget t)
        {
            InitializeComponent();
            widget = t;
            frame.Content = widget;
            widget.WWindow = this;

            Width = widget.Size.X;
            Height = widget.Size.Y;
        }

        private void WindowClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowTopMost(object sender, RoutedEventArgs e)
        {
            Topmost = true;
        }

        private void ShowSetting(object sender, RoutedEventArgs e)
        {

        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            
        }
    }
}
