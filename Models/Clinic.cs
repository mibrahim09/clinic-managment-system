using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Models
{
    public class Clinic
    {
        public static Dictionary<string, Clinic> ClinicsList = new Dictionary<string, Clinic>();

        string ClinicName;
        Dictionary<string, Doctor> DoctorsAssigned;
        int StartHour, FinishHour;

        public string ClinicName1 { get => ClinicName; }
        public Dictionary<string, Doctor> DoctorsAssigned1 { get => DoctorsAssigned; }
        public int StartHour1 { get => StartHour; }
        public int FinishHour1 { get => FinishHour; }

        public Clinic()
        {
            this.DoctorsAssigned = new Dictionary<string, Doctor>();
        }
        public void editWorkingHours(int startHour, int finishHour)
        {
            this.StartHour = startHour;
            this.FinishHour = finishHour;
        }

        public bool addDoctorToClinic(Doctor doctor)
        {
            if (!DoctorsAssigned.ContainsKey(doctor.UserId1))
            {
                DoctorsAssigned.Add(doctor.UserId1, doctor);
                doctor.AssignToClinic(this);
                return true;
            }
            return false;
        }

        public static bool addNewClinic(Clinic clinic)
        {
            if (!ClinicsList.ContainsKey(clinic.ClinicName))
            {
                ClinicsList.Add(clinic.ClinicName, clinic);
                return true;
            }
            return false;
        }

    }
}
