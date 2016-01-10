namespace WPFPoorMansMVVMApp.Main
{
    using System;
    using Common;
    using Features.View1;
    using Features.View2;
    using Helpers;
    using System.Windows.Input;
    using System.Windows;

    public class MainWindowVM : BaseVM
    {
        public MainWindowVM()
        {
            btn1Command = new RelayCommand(Btn1Execute, param => Btn1CanExecute);
            btn2Command = new RelayCommand(Btn2Execute, param => Btn2CanExecute);
            view1.View1VMEvent += View1_View1VMEvent;
        }

        private void View1_View1VMEvent(object sender, View1VMEventArgs e)
        {
            MessageBox.Show("MainWndow: View1 Event was fired");
        }

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

        private ICommand btn1Command;
        public ICommand Btn1Command
        {
            get { return btn1Command; }
            set { btn1Command = value; }
        }

        private bool btn1CanExecute = true;
        public bool Btn1CanExecute
        {
            get { return btn1CanExecute; }
            set
            {
                if (btn1CanExecute != value)
                {
                    btn1CanExecute = value;
                    NotifyPropertyChanged("Btn1CanExecute");
                }
            }
        }

        public void Btn1Execute(object msg)
        {
            MessageBox.Show(String.Format("Btn1Execute: '{0}'", msg.ToString()));
        }

        private ICommand btn2Command;
        public ICommand Btn2Command
        {
            get { return btn2Command; }
            set { btn2Command = value; }
        }

        private bool btn2CanExecute = true;
        public bool Btn2CanExecute
        {
            get { return btn2CanExecute; }
            set
            {
                if (btn2CanExecute != value)
                {
                    btn2CanExecute = value;
                    NotifyPropertyChanged("Btn2CanExecute");
                }
            }
        }

        public void Btn2Execute()
        {
            MessageBox.Show(String.Format("Btn2Execute"));
        }
    }
}
