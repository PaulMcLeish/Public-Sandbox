using System;
using LiveCharts;

namespace Test.LiveCharts.LiveChartsPattern
{
    public class LagTest :IObservableChartPoint
    {
        private double _value;
        public DateTime DateTime { get; set; }

        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (PointChanged != null) PointChanged.Invoke(this);
            }
        }

        public event Action<object> PointChanged;
    }
}
