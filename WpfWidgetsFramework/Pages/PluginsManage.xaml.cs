using Microsoft.Toolkit.Mvvm.ComponentModel;
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
using WidgetBase;

namespace WpfWidgetsFramework.Pages
{
    /// <summary>
    /// WidgetsManage.xaml 的交互逻辑
    /// </summary>
    public partial class PluginsManage : Page
    {
        VM.PluginManage vm = new ();
        public PluginsManage()
        {
            InitializeComponent();

            var mw = Application.Current as App;

            vm.Plugins = (List<IPlugin>)mw.Plugins;

            DataContext = vm;
        }
    }

}
