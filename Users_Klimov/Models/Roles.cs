using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Users_Klimov.Classes;

namespace Users_Klimov.Models
{
    public class Roles : Notification
    {
        public int Id { get; set; }
        private string role;
        public string Role
        {
            get { return role; }
            set
            {
                Match match = Regex.Match(value, "^.{1,50}$");
                if (!match.Success)
                    MessageBox.Show("Название роли не должно быть пустым, и не более 50 символов.",
                        "Не корректный ввод значения.");
                else
                {
                    role = value;
                    OnPropertyChanged("Role");
                }
            }
        }

        public virtual ICollection<Users> Users { get; set; }

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
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_roles.context.SaveChanges();
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
                    if (MessageBox.Show("Вы уверены что хотите удалить роль?",
                        "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_roles.Roles.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_roles.context.Roles.Remove(this);
                        (MainWindow.init.DataContext as ViewModels.VM_Pages).vm_roles.context.SaveChanges();
                    }
                });
            }
        }
    }
}
