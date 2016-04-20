using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.CoreComponents;

namespace Test.LiveCharts.LiveChartsPattern
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1
    {
        private readonly LineSeries _max;
        private readonly LineSeries _min;
        private readonly IEnumerable<Series> _maxAndMin;
        private readonly Random _r;
        private int _day;

        public Window1()
        {
            InitializeComponent();

            TopLeftLimit = new LagTest();
            TopRightLimit = new LagTest();
            BotLeftLimit = new LagTest();
            BotRightLimit = new LagTest();

            _r = new Random();

            //ok in this case we are plotting the lag of some different servers according to a date
            //we also need to highlight the max and min values in each test.

            //notice that the LagTestClass implements IObservableChartPoint
            //this allows the char to notice when a value changes

            //Lets define a configuration by default for all the series in the SeriesCollection
            var defConfig = new SeriesConfiguration<LagTest>()
                .X(lagTest => lagTest.DateTime.ToOADate())
                .Y(lagTest => lagTest.Value);

            var serverA = new LineSeries
            {
                Title = "Server A",
                Values = new ChartValues<LagTest>
                {
                    new LagTest {DateTime = DateTime.Now.AddDays(0), Value = _r.Next(0, 100)},
                    new LagTest {DateTime = DateTime.Now.AddDays(1), Value = _r.Next(0, 100)},
                    new LagTest {DateTime = DateTime.Now.AddDays(2), Value = _r.Next(0, 100)},
                },
                Fill = Brushes.Transparent
            };
            var serverB = new LineSeries
            {
                Title = "Server B",
                Values = new ChartValues<LagTest>
                {
                    new LagTest {DateTime = DateTime.Now.AddDays(0), Value = _r.Next(0, 100)},
                    new LagTest {DateTime = DateTime.Now.AddDays(1), Value = _r.Next(0, 100)},
                    new LagTest {DateTime = DateTime.Now.AddDays(2), Value = _r.Next(0, 100)},
                },
                Fill = Brushes.Transparent
            };

            //to highlight the max and min values, we will also plot 2 more series
            //every series contains 2 points: max and min

            _min = new LineSeries
            {
                Values = new ChartValues<LagTest> {BotLeftLimit, BotRightLimit},
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection {2}, //make it dashed
                Stroke = Brushes.DarkBlue,
                Fill = Brushes.Transparent
            };
            _max = new LineSeries
            {
                Values = new ChartValues<LagTest> {TopLeftLimit, TopRightLimit},
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection {2}, //make it dashed
                Stroke = Brushes.DarkBlue,
                Fill = Brushes.Transparent
            };

            Servers = new SeriesCollection(defConfig)
            {
                _min,
                _max,
                serverA,
                serverB
            };

            _maxAndMin = new List<Series> {_min, _max}.AsEnumerable();
            CalculateLimits();

            DateFormatter = val => DateTime.FromOADate(val).ToString("M");

            DataContext = this;
        }

        public SeriesCollection Servers { get; set; }
        public Func<double, string> DateFormatter { get; set; }

        public LagTest TopLeftLimit { get; set; }
        public LagTest TopRightLimit { get; set; }
        public LagTest BotLeftLimit { get; set; }
        public LagTest BotRightLimit { get; set; } 

        private void GetMeasureOnClick(object sender, RoutedEventArgs e)
        {
            var day = _day++ + 3;

            foreach (var server in Servers.Except(_maxAndMin))
            {
                server.Values.Add(new LagTest
                {
                    DateTime = DateTime.Now.AddDays(day),
                    Value = _r.Next(0, 100)
                });
            }

            CalculateLimits();
        }

        private void AddServerOnClick(object sender, RoutedEventArgs e)
        {
            Servers.Add(new LineSeries
            {
                Title = "A Random Server!",
                Values = Servers[2].Values.Cast<LagTest>().Select(test => new LagTest
                {
                    DateTime = test.DateTime,
                    Value = _r.Next(0, 100)
                }).AsChartValues(),
                Fill = Brushes.Transparent
            });
            CalculateLimits();
        }

        private void RandomizeAllValues(object sender, RoutedEventArgs e)
        {
            foreach (var server in Servers)
            {
                foreach (var lagTest in server.Values.Cast<LagTest>())
                {
                    lagTest.Value = _r.Next(0, 100);
                }
            }
            CalculateLimits();
        }

        private void CalculateLimits()
        {
            //this will of course not be smart when having multiple values,
            //instead you could try a smarter logic,
            //like cache every max and min value every time your collection changes

            var s = Servers.Except(_maxAndMin)
                .Select(x => x.Values)
                .Cast<IEnumerable<LagTest>>()
                .ToArray();

            var maxValue = s.Select(x => x.Max(y => y.Value)).Max();
            var minValue = s.Select(x => x.Min(y => y.Value)).Min();
            var maxDate = s.Select(x => x.Max(y => y.DateTime)).Max();
            var minDate = s.Select(x => x.Min(y => y.DateTime)).Min();

            TopLeftLimit.DateTime = minDate;
            TopRightLimit.DateTime = maxDate;
            TopLeftLimit.Value = maxValue;
            TopRightLimit.Value = maxValue;

            BotLeftLimit.DateTime = minDate;
            BotRightLimit.DateTime = maxDate;
            BotLeftLimit.Value = minValue;
            BotRightLimit.Value = minValue;
        }
    }
}
