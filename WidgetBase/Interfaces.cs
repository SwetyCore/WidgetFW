using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WidgetBase
{
    public interface IPlugin
    {
        public string Name { get; }

        public string Description { get; }

        public Version Version { get; }

        public string Author { get; }

        public string Home { get;  }
        public IEnumerable<IWidget> Widgets { get; }

    }


    public interface IWidget
    {
        public string WName { get; }
        public string UID { get; }
        public string Description { get; }
        public Action OnEnabled { get;  }
        public Action OnDisabled { get; }
        public Page Widget { get; }
        public Object Config { get;set;}
        public Point Size { get; }
    }
    
}
