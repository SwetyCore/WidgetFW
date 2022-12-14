

using CommunityToolkit.Mvvm.ComponentModel;

namespace DefaultStyle.VM
{
    internal class Clock : ObservableObject
    {

        private double _sec;

        public double Sec
        {
            get { return _sec; }
            set { SetProperty(ref _sec, value); }
        }
        private double _min;

        public double Min
        {
            get { return _min; }
            set { SetProperty(ref _min, value); }
        }
        private double _hou;

        public double Hour
        {
            get { return _hou; }
            set { SetProperty(ref _hou, value); }
        }


    }
}
