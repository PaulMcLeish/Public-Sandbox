namespace WPFPoorMansMVVMApp.Features.View1
{
    using System;
    using WPFPoorMansMVVMApp.Common;
    using WPFPoorMansMVVMApp.Helpers;

    public class View1VM : BaseVM
    {
        public View1VM()
        {
        }

        private View1DM dataModel = new View1DM();
        public View1DM DataModel
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
                    int i = this.GetHashCode();
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
