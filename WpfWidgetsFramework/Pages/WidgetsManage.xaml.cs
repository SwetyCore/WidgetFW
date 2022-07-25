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
using WidgetBase;

namespace WpfWidgetsFramework.Pages
{
    /// <summary>
    /// WidgetsManage.xaml 的交互逻辑
    /// </summary>
    public partial class WidgetsManage : Page
    {
        VM.WidgetsManage vm = new();
        public WidgetsManage()
        {
            InitializeComponent();

            var mw = Application.Current.MainWindow as MainWindow;

            vm.Status = new();

            foreach (var item in mw.Plugins)
            {
                foreach (var w in item.Widgets)
                {
                    vm.Status.Add(new VM.WidgetsManage.WidgetStatue()
                    {
                        widget = w,
                        plugin = item,
                        enabled = false
                    });
                }
            }

            DataContext = vm;
        }
    }
}
