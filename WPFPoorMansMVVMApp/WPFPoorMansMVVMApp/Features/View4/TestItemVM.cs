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
        private TestItemDM dataModel;
        IList<Action> undoActions;

        private TestItemVM() { }
        public TestItemVM(TestItemDM dataModel, IList<Action> undoActions = null)
        {
            this.dataModel = dataModel;
            this.undoActions = undoActions;
        }

        public TestItemVM(TestItemVM vm, IList<Action> undoActions = null)
        {
            this.dataModel = new TestItemDM() { Id = vm.Id, Message = vm.Message, SomeDouble = vm.SomeDouble };
            this.undoActions = undoActions;
        }

        public int Id
        {
            get { return dataModel.Id; }
            set
            {
                if (dataModel.Id != value)
                {
                    if (undoActions != null)
                    {
                        var oldValue = dataModel.Id;
                        undoActions.Add(new Action(() => { dataModel.Id = oldValue; }));
                    }
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
                    if (undoActions != null)
                    {
                        var oldValue = dataModel.SomeDouble;
                        undoActions.Add(new Action(() => { dataModel.SomeDouble = oldValue; }));
                    }
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
                    if (undoActions != null)
                    {
                        var oldValue = dataModel.Message;
                        undoActions.Add(new Action(() => { dataModel.Message = oldValue; }));
                    }
                    dataModel.Message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }
    }
}
