using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users_Klimov.Classes;
using Users_Klimov.Context;
using Users_Klimov.Models;

namespace Users_Klimov.ViewModels
{
    public class VM_Users : Notification
    {
        public AllContext context = new AllContext();
        public ObservableCollection<Users> Users { get; set; }
        public VM_Users()
        {
            var usersList = context.Users.Include(u => u.Role).ToList();
            Users = new ObservableCollection<Users>(usersList);

            var allRoles = context.Roles.ToList();
            foreach (var user in Users)
            {
                user.AllRoles = allRoles;
                user.IsEnable = false;
            }
        }

        public RealyCommand OnAddUser
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    var allRoles = context.Roles.ToList();

                    Users newUser = new Users();

                    newUser.Firstname = "Новый";
                    newUser.Surname = "Пользователь";
                    newUser.Email = "new@mail.ru";
                    newUser.AllRoles = allRoles;
                    newUser.RoleId = allRoles.First().Id;
                    newUser.IsEnable = true;

                    Users.Add(newUser);
                    context.Users.Add(newUser);
                    context.SaveChanges();
                });
            }
        }

        public void RefreshData()
        {
            var freshUsers = context.Users.Include(u => u.Role).ToList();
            var allRoles = context.Roles.ToList();

            foreach (var user in freshUsers)
            {
                user.AllRoles = allRoles;
                user.IsEnable = false;
            }

            Users = new ObservableCollection<Users>(freshUsers);
            OnPropertyChanged("Users");
        }

    }
}
