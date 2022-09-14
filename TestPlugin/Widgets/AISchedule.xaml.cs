using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using Flurl.Http;
namespace TestPlugin.Widgets
{
    /// <summary>
    /// AISchedule.xaml 的交互逻辑
    /// </summary>
    public partial class AISchedule : Page, WidgetBase.IWidget
    {
        VM.AISchedule vm;
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };

        public class sectionTime
        {
            public int i { get; set; }
            /// <summary>
            /// 开始          
            /// </summary>
            public string s { get; set; }
            /// <summary>
            /// 结束
            /// </summary>
            public string e { get; set; }
        }

        public static List <sectionTime> sections = new List <sectionTime> ();


        #region 接口实现
        public string WName => "小爱课程表";

        public string UID => "aischedule";

        public string Description => "哎嘿";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(250,250);

        public Window WWindow { get; set; } = null;

        #endregion
        public AISchedule()
        {
            InitializeComponent();
        }

        public Action OnEnabled => async () =>
        {
            vm=new VM.AISchedule();
            DataContext = vm;
            timer.Start();

            DataUpdate(false);
            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };
        };
        
        private void DataUpdate(bool v=false)
        {
            vm.LoadTable();
        }

        public Action OnDisabled => () =>
        {
            timer.Stop();

        };

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            popup.IsOpen = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                var strPath = url.Text.Substring(url.Text.IndexOf("linkToken=") + "linkToken=".Length);

                byte[] bpath = Convert.FromBase64String(strPath);
                var linkToken = System.Text.ASCIIEncoding.Default.GetString(bpath);

                linkToken = System.Web.HttpUtility.UrlDecode(linkToken);

                var tokens = linkToken.Split('&');
                var a = $"https://i.ai.mi.com/course-multi/table?ctId={tokens[4]}&userId={tokens[0]}&deviceId={tokens[1]}&sourceName=course-app-browser";

                Task.Run(async () =>
                {
                    if (!Directory.Exists("Config"))
                    {
                        Directory.CreateDirectory("Config");
                    }

                    await a.DownloadFileAsync("Config", "table.json");
                    vm.LoadTable();
                });
            }
            catch (Exception ex)
            {
                vm.ErrMsg = ex.Message;
                vm.ErrVi = Visibility.Visible;

            }

        }
    }
}
