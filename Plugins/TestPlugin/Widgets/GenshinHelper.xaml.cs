using DGP.Genshin.GamebarWidget.MiHoYoAPI;
using DGP.Genshin.GamebarWidget.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WidgetBase;

namespace DefaultStyle.Widgets
{
    /// <summary>
    /// GenshinHelper.xaml 的交互逻辑
    /// </summary>
    public partial class GenshinHelper : Page, IWidget
    {
        public GenshinHelper()
        {
            InitializeComponent();
        }
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
        VM.GenshinHelper vm = new VM.GenshinHelper();
        String Cookie = "";


        #region 接口实现
        public string WName => "原神实时便笺";

        public string UID => "genshinhelper";

        public string Description => "[已失效]显示树脂和委托的小工具";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(300, 150);
        public IWidgetWindow WWindow { get; set; } = null;


        #endregion

        public Action OnEnabled => async () =>
        {

            DataContext = vm;
            timer.Start();
            vm.Loading = true;

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

        public async void DataUpdate(bool tip = true)
        {
            if (String.IsNullOrEmpty(Cookie))
            {
                return;
            }
            else
            {
                RefreshDailyNotePoolAsync(Cookie);

            }
        }
        public async Task RefreshDailyNotePoolAsync(string mycookie)
        {

            if (mycookie == "" | mycookie == null)
            {
                MessageBox.Show("未设置cookie!", "提示");
                vm.Loading = true;

                return;
            }

            Cookie cookie = new Cookie(mycookie);


            List<UserGameRole> roles = await new UserGameRoleProvider(cookie.CookieValue).GetUserGameRolesAsync();
            //roleAndNotes.Clear();
            if (roles.Count < 1)
            {
                MessageBox.Show("无效的Cookie或者无网络!", "提示");
                vm.Loading = true;
                return;
            }
            foreach (UserGameRole role in roles)
            {
                DailyNote note = await new DailyNoteProvider(cookie.CookieValue).GetDailyNoteAsync(role.Region, role.GameUid);
                //roleAndNotes.Add(new RoleAndNote { Role = role, Note = note });
                this.vm.RoleAndNote = new RoleAndNote { Role = role, Note = note };
                vm.Loading = false;
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var dl = new CookieGetter("https://bbs.mihoyo.com/ys/");
            dl.ShowDialog();
            Cookie = dl.Cookie;
            DataUpdate(true);
        }
    }
}
