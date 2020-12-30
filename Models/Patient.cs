using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Models
{
    public class Patient
    {
        int patientId, age;
        Gender gender;
        string patientName, phoneNumber, address;
        List<string> History;
        public Patient(int patientId, int age, Gender gender, string patientName, string phoneNumber, string address)
        {
            this.patientId = patientId;
            this.age = age;
            this.gender = gender;
            this.patientName = patientName;
            this.phoneNumber = phoneNumber;
            this.address = address;
            this.History = new List<string>();
        }

        public static Patient LoadPatient(uint PatientId)
        {
            Patient patient = null;
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("patient").Where("PatientId", PatientId))
            using (var reader = new MySqlReader(cmd))
            {
                if (reader.Read())
                {
                    patient = new Patient(reader.ReadInt32("PatientId"),
                        reader.ReadInt32("Age"),
                        (Gender)reader.ReadByte("Gender"),
                        reader.ReadString("PatientName"),
                        reader.ReadString("PhoneNumber"),
                        reader.ReadString("Address"));

                }
            }
            if (patient != null)
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("history").Where("PatientId", PatientId))
                using (var reader = new MySqlReader(cmd))
                {
                    while (reader.Read())
                        patient.insertNewHistory($"[{DateTime.FromBinary(reader.ReadInt64("Date"))}] ==> {reader.ReadString("History")}");
                }
            }
            return patient;
        }
        public bool registerPatient()
        {
            try
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.INSERT))
                {
                    cmd.Insert("patient")
                        .Insert("PatientName", PatientName)
                        .Insert("PhoneNumber", PhoneNumber)
                        .Insert("Address", Address)
                        .Insert("Age", Age)
                        .Insert("Gender", (int)Gender)
                        .Execute();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool insertNewHistory(string NewHistory)
        {
            try
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.INSERT))
                {
                    cmd.Insert("history")
                        .Insert("PatientId", PatientName)
                        .Insert("History", NewHistory)
                        .Insert("Date", DateTime.Now.ToBinary())
                        .Execute();
                }
                History.Add(NewHistory);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public int PatientId { get => patientId; set => patientId = value; }
        public int Age { get => age; set => age = value; }
        public Gender Gender { get => gender; set => gender = value; }
        public string PatientName { get => patientName; set => patientName = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Address { get => address; set => address = value; }
    }
}
