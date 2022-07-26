using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBase;

namespace WpfWidgetsFramework.VM
{
    public class PluginManage:ObservableObject
    {

        private List<IPlugin> plugins;

        public List<IPlugin> Plugins
        {
            get { return plugins; }
            set { SetProperty(ref plugins, value); }
        }
    }
}
