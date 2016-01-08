namespace WPFCaliburnApp.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Caliburn.Micro;
    using System.Windows;
    using WPFCaliburnApp.Features.F1;

    public class MainViewModel : PropertyChangedBase
    {
        public MainViewModel(IEventAggregator eventAggregator, MainDataModel dataModel, F1ViewModel f1View)
        {
            this.eventAggregator = eventAggregator;
            this.dataModel = dataModel;
            this.dataModel.Message = "";
            this.f1View = f1View;
        }

        private IEventAggregator eventAggregator;
        private MainDataModel dataModel;
        private F1ViewModel f1View;

        public F1ViewModel F1View
        {
            get { return f1View; }
        }

        public string Message
        {
            get { return dataModel.Message; }
            set
            {
                if (!string.Equals(dataModel.Message, value.Trim(), StringComparison.CurrentCulture))
                {
                    dataModel.Message = value;
                    NotifyOfPropertyChange(() => Message);
                    NotifyOfPropertyChange(() => CanSayHello);
                }
            }
        }

        public bool CanSayHello
        {
            get { return !string.IsNullOrWhiteSpace(Message); }
        }

        public void SayHello()
        {
            MessageBox.Show(string.Format("Hello {0}!", Message)); //Don't do this in real life :)
        }
    }
}
