using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WidgetBase;

namespace TestPlugin.Widgets
{
    /// <summary>
    /// Clock.xaml 的交互逻辑
    /// </summary>
    public partial class Clock : Page, IWidget
    {
        VM.Clock vm = new VM.Clock();
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };
        public Clock()
        {
            InitializeComponent();

        }

        public string WName => "时钟";

        public string UID => "clock";

        public string Description => "简简单单的时钟";

        public Action OnEnabled => () =>
        {
            DataContext = vm;
            timer.Start();

            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };
        };

        public Action OnDisabled => () =>
        {

        };

        public Page Widget => new Page();

        public Window WWindow { get; set; }
        public object Config { get; set; }

        public Point Size => new Point(150, 150);


        private void DataUpdate()
        {

            DateTime now = DateTime.Now;
            vm.Hour = (360 / 12) * now.Hour;
            vm.Sec = (360 / 60) * now.Second;
            vm.Min = (360 / 60) * now.Minute;
        }


    }
}
