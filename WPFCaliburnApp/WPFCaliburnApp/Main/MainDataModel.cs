using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCaliburnApp.Main
{
    public class MainDataModel
    {
        private string message = "";
        public string Message
        {
            get { return message.Trim(); }
            set { message = (string.IsNullOrWhiteSpace(value)) ? "" : value.Trim(); }
        }
    }
}
