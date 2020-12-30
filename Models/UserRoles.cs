using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Models
{
    public enum UserRoles : byte
    {
        Unknown = 0,
        SystemAdmin,
        Doctor, 
        Accountant
    }
}
