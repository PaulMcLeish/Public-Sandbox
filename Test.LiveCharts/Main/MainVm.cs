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
        private LineSeries minSeries;
        private LineSeries maxSeries;

        private double maxStrain = 1100;
        private double minStrain = 700;


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
                Series.Remove(maxSeries);
                maxSeries = new LineSeries
                {
                    Title = "Max",
                    Values = new ChartValues<double> { 0, 20000 },
                    Fill = Brushes.Transparent,
                    Configuration = new SeriesConfiguration<double>().X(val => val).Y(val => this.MaxStrain),
                    Stroke = Brushes.Red
                };
                Series.Add(maxSeries);
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
                Series.Remove(minSeries);
                minSeries = new LineSeries
                {
                    Title = "Min",
                    Values = new ChartValues<double> { 0, 20000 },
                    Fill = Brushes.Transparent,
                    Configuration = new SeriesConfiguration<double>().X(val => val).Y(val => this.MinStrain),
                    Stroke = Brushes.Red
                };
                Series.Add(minSeries);
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

            // we create a new SeriesCollection
            //create some LineSeries
            maxSeries = new LineSeries
            {
                Title = "Max",
                Values = new ChartValues<double> { 0, 20000 },
                Fill = Brushes.Transparent,
                Configuration = new SeriesConfiguration<double>().X( val => val).Y(val => this.MaxStrain),
                Stroke = Brushes.Red
            };

            minSeries = new LineSeries
            {
                Title = "Min",
                Values = new ChartValues<double> { 0, 20000 },
                Fill = Brushes.Transparent,
                Configuration = new SeriesConfiguration<double>().X(val => val).Y(val => this.MinStrain),
                Stroke = Brushes.Red
            };

            //add series to SeriesCollection
            Series.Add(maxSeries);
            Series.Add(minSeries);
        }
    }
}
