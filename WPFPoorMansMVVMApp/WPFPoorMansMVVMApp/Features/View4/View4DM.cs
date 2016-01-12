using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPoorMansMVVMApp.Features.View4
{
    public class View4DM
    {
        private int id = 2222222;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private double someDouble = 3.1415926535 * 2;
        public double SomeDouble
        {
            get { return someDouble; }
            set { someDouble = value; }
        }

        private string message = "<Default Message 2>";
        public string Message
        {
            get { return message; }
            set { message = (string.IsNullOrWhiteSpace(value)) ? ""  : value.Trim(); }
        }
    }
}
