using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Klimov.Classes;

namespace Users_Klimov.ViewModels
{
    public class VM_Pages : Notification
    {
        public VM_Users vm_users = new VM_Users();
        public VM_Roles vm_roles = new VM_Roles();

        public VM_Pages()
        {
            MainWindow.init.frame.Navigate(new View.MainUsers(vm_users));
            MainWindow.init.frame.Navigate(new View.MainRoles(vm_roles));
        }
    }
}
