using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfWidgetsFramework.Pages
{
    /// <summary>
    /// WidgetsManage.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetsManage : Page
    {
        const string SETTING_FILE = "setting.json";
        public WidgetsManage()
        {
            InitializeComponent();

            var app = Application.Current as App;

            DataContext = app.wmvm;
        }

        public class WidgetStatueCfg
        {
            public Point position;
            public bool enabled;
            public bool topmost;
        }

        public static bool LoadCfg()
        {
            var app = Application.Current as App;
            int count = 0;
            if (File.Exists(SETTING_FILE))
            {
                var s = File.ReadAllText(SETTING_FILE);
                Dictionary<string, WidgetStatueCfg> cfg = JsonConvert.DeserializeObject<Dictionary<string, WidgetStatueCfg>>(s);

                foreach (var item in app.wmvm.Status)
                {
                    try
                    {

                        item.position = cfg[item.widget.UID].position;
                        item.enabled = cfg[item.widget.UID].enabled;
                        if (item.enabled)
                        {
                            count++;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            return true;

        }

        public static void SaveCfg()
        {
            var app = Application.Current as App;

            Dictionary<string, WidgetStatueCfg> cfg = new Dictionary<string, WidgetStatueCfg>();

            foreach (var item in app.wmvm.Status)
            {
                cfg.Add(item.widget.UID, new WidgetStatueCfg()
                {
                    position = item.position,
                    enabled = item.enabled,

                });
            }

            File.WriteAllText(SETTING_FILE, JsonConvert.SerializeObject(cfg));
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            SaveCfg();
        }
    }
}
