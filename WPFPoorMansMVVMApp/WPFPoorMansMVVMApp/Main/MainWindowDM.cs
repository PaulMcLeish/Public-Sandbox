using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPoorMansMVVMApp.Main
{
    public class MainWindowDM
    {
        private int id = 9999999;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private double someDouble = 9.999999;
        public double SomeDouble
        {
            get { return someDouble; }
            set { someDouble = value; }
        }

        private string message = "<Default Message Main>";
        public string Message
        {
            get { return message; }
            set { message = (string.IsNullOrWhiteSpace(value)) ? "" : value.Trim(); }
        }
    }
}
