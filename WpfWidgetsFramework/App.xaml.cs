using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WidgetBase;
using WpfWidgetsFramework.Common;

namespace WpfWidgetsFramework
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IEnumerable<IPlugin> Plugins { get; set; }

        public VM.WidgetsManage wmvm;

        protected override void OnStartup(StartupEventArgs e)
        {
            Plugins = new PluginLoader().Load();
            wmvm = new VM.WidgetsManage();
            wmvm.Status = new();

            foreach (var item in Plugins)
            {
                foreach (var w in item.Widgets)
                {
                    wmvm.Status.Add(new VM.WidgetsManage.WidgetStatue()
                    {
                        widget = w,
                        plugin = item,
                        enabled = false
                    });
                }
            }

            base.OnStartup(e);  
        }
    }
}
