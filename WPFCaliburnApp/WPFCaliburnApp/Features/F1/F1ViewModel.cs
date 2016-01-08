using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCaliburnApp.Features.F1
{
    public class F1ViewModel : PropertyChangedBase
    {
        private F1DataModel dataModel;
        private IEventAggregator eventAggregator;

        public F1ViewModel(IEventAggregator eventAggregator, F1DataModel dataModel)
        {
            this.dataModel = dataModel;
            this.eventAggregator = eventAggregator;
        }

        public int Id
        {
            get { return dataModel.Id; }
            set
            {
                if (dataModel.Id != value)
                {
                    dataModel.Id = value;
                    NotifyOfPropertyChange(() => Id);
                }
            }
        }

        public string Description
        {
            get { return dataModel.Description; }
            set
            {
                if (!string.Equals(dataModel.Description, value.Trim(), StringComparison.CurrentCulture))
                {
                    dataModel.Description = value;
                    NotifyOfPropertyChange(() => Description);
                }
            }
        }

        public bool Hidden
        {
            get { return dataModel.Hidden; }
            set
            {
                if (dataModel.Hidden != value)
                {
                    dataModel.Hidden = value;
                    NotifyOfPropertyChange(() => Hidden);
                }
            }
        }
    }
}
