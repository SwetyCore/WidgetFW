using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WidgetBase;

namespace WpfWidgetsFramework.Pages
{
    /// <summary>
    /// WidgetsManage.xaml 的交互逻辑
    /// </summary>
    public partial class PluginsManage : Page
    {
        VM.PluginManage vm = new();
        public PluginsManage()
        {
            InitializeComponent();

            var mw = Application.Current as App;

            vm.Plugins = (List<IPlugin>)mw.Plugins;

            DataContext = vm;
        }
    }

}
