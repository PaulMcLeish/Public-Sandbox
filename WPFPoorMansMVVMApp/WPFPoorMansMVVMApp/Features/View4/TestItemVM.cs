namespace WPFPoorMansMVVMApp.Features.View4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WPFPoorMansMVVMApp.Common;
    using WPFPoorMansMVVMApp.Helpers;

    public class TestItemVM : BaseVM
    {
        // TODO: Fire Event: ItemChanged on prop change - To be caught by parent for undo/redo
        private TestItemDM dataModel;

        private TestItemVM() { }
        public TestItemVM(TestItemDM dataModel, IList<Action> undoActions = null)
        {
            this.dataModel = dataModel;
        }

        public TestItemVM(TestItemVM vm)
        {
            this.dataModel = new TestItemDM() { Id = vm.Id, Message = vm.Message, SomeDouble = vm.SomeDouble };
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
                if (!string.Equals(dataModel.Message, value, StringComparison.CurrentCulture))
                {
                    dataModel.Message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }
    }
}
