namespace WPFCaliburnApp.Commands
{
    using System;
    using System.Windows.Input;

    public class TestCommand1 : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
