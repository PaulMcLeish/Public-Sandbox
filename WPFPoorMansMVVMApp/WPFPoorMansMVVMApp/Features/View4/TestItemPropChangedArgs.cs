using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPoorMansMVVMApp.Features.View4
{
    public class TestItemPropChangedArgs : EventArgs
    {
        private TestItemVM vm = null;
        private Property changedProperty = Property.Id;
        private List<Action> undoActions = null;

        public enum Property
        {
            Id,
            SomeDouble,
            Message
        };

        public TestItemPropChangedArgs(TestItemVM vm, Property changedProperty, List<Action> undoActions)
        {
            this.vm = vm;
            this.changedProperty = changedProperty;
            this.undoActions = undoActions;
        }

        public TestItemVM ViewModel
        {
            get { return vm; }
        }

        public Property ChangedProperty
        {
            get { return changedProperty; }
        }

        public List<Action> UndoActions
        {
            get { return undoActions; }
        }
    }
}
