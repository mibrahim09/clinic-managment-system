using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Models
{
    public class Accountant : User
    {
        public Accountant(string userId, string userPassword, string name, string phoneNumber)
            : base(userId, userPassword, name, phoneNumber, UserRoles.Accountant)
        {
        }
    }
}
