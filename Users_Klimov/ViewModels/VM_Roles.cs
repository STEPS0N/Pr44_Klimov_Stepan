using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Klimov.Classes;
using Users_Klimov.Context;
using Users_Klimov.Models;

namespace Users_Klimov.ViewModels
{
    public class VM_Roles : Notification
    {
        public AllContext context = new AllContext();
        public ObservableCollection<Roles> Roles { get; set; }
        public VM_Roles()
        {
            Roles = new ObservableCollection<Roles>(context.Roles.ToList());
        }

        public RealyCommand OnAddRole
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    Roles newRole = new Roles();

                    newRole.Role = "Новая роль";
                    newRole.IsEnable = true;

                    Roles.Add(newRole);
                    context.Roles.Add(newRole);
                    context.SaveChanges();

                    var vmUsers = (MainWindow.init.DataContext as VM_Pages).vm_users;
                    vmUsers.RefreshData();
                });
            }
        }
    }
}
