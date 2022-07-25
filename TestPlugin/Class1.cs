using System;
using System.Collections.Generic;
using WidgetBase;

namespace TestPlugin
{
    public class Class1 : IPlugin
    {
        public string Name => "test";

        public string Description => "Desp";

        public Version Version =>new Version("2.1.1");

        public string Author => "23";

        public string Home => "home";

        public string Guid =>"hesdwdwqwe";

        public bool Enabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Config { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IEnumerable<IWidget> Widgets => new List<IWidget>()
        {
            new TestPlugin.Widgets.Page1()
        };

        public int Execute()
        {
            throw new NotImplementedException();
        }
    }
}
