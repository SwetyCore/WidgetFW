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
        public string WName => throw new NotImplementedException();

        public string UID => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public Action OnEnabled => ()=>{ };

        public Action OnDisabled => () => { };

        public Page Widget => this;

        public object Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
