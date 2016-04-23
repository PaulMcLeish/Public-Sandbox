using LiveCharts;
using System;
using System.Windows.Media;
using Test.LiveCharts.Common;

namespace Test.LiveCharts.Main
{
    public class MainVm : BaseVM
    {
        private RelayCommand windowClosedCmd;
        private RelayCommand windowLoadedCmd;
        private SeriesCollection series = new SeriesCollection();
        public Func<double, string> DateFormatter { get; set; }
        private LineSeries minSeries;
        private LineSeries maxSeries;

        private double maxStrain = 1100;
        private double minStrain = 700;

        public ObservablePt MinLeft { get; set; }
        public ObservablePt MinRight { get; set; }
        public ObservablePt MaxLeft { get; set; }
        public ObservablePt MaxRight { get; set; }

        public MainVm()
        {
            InitCommands();
        }

        public RelayCommand WindowClosedCmd { get { return windowClosedCmd; } }

        public RelayCommand WindowLoadedCmd { get { return windowLoadedCmd; } }

        public SeriesCollection Series { get { return series; } }


        public double MaxStrain
        {
            get
            {
                return maxStrain;
            }

            set
            {
                maxStrain = value;
                NotifyPropertyChanged(() => MaxStrain);
                this.MaxLeft.Data = maxStrain;
                this.MaxRight.Data = maxStrain;
            }
        }

        public double MinStrain
        {
            get
            {
                return minStrain;
            }

            set
            {
                minStrain = value;
                NotifyPropertyChanged(() => MinStrain);
                this.MinLeft.Data = minStrain;
                this.MinRight.Data = minStrain;
            }
        }

        private void InitCommands()
        {
            windowLoadedCmd = new RelayCommand(OnWindowLoaded);
            windowClosedCmd = new RelayCommand(OnWindowClosed);
        }

        private void OnWindowClosed(object obj)
        {
            // Dispose stuff here
        }

        private void OnWindowLoaded(object obj)
        {
            // Do Data Initialisation here
            var bot = DateTime.Today;
            var eot = bot.AddDays(1);

            MaxLeft = new ObservablePt() { DateTime = bot, Data = this.MaxStrain };
            MaxRight = new ObservablePt() { DateTime = eot, Data = this.MaxStrain };
            MinLeft = new ObservablePt() { DateTime = bot, Data = this.MinStrain };
            MinRight = new ObservablePt() { DateTime = eot, Data = this.MinStrain };

            DateFormatter = val => DateTime.FromOADate(val).ToString("M");

            // we create a new SeriesCollection
            //create some LineSeries
            maxSeries = new LineSeries
            {
                Title = "Max",
                Values = new ChartValues<ObservablePt> { MaxLeft, MaxRight },
                Fill = Brushes.Transparent,
                Configuration = new SeriesConfiguration<ObservablePt>().X( pt => pt.DateTime.ToOADate()).Y(pt => pt.Data),
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 2 }, //make it dashed
            };

            minSeries = new LineSeries
            {
                Title = "Min",
                Values = new ChartValues<ObservablePt> { MinLeft, MinRight },
                Fill = Brushes.Transparent,
                Configuration = new SeriesConfiguration<ObservablePt>().X(pt => pt.DateTime.ToOADate()).Y(pt => pt.Data),
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 2 }, //make it dashed
            };

            //add series to SeriesCollection
            Series.Add(maxSeries);
            Series.Add(minSeries);
        }
    }
}
