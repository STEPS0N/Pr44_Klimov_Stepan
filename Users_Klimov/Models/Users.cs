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

        private string role;
        public string Role
        {
            get { return role; }
            set
            {
                role = value;
                OnPropertyChanged("Surname");
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
        public RealyCommand OnEdit
        {
            get
            {
                return new RealyCommand(obj =>
                {
                    IsEnable = !IsEnable;

                    if (!IsEnable)
                    {
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_users.context.Users.SaveChanges();
                    }
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
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_users.context.Users.SaveChanges();
                    }
                });
            }
        }
    }
}
