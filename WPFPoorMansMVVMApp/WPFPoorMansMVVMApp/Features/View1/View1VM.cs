namespace WPFPoorMansMVVMApp.Features.View1
{
    using System;
    using System.Windows.Input;
    using WPFPoorMansMVVMApp.Common;
    using WPFPoorMansMVVMApp.Helpers;

    public class View1VM : BaseVM
    {
        public View1VM()
        {
            fireEventCommand = new RelayCommand(FireEventExecute);
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

        // Command to fire event
        private ICommand fireEventCommand;
        public ICommand FireEventCommand
        {
            get { return fireEventCommand; }
            set { fireEventCommand = value; }
        }
        private void FireEventExecute(object msg)
        {
            OnView1VMEvent();
        }

        public event EventHandler<View1VMEventArgs> View1VMEvent;
        private void OnView1VMEvent()
        {
            var handler = View1VMEvent;

            if (handler != null)
            {
                handler(this, new View1VMEventArgs(this));
            }
        }
    }
}
