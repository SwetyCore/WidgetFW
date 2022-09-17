using System;
using System.Threading.Tasks;
using System.Windows;

namespace DefaultStyle
{
    /// <summary>
    /// CookieGetter.xaml 的交互逻辑
    /// </summary>
    public partial class CookieGetter : Window
    {
        public CookieGetter(string url)
        {
            InitializeComponent();
            wv2.Source = new Uri(url);
        }
        public string Cookie { get; set; }
        private void GetCookieBtn_Click(object sender, RoutedEventArgs e)
        {
            GetCookie();
        }

        public async Task GetCookie()
        {
            Cookie = await wv2.ExecuteScriptAsync("document.cookie");
            Close();
        }

        private void wv2_Initialized(object sender, EventArgs e)
        {

        }
    }
}
