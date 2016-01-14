using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WPFPoorMansMVVMApp.Common;
using WPFPoorMansMVVMApp.Helpers;

namespace WPFPoorMansMVVMApp.Features.View4
{
    public class View4VM : BaseVM
    {
        public View4VM()
        {
            addCommand = new RelayCommand(AddExecute, param => AddCanExecute);
            deleteCommand = new RelayCommand(DeleteExecute, param => DeleteCanExecute);
            undoCommand = new RelayCommand(UndoExecute, param => UndoCanExecute);
            redoCommand = new RelayCommand(RedoExecute, param => RedoCanExecute);
        }

        private View4DM dataModel = new View4DM();
        public View4DM DataModel
        {
            get { return dataModel; }
        }

        private IList<IList<Action>> undoActions = new List<IList<Action>>(); // Each Entry will contain 2 actions [0]: Undo, [1]: Redo
        private IList<IList<Action>> redoActions = new List<IList<Action>>(); // Each Entry will contain 2 actions [0]: Undo, [1]: Redo

        private ObservableCollection<TestItemVM> obsTestList = new ObservableCollection<TestItemVM>();
        public ObservableCollection<TestItemVM> TestList
        {
            get { return obsTestList; }
        }

        private TestItemVM selectedTestItem;
        public TestItemVM SelectedTestItem
        {
            get { return selectedTestItem; }
            set
            {
                selectedTestItem = value;
                NotifyPropertyChanged("SelectedTestItem");
            }
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
                        dataModel.Id = oldValue;
                        NotifyPropertyChanged("Id");

                    });

                    var redo = new Action(() =>
                    {
                        dataModel.Id = value;
                        NotifyPropertyChanged("Id");
                    });

                    undoActions.Add(new List<Action>() { undo, redo });
                    #endregion undo/redo

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
                    #region undo/redo
                    var oldValue = dataModel.SomeDouble;

                    var undo = new Action(() =>
                    {
                        dataModel.SomeDouble = oldValue;
                        NotifyPropertyChanged("SomeDouble");
                    });

                    var redo = new Action(() =>
                    {
                        dataModel.SomeDouble = value;
                        NotifyPropertyChanged("SomeDouble");
                    });

                    undoActions.Add(new List<Action>() { undo, redo });
                    #endregion undo/redo

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
                    #region undo/redo
                    var oldValue = dataModel.Message;

                    var undo = new Action(() =>
                    {
                        dataModel.Message = oldValue;
                        NotifyPropertyChanged("Message");
                    });

                    var redo = new Action(() =>
                    {
                        dataModel.Message = value;
                        NotifyPropertyChanged("Message");
                    });

                    undoActions.Add(new List<Action>() { undo, redo });
                    #endregion undo/redo

                    dataModel.Message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }

        private ICommand undoCommand;
        public ICommand UndoCommand
        {
            get { return undoCommand; }
            set { undoCommand = value; }
        }

        public bool UndoCanExecute
        {
            get { return undoActions.Any(); }
        }

        public void UndoExecute()
        {
            var action = undoActions.LastOrDefault();
            if (action != null)
            {
                var undo = action.First();
                var redo = action.Last();
                Dispatcher.CurrentDispatcher.BeginInvoke(undo);
                undoActions.Remove(action);
                redoActions.Add(new List<Action>() { undo, redo });
            }
        }

        private ICommand redoCommand;
        public ICommand RedoCommand
        {
            get { return redoCommand; }
            set { redoCommand = value; }
        }

        public void RedoExecute()
        {
            var action = redoActions.LastOrDefault();
            if (action != null)
            {
                var undo = action.First();
                var redo = action.Last();
                Dispatcher.CurrentDispatcher.BeginInvoke(redo);
                redoActions.Remove(action);
                undoActions.Add(new List<Action>() { undo, redo });
            }
        }

        public bool RedoCanExecute
        {
            get { return redoActions.Any(); }
        }

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get { return addCommand; }
            set { addCommand = value; }
        }

        public bool AddCanExecute
        {
            get { return true; }
        }

        public void AddExecute()
        {
            var dm = new TestItemDM() { Id = Id, Message = Message, SomeDouble = SomeDouble };
            var vm = new TestItemVM(dm);
            var hashCode = vm.GetHashCode();

            #region undo/redo
            var oldValue = dataModel.Message;

            var undo = new Action(() =>
            {
                var deleteMe = obsTestList.FirstOrDefault(x => x.GetHashCode() == hashCode);
                obsTestList.Remove(deleteMe);
            });

            var redo = new Action(() =>
            {
                obsTestList.Add(vm);
            });

            undoActions.Add(new List<Action>() { undo, redo });
            #endregion undo/redo

            obsTestList.Add(vm);
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set { deleteCommand = value; }
        }

        public bool DeleteCanExecute
        {
            get { return SelectedTestItem != null; }
        }

        public void DeleteExecute()
        {
            var dm = new TestItemDM() { Id = this.SelectedTestItem.Id, SomeDouble = this.SelectedTestItem.SomeDouble, Message = this.SelectedTestItem.Message };
            var vm = new TestItemVM(dm);
            var hashCode = vm.GetHashCode();

            #region undo/redo
            var oldValue = dataModel.Message;

            var undo = new Action(() =>
            {
                obsTestList.Add(vm);
            });

            var redo = new Action(() =>
            {
                var deleteMe = obsTestList.FirstOrDefault(x => x.GetHashCode() == hashCode);
                obsTestList.Remove(deleteMe);
            });

            undoActions.Add(new List<Action>() { undo, redo });
            #endregion undo/redo

            obsTestList.Remove(SelectedTestItem);
        }
    }
}
