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
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Wpf;

namespace TestPlugin
{
    /// <summary>
    /// CookieGetter.xaml 的交互逻辑
    /// </summary>
    public partial class CookieGetter : Window
    {
        public CookieGetter(string url)
        {
            new WebView2().Dispose();
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
    }
}
