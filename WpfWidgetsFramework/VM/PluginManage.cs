using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Diagnostics;
using WidgetBase;

namespace WpfWidgetsFramework.VM
{
    partial class PluginManage : ObservableObject
    {

        private List<IPlugin> plugins;

        public List<IPlugin> Plugins
        {
            get { return plugins; }
            set { SetProperty(ref plugins, value); }
        }


        [RelayCommand]
        private void OpenFolder()
        {
            Process.Start("explorer.exe", ".\\Plugins");
        }
    }
}
