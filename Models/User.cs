using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Models
{
    public abstract class User
    {
        string UserId, UserPassword, Name, PhoneNumber;
        UserRoles Role;

        public User(string userId, string userPassword, string name, string phoneNumber, UserRoles Role)
        {
            UserId = userId;
            UserPassword = userPassword;
            Name = name;
            PhoneNumber = phoneNumber;
            this.Role = Role;
        }

        public string UserId1 { get => UserId; }
        public string UserPassword1 { get => UserPassword;  }
        public string Name1 { get => Name; }
        public string PhoneNumber1 { get => PhoneNumber; }
    }
}
