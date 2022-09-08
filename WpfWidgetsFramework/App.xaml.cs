using HandyControl.Controls;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using WidgetBase;
using WpfWidgetsFramework.Common;
using MessageBox = HandyControl.Controls.MessageBox;

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

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            try
            {
                Plugins = new PluginLoader().Load();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("我们很抱歉，当前应用程序遇到一些问题，该操作已经终止，请进行重试，如果问题继续存在，请联系管理员.", "意外的操作", MessageBoxButton.OK, MessageBoxImage.Information);//这里通常需要给用户一些较为友好的提示，并且后续可能的操作

            File.WriteAllText("err.log", JsonConvert.SerializeObject(e.Exception));

            e.Handled = true;//使用这一行代码告诉运行时，该异常被处理了，不再作为UnhandledException抛出了。
        }
    }
}
