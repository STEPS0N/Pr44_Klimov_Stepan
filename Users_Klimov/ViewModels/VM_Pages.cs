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
        public VM_Users vm_users { get; set; }
        public VM_Roles vm_roles { get; set; }

        public VM_Pages()
        {
            vm_users = new VM_Users();
            vm_roles = new VM_Roles();
            MainWindow.init.frame.Navigate(new View.MainUsers(vm_users));
        }
    }
}
