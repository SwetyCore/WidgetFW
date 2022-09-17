
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WidgetBase;
using static DefaultWidgets.Utils.Weather.DataType;

namespace DefaultWidgets.Widgets
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class Weather : Page,IWidget
    {
        #region 接口实现
        public string WName => "小爱课程表";

        public string UID => "aischedule";

        public string Description => "哎嘿";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(250, 250);


        public Action OnEnabled => throw new NotImplementedException();

        public Action OnDisabled => throw new NotImplementedException();

        public IWidgetWindow WWindow { get; set; } = null;



        #endregion

        WeatherVM vm = new WeatherVM();
        public Weather()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = vm;
            RefreshAsync();

        }


        private async Task RefreshAsync()
        {

            try
            {
                Utils.Weather.API api = new Utils.Weather.API();
                var resp = await api.GetCurrent();

                vm.Weather = JsonConvert.DeserializeObject<WeatherCurrent.Root>(resp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            RefreshAsync();

        }
    }

    partial class WeatherVM : ObservableObject
    {
        [ObservableProperty]
        private WeatherCurrent.Root _weather;




    }
}
