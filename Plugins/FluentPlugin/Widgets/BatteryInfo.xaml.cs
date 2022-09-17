
using CommunityToolkit.Mvvm.ComponentModel;
using DefaultWidgets.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WidgetBase;
using static DefaultWidgets.Utils.SysBatteryInfo;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// SearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class BatteryInfo : Page, IWidget
    {
        private SysBatteryInfo batteryInfo;
        private DispatcherTimer dt;
        private BatteryInfoVM vm;

        #region 接口实现
        public string WName => "电量控制";

        public string UID => "batteryinfo";

        public string Description => "哎嘿";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(200, 50);

        public IWidgetWindow WWindow { get; set; } = null;

        public Action OnEnabled => () => { };

        public Action OnDisabled => () => { };

        #endregion

        public BatteryInfo()
        {
            InitializeComponent();
        }

        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            batteryInfo = new SysBatteryInfo();
            vm = new BatteryInfoVM();
            this.DataContext = vm;
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(60);
            dt.Tick += dt_Tick;
            dt.Start();
            dt_Tick(null, null);

            WWindow.UseDefaultCard(true);

        }

        void dt_Tick(object sender, EventArgs e)
        {
            SystemPowerStatus systemPowerStatus = batteryInfo.Get();
            vm.Value = systemPowerStatus.BatteryLifePercent;
            vm.Icon = systemPowerStatus.ACLineStatus == ACLineStatus_.Online ? "\uea93" : "\ue83f";
            vm.Text = vm.Value.ToString();
        }
    }
    partial class BatteryInfoVM : ObservableObject
    {
        [ObservableProperty]
        private int _value;

        [ObservableProperty]
        private string _icon;

        [ObservableProperty]

        private string _text;


    }
}
