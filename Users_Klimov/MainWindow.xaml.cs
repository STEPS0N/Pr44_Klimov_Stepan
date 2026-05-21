using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Users_Klimov.ViewModels;

namespace Users_Klimov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;
        public MainWindow()
        {
            InitializeComponent();
            init = this;
            DataContext = new VM_Pages();
        }

        private void ToRoles(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as VM_Pages;
            frame.Navigate(new View.MainRoles(vm.vm_roles));
        }

        private void ToUsers(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as VM_Pages;
            vm.vm_users = new VM_Users();
            frame.Navigate(new View.MainUsers(vm.vm_users));
        }
    }
}