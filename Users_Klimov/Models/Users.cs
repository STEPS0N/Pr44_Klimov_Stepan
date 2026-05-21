using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Users_Klimov.Classes;

namespace Users_Klimov.Models
{
    public class Users : Notification
    {
        public int Id { get; set; }
        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set
            {
                Match match = Regex.Match(value, "^.{1,100}$");
                if (!match.Success)
                    MessageBox.Show("Имя не должно быть пустым, и не более 100 символов.",
                        "Не корректный ввод значения.");
                else
                {
                    firstname = value;
                    OnPropertyChanged("Firstname");
                }
            }
        }

        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                Match match = Regex.Match(value, "^.{1,100}$");
                if (!match.Success)
                    MessageBox.Show("Имя не должно быть пустым, и не более 100 символов.",
                        "Не корректный ввод значения.");
                else
                {
                    surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                Match match = Regex.Match(value, @"^[\w\.-]+@[\w\.-]+\.\w+$");
                if (!match.Success)
                    MessageBox.Show("Введите корректный email!",
                        "Не корректный ввод значения.");
                else
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private int roleId;
        public int RoleId
        {
            get { return roleId; }
            set
            {
                roleId = value;
                OnPropertyChanged("RoleId");

                if (AllRoles != null)
                {
                    Role = AllRoles.FirstOrDefault(r => r.Id == value);
                    OnPropertyChanged("Role");
                }
            }
        }

        private Roles role;
        public virtual Roles Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged("Role");
            }
        }

        [NotMapped]
        private bool isEnable;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                OnPropertyChanged("IsEnable");
                OnPropertyChanged("IsEnableText");
            }
        }

        [NotMapped]
        public string IsEnableText
        {
            get
            {
                if (IsEnable) return "Сохранить";
                else return "Изменить";
            }
        }

        [NotMapped]
        public List<Roles> AllRoles { get; set; }

        [NotMapped]
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    if (IsEnable)
                    {
                        try
                        {
                            var context = (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_users.context;
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                        }
                    }
                    else
                    {
                        var context = (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_users.context;
                        AllRoles = context.Roles.ToList();
                        OnPropertyChanged("AllRoles");

                        OnPropertyChanged("RoleId");
                        OnPropertyChanged("Role");
                    }

                    IsEnable = !IsEnable;
                });
            }
        }

        [NotMapped]
        public RealyCommand OnDelete
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    if (MessageBox.Show("Вы уверены что хотите удалить пользователя?",
                        "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_users.Users.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_users.context.Users.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_users.context.SaveChanges();
                    }
                });
            }
        }
    }
}
