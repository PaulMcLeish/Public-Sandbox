using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPoorMansMVVMApp.Features.View1
{
    public class View1VMEventArgs : EventArgs
    {
        private View1VM vm;

        public View1VMEventArgs(View1VM vm)
        {
            this.vm = vm;
        }

        private View1VM ViewModel
        {
            get { return vm; }
        }
    }
}
