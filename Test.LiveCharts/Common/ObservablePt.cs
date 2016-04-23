using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.LiveCharts.Common
{
    public class ObservablePt : IObservableChartPoint
    {
        private double data;
        private DateTime dateTime;

        public DateTime DateTime
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                if (PointChanged != null) PointChanged.Invoke(this);
            }
        }

        public double Data
        {
            get { return data; }
            set
            {
                data = value;
                if (PointChanged != null) PointChanged.Invoke(this);
            }
        }

        public event Action<object> PointChanged;
    }
}
