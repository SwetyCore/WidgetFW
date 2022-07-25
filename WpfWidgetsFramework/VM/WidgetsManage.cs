using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WidgetBase;

namespace WpfWidgetsFramework.VM
{
    internal class WidgetsManage:ObservableObject
    {
        internal class WidgetStatue
        {
            public IWidget widget { get; set; }
            public IPlugin plugin { get; set; }
            public bool enabled { get; set; }
        }

        private ObservableCollection<WidgetStatue> _status;

        public ObservableCollection<WidgetStatue> Status
        {
            get { return _status; }
            set { SetProperty( ref _status , value); }
        }


    }
}
