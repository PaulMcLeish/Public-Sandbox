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
using WPFPoorMansMVVMApp.Features.View1;
using WPFPoorMansMVVMApp.Features.View2;
using WPFPoorMansMVVMApp.Main;

namespace WPFPoorMansMVVMApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = this.DataContext as MainWindowVM;

            this.view1.View1VM = vm.View1;
            this.view2.View2VM = vm.View2;
        }
    }
}
