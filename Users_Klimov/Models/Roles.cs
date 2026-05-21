using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                role = value;
                OnPropertyChanged("Role");
            }
        }
    }
}
