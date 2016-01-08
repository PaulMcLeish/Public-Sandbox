using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFPoorMansMVVMApp.Common;
using WPFPoorMansMVVMApp.Features.View1;
using WPFPoorMansMVVMApp.Features.View2;
using WPFPoorMansMVVMApp.Helpers;

namespace WPFPoorMansMVVMApp.Main
{
    public class MainWindowVM : BaseVM
    {
        private MainWindowDM dataModel = new MainWindowDM();
        public MainWindowDM DataModel
        {
            get { return dataModel; }
        }

        private View1VM view1 = new View1VM();
        public View1VM View1
        {
            get { return view1; }
        }

        private View2VM view2 = new View2VM();
        public View2VM View2
        {
            get { return view2; }
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
