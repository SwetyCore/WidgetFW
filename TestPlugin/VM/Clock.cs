using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlugin.VM
{
    internal class Clock: ObservableObject
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
