using SharpVectors.Converters;
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
using Flurl.Http;
using WidgetBase;
using static TestPlugin.VM.Weather;
using Flurl;

namespace TestPlugin.Widgets
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class Weather : Page,IWidget
    {
        VM.Weather vm = new VM.Weather();
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 30, 0) };
        public Weather()
        {
            new SvgViewbox();
            InitializeComponent();
        }
        #region 接口实现
        public string WName => "天气";

        public string UID => "weather";

        public string Description => "天气实况";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(100, 100);

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


        private async Task DataUpdate()
        {
            await Task.Run(async () =>
            {
                string current_api = "https://api.msn.cn/weather/current";
                var s =  current_api.SetQueryParams(new
                {
                    /*
                locale: zh-cn
                units: C
                appId: 9e21380c-ff19-4c78-b4ea-19558e93a5d3
                apiKey: j5i4gDqHL6nGYwx5wi5kRhXjtf2c5qgFX9fzfk0TOo
                ocid: msftweather
                wrapOData: false
                getCmaAlert: true
                    */
                    locale = "zh-cn",
                    units = "C",
                    appId = "9e21380c-ff19-4c78-b4ea-19558e93a5d3",
                    apiKey = "j5i4gDqHL6nGYwx5wi5kRhXjtf2c5qgFX9fzfk0TOo",
                    ocid = "msftweather",
                    wrapOData= "false",
                    getCmaAlert="true",

                });
                vm.Current =await s.GetJsonAsync<WeatherCurrent.Root>();

            });
        }

    }
}
