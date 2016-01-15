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
                    #region undo/redo
                    var oldValue = dataModel.Id;

                    var undo = new Action(() =>
                    {
                        this.Id = oldValue;
                    });

                    var redo = new Action(() =>
                    {
                        this.Id = value;
                    });

                    var undoActions = new List<Action>() { undo, redo };
                    #endregion undo/redo

                    dataModel.Id = value;
                    NotifyPropertyChanged("Id");
                    OnTestItemPropChanged(TestItemPropChangedArgs.Property.Id, undoActions);
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
                    #region undo/redo
                    var oldValue = dataModel.SomeDouble;

                    var undo = new Action(() =>
                    {
                        this.SomeDouble = oldValue;
                    });

                    var redo = new Action(() =>
                    {
                        this.SomeDouble = value;
                    });

                    var undoActions = new List<Action>() { undo, redo };
                    #endregion undo/redo

                    dataModel.SomeDouble = value;
                    NotifyPropertyChanged("SomeDouble");
                    OnTestItemPropChanged(TestItemPropChangedArgs.Property.SomeDouble, undoActions);
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
                    #region undo/redo
                    var oldValue = dataModel.Message;

                    var undo = new Action(() =>
                    {
                        this.Message = oldValue;
                    });

                    var redo = new Action(() =>
                    {
                        this.Message = value;
                    });

                    var undoActions = new List<Action>() { undo, redo };
                    #endregion undo/redo

                    dataModel.Message = value;
                    NotifyPropertyChanged("Message");
                    OnTestItemPropChanged(TestItemPropChangedArgs.Property.Message, undoActions);
                }
            }
        }

        public event EventHandler<TestItemPropChangedArgs> TestItemPropChanged;
        private void OnTestItemPropChanged(TestItemPropChangedArgs.Property changedProperty, List<Action> undoActions)
        {
            var handler = TestItemPropChanged;

            if (handler != null)
            {
                handler(this, new TestItemPropChangedArgs(this, changedProperty, undoActions));
            }
        }
    }
}
