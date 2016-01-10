using System.Windows.Controls;

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
