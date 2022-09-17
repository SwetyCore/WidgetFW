using System;
using System.Collections.Generic;
using WidgetBase;

namespace DefaultStyle
{
    public class Class1 : IPlugin
    {
        public string Name => "����С��������";

        public string Description => "��������";

        public Version Version => new Version("2.1.1");

        public string Author => "SwetyCore";

        public string Home => "https://github.com";

        public string Guid => "efjlwkgf";

        public bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IWidget> Widgets => new List<IWidget>()
        {
            new DefaultStyle.Widgets.ResMonitor(),
            new DefaultStyle.Widgets.Clock(),
            new DefaultStyle.Widgets.Weather(),
            new DefaultStyle.Widgets.BiliHelper(),
            new DefaultStyle.Widgets.GenshinHelper(),
            new DefaultStyle.Widgets.AISchedule(),
        };

        public int Execute()
        {
            throw new NotImplementedException();
        }
    }
}
