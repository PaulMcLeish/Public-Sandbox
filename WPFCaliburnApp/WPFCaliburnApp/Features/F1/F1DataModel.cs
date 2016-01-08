using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCaliburnApp.Features.F1
{
    public class F1DataModel
    {
        public int Id { get; set; }

        private string description = "";
        public string Description
        {
            get { return description.Trim(); }
            set { description = (string.IsNullOrWhiteSpace(value)) ? "" : value.Trim(); }
        }

        public bool Hidden { get; set; }
    }
}
