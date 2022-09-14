using LibreHardwareMonitor.Hardware;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WidgetBase;

namespace TestPlugin.Widgets
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class ResMonitor : Page, IWidget
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

        public ResMonitor()
        {
            InitializeComponent();

        }
        #region 接口实现
        public string WName => "性能监视";

        public string UID => "monitor";

        public string Description => "CPU，RAM占用查看";



        public Page Widget => this;

        public object Config { get; set; }

        //30*4-10
        public Point Size => new(200, 100);

        public Window WWindow { get; set; } = null;

        #endregion

        public Action OnEnabled => async () =>
        {
            DataContext = vm;
            await Task.Run(() =>
            {
                mycom.Open();
                mycom.Accept(new UpDataIVisitor());
            });
            timer.Start();

            timer.Tick += (object? sender, EventArgs e) =>
            {
                DataUpdate();
            };
        };

        public Action OnDisabled => () =>
        {
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
                if (item.HardwareType == HardwareType.Memory)
                {
                    item.Update();
                    foreach (var item2 in item.Sensors)
                    {
                        if (item2.SensorType == SensorType.Load)
                        {
                            if (item2.Name == "Memory")
                            {
                                vm.MEMLoadAvr = Math.Round((double)item2.Value!);
                            }
                        }
                    }

                }
            }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var process1 = new Process();
            process1.StartInfo.FileName = @"C:\WINDOWS\system32\taskmgr.exe";
            process1.StartInfo.UseShellExecute = true;
            process1.Start();
        }
    }
}
