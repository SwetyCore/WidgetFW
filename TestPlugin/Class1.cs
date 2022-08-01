using System;
using System.Collections.Generic;
using WidgetBase;

namespace TestPlugin
{
    public class Class1 : IPlugin
    {
        public string Name => "测试小部件集合";

        public string Description => "测试描述";

        public Version Version =>new Version("2.1.1");

        public string Author => "SwetyCore";

        public string Home => "https://github.com";

        public string Guid =>"hesdwdwqwe";

        public bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IWidget> Widgets => new List<IWidget>()
        {
            new TestPlugin.Widgets.ResMonitor(),
            new TestPlugin.Widgets.Clock(),
            new TestPlugin.Widgets.Weather(),
            new TestPlugin.Widgets.BiliHelper(),
            new TestPlugin.Widgets.GenshinHelper(),
        };

        public int Execute()
        {
            throw new NotImplementedException();
        }
    }
}
