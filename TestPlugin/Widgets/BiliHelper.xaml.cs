using Flurl;
using Flurl.Http;
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
using System.Windows.Threading;
using WidgetBase;
using static TestPlugin.VM.BiliHelper;

namespace TestPlugin.Widgets
{
    /// <summary>
    /// BiliHelper.xaml 的交互逻辑
    /// </summary>
    public partial class BiliHelper : Page,IWidget
    {
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
        VM.BiliHelper vm = new VM.BiliHelper();
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
        public Point Size => new(300, 200);

        public Window WWindow { get; set; } = null;

        #endregion

        public Action OnEnabled => async () =>
        {

            DataContext = vm;
            timer.Start();

            DataUpdate();
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
            string Cookie = "buvid3=7F674FC0-5CB0-F077-036F-9B4FE62DB24331528infoc; i-wanna-go-back=-1; _uuid=B9716EB5-5D97-10162-F3C4-37ADFC2DCDDD33415infoc; buvid4=C9812B7A-D134-4703-EFBE-2F19EA44E95034121-022071512-fs30e/wXVzF+VextIYyCZUTr1b3QjpsfXV6v6qBJZAT21pGay5ubhg%3D%3D; CURRENT_BLACKGAP=0; rpdid=0zbfAHRcO4|12PaVwEa5|1p|3w1Ocd0a; fingerprint=9681eba18bb19e664673b8b2b13307ce; buvid_fp_plain=undefined; DedeUserID=156785512; DedeUserID__ckMd5=99e73cdaf36589d6; bili_jct=2783e3df88d6884d9ae390d0da8f0b7d; buvid_fp=9681eba18bb19e664673b8b2b13307ce; blackside_state=0; b_ut=5; nostalgia_conf=-1; LIVE_BUVID=AUTO8016580232683158; hit-dyn-v2=1; PVID=1; CURRENT_FNVAL=4048; bsource=share_source_qqchat; theme_style=light; sid=6160dplx; innersign=0; b_lsid=4121B54E_182523D4BBD; b_timer=%7B%22ffp%22%3A%7B%22333.788.fp.risk_7F674FC0%22%3A%221824F4BFF2F%22%2C%22333.999.fp.risk_7F674FC0%22%3A%221824F4E688D%22%2C%22333.1007.fp.risk_7F674FC0%22%3A%22182523D4EF7%22%7D%7D; bp_video_offset_156785512=688878858413277200";
            if (Cookie != null && Cookie != "" && GetMidFromCookie(Cookie) != "")
            {
                try
                {
                    string api = "http://api.bilibili.com/x/web-interface/card";
                    string url = api.SetQueryParams(new { mid = GetMidFromCookie(Cookie), photo = "true" });
                    vm.Card = await url.GetJsonAsync<web_interface_card.Root>();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (tip)
            {
                MessageBox.Show("哔哩哔哩:无效的Cookie!");
            }
        }
    }
}
