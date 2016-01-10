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

namespace WPFPoorMansMVVMApp.Features.View3
{
    /// <summary>
    /// Interaction logic for View3.xaml
    /// </summary>
    public partial class View3 : UserControl
    {
        /// <summary>
        /// viewModel for the current view.
        /// </summary>
        private View3VM viewModel;

        public View3()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup ViewModel here, instead of specifying it directly in the XAML.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.viewModel = new View3VM();
            this.DataContext = this.viewModel;
        }
    }
}
