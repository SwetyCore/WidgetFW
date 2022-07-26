using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WidgetBase;

namespace WpfWidgetsFramework.VM
{
    public class WidgetsManage:ObservableObject
    {
        public class WidgetStatue:ObservableObject
        {
            public IWidget widget { get; set; }
            public IPlugin plugin { get; set; }
            public Point position { get; set; }

            private bool _enabled;
            public bool enabled 
            { 
                get { return _enabled; } 
                set 
                {
                    SetProperty(ref _enabled, value);
                    //_enabled = value;
                    if (value)
                    {
                        widget.OnEnabled();
                        if (widget.WWindow==null)
                        {
                            new WidgetWindow(this).Show();
                        }

                    }
                    else
                    {
                        if (widget.WWindow!=null)
                        {
                            widget.WWindow.Close();
                            widget.OnDisabled();
                        }




                    }
                } 
            }
        }

        private ObservableCollection<WidgetStatue> _status;

        public ObservableCollection<WidgetStatue> Status
        {
            get { return _status; }
            set { SetProperty( ref _status , value); }
        }


    }
}
