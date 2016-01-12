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

        private IList<Action> undoActions = new List<Action>();
        private IList<Action> redoActions = new List<Action>();

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
                    {
                        var oldValue = dataModel.Id;

                        undoActions.Add(new Action(() =>
                        {
                            dataModel.Id = oldValue;
                            NotifyPropertyChanged("Id");
                            redoActions.Add(new Action(() =>
                            {
                                dataModel.Id = value;
                                NotifyPropertyChanged("Id");
                            }));
                        }));
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
                    {
                        var oldValue = dataModel.SomeDouble;
                        undoActions.Add(new Action(() => 
                        {
                            dataModel.SomeDouble = oldValue;
                            NotifyPropertyChanged("SomeDouble");
                            redoActions.Add(new Action(() =>
                            {
                                dataModel.SomeDouble = value;
                                NotifyPropertyChanged("SomeDouble");
                            }));
                        }));
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
                if (dataModel.Message != value)
                {
                    {
                        var oldValue = dataModel.Message;
                        undoActions.Add(new Action(() =>
                        {
                            dataModel.Message = oldValue;
                            NotifyPropertyChanged("Message");
                            redoActions.Add(new Action(() =>
                            {
                                dataModel.Message = value;
                                NotifyPropertyChanged("Message");
                            }));
                        }));
                    }
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
                Dispatcher.CurrentDispatcher.BeginInvoke(action);
                undoActions.Remove(action);
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
                Dispatcher.CurrentDispatcher.BeginInvoke(action);
                redoActions.Remove(action);
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
            var vm = new TestItemVM(dm, undoActions);

            obsTestList.Add(vm);
            undoActions.Add(new Action(() => { obsTestList.Remove(vm); }));
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
            var vm = new TestItemVM(SelectedTestItem, undoActions);

            obsTestList.Remove(SelectedTestItem);
            undoActions.Add(new Action(() => { obsTestList.Add(vm); }));
        }
    }
}
