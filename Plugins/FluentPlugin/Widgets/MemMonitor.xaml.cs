using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WidgetBase;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class MemMonitor : Page, IWidget
    {
        #region 接口实现
        public string WName => "小爱课程表";

        public string UID => "aischedule";

        public string Description => "哎嘿";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(250, 250);

        public IWidgetWindow WWindow { get; set; } = null;

        public Action OnEnabled => throw new NotImplementedException();

        public Action OnDisabled => throw new NotImplementedException();

        #endregion

        private PerformanceCounter MEMCommitedPerc;
        private DispatcherTimer dt;
        private MemMonitorVM vm;



        public MemMonitor()
        {
            InitializeComponent();
        }


        private void WidgetBase_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new MemMonitorVM();
            this.DataContext = vm;
            MEMCommitedPerc = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dt_Tick;
            dt.Start();
        }
        public void getprocessorUtility()
        {
            vm.MemAvailable = MEMCommitedPerc.NextValue();
            vm.Text = Math.Round(vm.MemAvailable, 1).ToString();

        }
        void dt_Tick(object sender, EventArgs e)
        {
            getprocessorUtility();
        }
    }


    partial class MemMonitorVM : ObservableObject
    {
        [ObservableProperty]
        private float _memAvailable;


        [ObservableProperty]
        private string _text;




    }
}
