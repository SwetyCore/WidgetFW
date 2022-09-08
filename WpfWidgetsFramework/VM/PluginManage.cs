using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.Generic;
using WidgetBase;

namespace WpfWidgetsFramework.VM
{
    public class PluginManage : ObservableObject
    {

        private List<IPlugin> plugins;

        public List<IPlugin> Plugins
        {
            get { return plugins; }
            set { SetProperty(ref plugins, value); }
        }
    }
}
