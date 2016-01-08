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

namespace WPFPoorMansMVVMApp.Features.View1
{
    /// <summary>
    /// Interaction logic for View1.xaml
    /// </summary>
    public partial class View1 : UserControl
    {
        public static readonly DependencyProperty View1VMProperty =
            DependencyProperty.Register("View1VM", typeof(View1VM), typeof(View1), new PropertyMetadata(null));

        public View1VM View1VM
        {
            get { return (View1VM)GetValue(View1VMProperty); }
            set
            {
                int i = value.GetHashCode();
                this.DataContext = value;
                SetValue(View1VMProperty, value);
            }
        }

        public View1()
        {
            InitializeComponent();
        }
    }
}
