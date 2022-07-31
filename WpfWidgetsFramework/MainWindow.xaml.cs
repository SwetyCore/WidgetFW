﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using WidgetBase;
using WpfWidgetsFramework.Common;

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
                    this.WindowState=WindowState.Minimized;
                }
                first = false;

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
