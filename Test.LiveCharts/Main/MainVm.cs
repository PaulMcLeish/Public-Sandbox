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
        private LineSeries periodSeries;

        private double maxStrain = 1100;
        private double minStrain = 700;

        public ObservablePt MinLeft { get; set; }
        public ObservablePt MinRight { get; set; }
        public ObservablePt MaxLeft { get; set; }
        public ObservablePt MaxRight { get; set; }

        public ObservablePt SOSBottom { get; set; }
        public ObservablePt SOSTop { get; set; }

        private double start = 9;
        public double Start
        {
            get { return this.start; }
            set
            {
                this.start = value;
                NotifyPropertyChanged(() => Start);
                SOS = DateTime.Today.AddHours(Start);

            }
        }

        private double period = 3.5;
        public double Period
        {
            get { return this.period; }
            set
            {
                this.period = value;
                NotifyPropertyChanged(() => Period);
                SOSBottom.DateTime = SOS.AddHours(Period);
                SOSTop.DateTime = SOS.AddHours(Period);
            }
        }

        private DateTime sos;
        public DateTime SOS
        {
            get { return sos; }
            set
            {
                sos = value;
                NotifyPropertyChanged(() => SOS);
                SOSBottom.DateTime = SOS.AddHours(Period);
                SOSTop.DateTime = SOS.AddHours(Period);
            }
        }

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
                if (value > MinStrain)
                {
                    maxStrain = value;
                    NotifyPropertyChanged(() => MaxStrain);
                    this.MaxLeft.Data = maxStrain;
                    this.MaxRight.Data = maxStrain;
                }
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
                if (value < MaxStrain)
                {
                    minStrain = value;
                    NotifyPropertyChanged(() => MinStrain);
                    this.MinLeft.Data = minStrain;
                    this.MinRight.Data = minStrain;
                }
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
            SOSBottom = new ObservablePt() { DateTime = SOS.AddHours(Start + Period), Data = 0 };
            SOSTop = new ObservablePt() { DateTime = SOS.AddHours(Start + Period), Data = 3000 };

            Start = 9;
            Period = 2.5;

            MaxLeft = new ObservablePt() { DateTime = bot, Data = this.MaxStrain };
            MaxRight = new ObservablePt() { DateTime = eot, Data = this.MaxStrain };
            MinLeft = new ObservablePt() { DateTime = bot, Data = this.MinStrain };
            MinRight = new ObservablePt() { DateTime = eot, Data = this.MinStrain };

            DateFormatter = val => DateTime.FromOADate(val).ToString("T");

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

            periodSeries = new LineSeries
            {
                Title = "EOS",
                Values = new ChartValues<ObservablePt> { SOSBottom, SOSTop },
                Fill = Brushes.Transparent,
                Configuration = new SeriesConfiguration<ObservablePt>().X(pt => pt.DateTime.ToOADate()).Y(pt => pt.Data),
                Stroke = Brushes.Yellow,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 2 }, //make it dashed
            };

            //add series to SeriesCollection
            Series.Add(maxSeries);
            Series.Add(minSeries);
            Series.Add(periodSeries);
        }
    }
}
