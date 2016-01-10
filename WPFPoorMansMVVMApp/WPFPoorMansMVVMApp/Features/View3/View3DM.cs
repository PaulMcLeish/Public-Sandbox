namespace WPFPoorMansMVVMApp.Features.View3
{
    public class View3DM
    {
        #region Fields
        private int id = 333333;
        private double someDouble = 3.1415926535;
        private string message = "<Default Message 3>";
        #endregion

        #region Properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public double SomeDouble
        {
            get { return someDouble; }
            set { someDouble = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = (string.IsNullOrWhiteSpace(value)) ? "" : value.Trim(); }
        }
        #endregion
    }
}
