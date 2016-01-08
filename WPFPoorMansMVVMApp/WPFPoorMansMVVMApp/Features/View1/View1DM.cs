using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPoorMansMVVMApp.Features.View1
{
    public class View1DM
    {
        private int id = 1111111;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private double someDouble = 3.1415926535;
        public double SomeDouble
        {
            get { return someDouble; }
            set { someDouble = value; }
        }

        private string message = "<Default Message 1>";
        public string Message
        {
            get { return message; }
            set { message = (string.IsNullOrWhiteSpace(value)) ? ""  : value.Trim(); }
        }
    }
}
