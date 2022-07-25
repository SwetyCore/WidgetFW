using Newtonsoft.Json;
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

namespace TestPlugin.Widgets
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page, IWidget
    { 
        public Page1()
        {
            InitializeComponent();
        }
        public string WName => "测试部件名称";

        public string UID => "dwyfguiop2134e";

        public string Description => "测试描述";

        public Action OnEnabled => ()=>{

        };

        public Action OnDisabled => () => 
        { 
            this.WWindow = null;

        };

        public Page Widget => this;

        public object Config { get; set; }

        //50+8
        public Point Size => new(50,50);

        public Window WWindow { get; set; } =null;
    }
}
