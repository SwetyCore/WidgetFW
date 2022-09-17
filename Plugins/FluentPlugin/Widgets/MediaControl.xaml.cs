using CommunityToolkit.Mvvm.ComponentModel;
using DefaultWidgets.Utils;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WidgetBase;
namespace DefaultWidgets.Widgets
{

    /// <summary>
    /// MediaControl.xaml 的交互逻辑
    /// </summary>
    public partial class MediaControl : Page, IWidget
    {
        #region 接口实现
        public string WName => "媒体控制";

        public string UID => "mediacontrol";

        public string Description => "媒体控制";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(200, 100);

        public IWidgetWindow WWindow { get; set; } = null;

        #endregion



        private DispatcherTimer dt;
        private MediaControlVm vm;
        public MediaControl()
        {
            InitializeComponent();
            new Wpf.Ui.Controls.Badge();
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(2);
            dt.Tick += dt_Tick;
            dt.Start();
            vm = new MediaControlVm();
            this.DataContext = vm;
        }

        private string sName;

        public string SName
        {
            get { return sName; }
            set
            {
                if (sName != value)
                {
                    UpdateCover(value);

                }
                sName = value;
            }
        }

        public Action OnEnabled => () => { };

        public Action OnDisabled => () => { };

        public async void UpdateCover(string s)
        {
            if (s == null)
            {
                return;
            }
            string coverUrl = await NeteaseCloudMusic.FindAvator(s);
            if (coverUrl == null)
            {
                return;
            }
            vm.ImgSrc = BitmapFrame.Create(new Uri(coverUrl));
        }
        public void dt_Tick(object sender, EventArgs e)
        {
            SName = NeteaseCloudMusic.FindName();
            string QwQ = SName == "" ? "网易云音乐-未播放" : SName;
            if (QwQ.Split('-').Count() >= 2)
            {
                vm.Singer = QwQ.Split('-')[1];

                vm.MusicName = QwQ.Split('-')[0];

            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //BasicMethods.runcmd("start Assets\\Scripts\\previous.vbs");

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //BasicMethods.runcmd("start Assets\\Scripts\\play.vbs");

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //BasicMethods.runcmd("start Assets\\Scripts\\next.vbs");

        }
    }

    partial class MediaControlVm : ObservableObject
    {
        [ObservableProperty]

        private string _musicName;



        [ObservableProperty]
        private ImageSource _imgSrc;


        [ObservableProperty]
        private string _singer;


    }
}

