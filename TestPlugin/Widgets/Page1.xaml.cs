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
using LibreHardwareMonitor.Hardware;
using System.Windows.Threading;
using LiveCharts.Wpf;

namespace TestPlugin.Widgets
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page, IWidget
    {
        VM.Monitor vm = new VM.Monitor();
        DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 1) };
        Computer mycom = new Computer()
        {
            IsCpuEnabled = true,
            IsGpuEnabled = true,
            IsMemoryEnabled = true,
        };
        public class UpDataIVisitor : IVisitor
        {
            public void VisitComputer(IComputer computer)
            {
                computer.Traverse(this);
            }
            public void VisitHardware(IHardware hardware)
            {
                hardware.Update();
                foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
            }
            public void VisitSensor(ISensor sensor) { }
            public void VisitParameter(IParameter parameter) { }
        }

        public Page1()
        {
            new Gauge();
            InitializeComponent();

        }
        #region 接口实现
        public string WName => "性能监视";

        public string UID => "dwyfguiop2134e";

        public string Description => "CPU，RAM占用查看";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new( 230,110);

        public Window WWindow { get; set; } = null;

        #endregion

        public Action OnEnabled => async () =>
        {

            DataContext = vm;
            timer.Start();
            mycom.Open();
            mycom.Accept(new UpDataIVisitor());

            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };
        };

        public Action OnDisabled => () =>
        {
            this.WWindow = null;
            mycom.Close();
            timer.Stop();

        };

        private void DataUpdate()
        {

            foreach (var item in mycom.Hardware)
            {
                if (item.HardwareType == HardwareType.Cpu)
                {
                    item.Update();
                    foreach (var item2 in item.Sensors)
                    {
                        if (item2.SensorType == SensorType.Load)
                        {
                            //CPU总计
                            if (item2.Name == "CPU Total")
                            {
                                vm.CPULoadAvr = Math.Round((double)item2.Value!);
                            }
                        }
                        if (item2.SensorType == SensorType.Temperature)
                        {
                            if (item2.Name == "Core Average")
                            {
                                vm.Temperature = Math.Round((double)item2.Value!, 2);
                            }
                        }
                    }

                }
                if (item.HardwareType==HardwareType.Memory)
                {
                    item.Update();
                    foreach (var item2 in item.Sensors)
                    {
                        if (item2.SensorType==SensorType.Load)
                        {
                            if (item2.Name== "Memory")
                            {
                                vm.MEMLoadAvr = Math.Round((double)item2.Value!);
                            }
                        }
                    }

                }
            }


        }
    }
}
