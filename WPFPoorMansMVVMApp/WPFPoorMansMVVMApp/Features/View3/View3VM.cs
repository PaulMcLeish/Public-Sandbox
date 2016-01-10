using System;
using WPFPoorMansMVVMApp.Common;
using WPFPoorMansMVVMApp.Helpers;

namespace WPFPoorMansMVVMApp.Features.View3
{
    public class View3VM : BaseVM
    {
        #region Fields

        /// <summary>
        /// View 3 DataModel.
        /// </summary>
        private View3DM dataModel = new View3DM();

        #endregion

        #region Properties

        public View3DM DataModel
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
                    int i = GetHashCode();
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

        #endregion
    }
}
