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

        public string Home { get; }
        public IEnumerable<IWidget> Widgets { get; }

    }


    public interface IWidget
    {
        public string WName { get; }
        public string UID { get; }
        public string Description { get; }
        public Page Widget { get; }

        public IWidgetWindow WWindow { get; set; }

        public Object Config { get; set; }
        public Point Size { get; }


        public Action OnEnabled { get; }
        public Action OnDisabled { get; }
    }

    public interface IWidgetWindow
    {
        public void UseDefaultCard(bool a=true);

        public void Close();
    }

}
