using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfWidgetsFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool first = true;
        public MainWindow()
        {
            InitializeComponent();
            if (first)
            {

                var b = Pages.WidgetsManage.LoadCfg();
                if (!b)
                {
                    Close();
                }
                first = false;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nav.SelectedIndex = 0;
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var i = (sender as ListBox).SelectedIndex;
            if (i==0)
            {
                RootFrame.Navigate(new Pages.WidgetsManage());
            }
            if (i == 1)
            {
                RootFrame.Navigate(new Pages.PluginsManage());

            }
            if (i == 2)
            {
                RootFrame.Navigate(new Pages.AboutPage());

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState=WindowState.Minimized;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
