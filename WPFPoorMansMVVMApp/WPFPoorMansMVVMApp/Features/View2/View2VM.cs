using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPoorMansMVVMApp.Common;
using WPFPoorMansMVVMApp.Helpers;

namespace WPFPoorMansMVVMApp.Features.View2
{
    public class View2VM : BaseVM
    {
        private View2DM dataModel = new View2DM();
        public View2DM DataModel
        {
            get { return dataModel; }
        }

        public int Id
        {
            get { return dataModel.Id; }
            set
            {
                if (dataModel.Id != value)
                {
                    dataModel.Id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        public double SomeDouble
        {
            get { return dataModel.SomeDouble; }
            set
            {
                if (Math.Abs(dataModel.SomeDouble - value) > Constants.TOLERANCE)
                {
                    dataModel.SomeDouble = value;
                    NotifyPropertyChanged("SomeDouble");
                }
            }
        }

        public string Message
        {
            get { return dataModel.Message; }
            set
            {
                if (dataModel.Message != value)
                {
                    dataModel.Message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }
    }
}
