using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPoorMansMVVMApp.Features.View2
{
    /// <summary>
    /// Interaction logic for View1.xaml
    /// </summary>
    public partial class View2 : UserControl
    {
        public static readonly DependencyProperty View2VMProperty =
              DependencyProperty.Register("View2VM", typeof(View2VM), typeof(View2), new PropertyMetadata(null));

        public View2VM View2VM
        {
            get { return (View2VM)GetValue(View2VMProperty); }
            set { SetValue(View2VMProperty, value); }
        }

        public View2()
        {
            InitializeComponent();
        }
    }
}
