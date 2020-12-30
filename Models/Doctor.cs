using System;

namespace HospitalManagmentSystem.Models
{
    public class Doctor : User
    {
        Clinic myClinic = null;
        public Doctor(string userId, string userPassword, string name, string phoneNumber) 
            : base(userId, userPassword, name, phoneNumber, UserRoles.Doctor)
        {
        }

        public Doctor getUser()
        {
            return this;
        }

        internal void AssignToClinic(Clinic clinic)
        {
            this.myClinic = clinic;

            using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
            {
                cmd.Update("users")
                    .Set("ClinicId", myClinic.ClinicName1)
                    .Where("Username", UserId1);
                cmd.Execute();
            }
        }
    }
}