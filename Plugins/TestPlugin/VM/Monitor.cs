using CommunityToolkit.Mvvm.ComponentModel;

namespace DefaultStyle.VM
{
    internal class Monitor : ObservableObject
    {

        /// <summary>
        /// CPU值
        /// </summary>
        //public double Value { get; set; }
        private double _loadavr;

        public double CPULoadAvr
        {
            get { return _loadavr; }
            set { SetProperty(ref _loadavr, value); }
        }

        /// <summary>
        /// CPU值
        /// </summary>
        //public double Value { get; set; }
        private double _memloadavr;

        public double MEMLoadAvr
        {
            get { return _memloadavr; }
            set { SetProperty(ref _memloadavr, value); }
        }


        /// <summary>
        /// 核心温度
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// 当前最大温度
        /// </summary>
        public double MaxTemperature { get; set; }

    }
}
