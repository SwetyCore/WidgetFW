using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WidgetBase;
using static TestPlugin.VM.BiliHelper;

namespace TestPlugin.Widgets
{
    /// <summary>
    /// BiliHelper.xaml 的交互逻辑
    /// </summary>
    public partial class BiliHelper : Page, IWidget
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
        VM.BiliHelper vm = new VM.BiliHelper();
        String Cookie = "";
        public BiliHelper()
        {
            InitializeComponent();
        }

        #region 接口实现
        public string WName => "B站卡片";

        public string UID => "bilicard";

        public string Description => "哎嘿";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(200, 150);

        public Window WWindow { get; set; } = null;

        #endregion

        public Action OnEnabled => async () =>
        {

            DataContext = vm;
            vm.Loading = true;
            timer.Start();

            DataUpdate(false);
            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };
        };

        public Action OnDisabled => () =>
        {
            timer.Stop();

        };

        public static string GetMidFromCookie(string cookie)
        {
            List<string> DedeUserID = cookie.Split("; ").Where(x => x.IndexOf("DedeUserID=") != -1).ToList();
            if (DedeUserID.Count == 0)
            {
                return "";
            }
            else
            {
                return DedeUserID[0].Replace("DedeUserID=", "");
            }
        }
        /// <summary>
        /// http://api.bilibili.com/x/web-interface/card?mid=196435612&photo=true
        /// </summary>
        public async void DataUpdate(bool tip = true)
        {
            if (Cookie != null && Cookie != "" && GetMidFromCookie(Cookie) != "")
            {
                try
                {
                    string api = "http://api.bilibili.com/x/web-interface/card";
                    string url = api.SetQueryParams(new { mid = GetMidFromCookie(Cookie), photo = "true" });
                    vm.Card = await url.GetJsonAsync<web_interface_card.Root>();
                    vm.Loading = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (tip)
            {
                MessageBox.Show("哔哩哔哩:无效的Cookie!");
                vm.Loading = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dl = new CookieGetter("https://passport.bilibili.com/login");
            dl.ShowDialog();
            Cookie = dl.Cookie;
            DataUpdate(true);
        }
    }
}
