using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagmentSystem.Models
{
    public enum ReservationType
    {
        Confirmed = 0,
        Canceled = 1
    }

    public class Reservation
    {
        public const uint ReservationPrice = 100;
        int patientId, reservationId;
        string doctorId, clinicId, accountantId;
        DateTime reservationDate;
        uint TotalPaid = 0;
        ReservationType Status = ReservationType.Confirmed;

        public Reservation(int patientId, int reservationId, string doctorId, string clinicId, string accountantId, DateTime reservationDate)
        {
            this.patientId = patientId;
            this.reservationId = reservationId;
            this.doctorId = doctorId;
            this.clinicId = clinicId;
            this.accountantId = accountantId;
            this.reservationDate = reservationDate;
        }
        public static Reservation LoadReservation(uint ReservationId)
        {
            Reservation reservation = null;
            using (var cmd = new MySqlCommand(MySqlCommandType.SELECT).Select("reservation").Where("ReservationId", ReservationId))
            using (var reader = new MySqlReader(cmd))
            {
                if (reader.Read())
                {
                    reservation = new Reservation(reader.ReadInt32("PatientId"),
                        reader.ReadInt32("ReservationId"),
                        reader.ReadString("DoctorId"),
                        reader.ReadString("ClinicId"),
                        reader.ReadString("AccountantId"),
                        DateTime.FromBinary(reader.ReadInt64("ReservationDate"))
                        );
                }
            }
            return reservation;
        }
        public bool createReservation(uint TotalPaid = 0)
        {
            try
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.INSERT))
                {
                    cmd.Insert("reservation")
                        .Insert("PatientId", patientId)
                        .Insert("DoctorId", doctorId)
                        .Insert("ClinicId", clinicId)
                        .Insert("AccountantId", accountantId)
                        .Insert("ReservationType", (byte)Status)
                        .Insert("ReservationDate", reservationDate.ToBinary())
                        .Insert("TotalPaid", TotalPaid)
                        .Insert("LeftToPay", ReservationPrice - TotalPaid)
                        .Execute();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool postponeReservation(DateTime newDate)
        {
            try
            {
                // TODO: add checks for timeclash.
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                {
                    cmd.Update("reservation")
                        .Set("ReservationDate", newDate.ToBinary())
                        .Where("ReservationId", reservationId)
                        .Execute();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool payAmount(uint Paid)
        {
            try
            {
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                {
                    cmd.Update("reservation")
                        .Set("TotalPaid", TotalPaid + Paid)
                        .Set("LeftToPay", ReservationPrice - (TotalPaid + Paid))
                        .Where("ReservationId", reservationId)
                        .Execute();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool refundAmount(out string Message)
        {
            if (Status == ReservationType.Confirmed)
            {
                Status = ReservationType.Canceled;
                using (var cmd = new MySqlCommand(MySqlCommandType.UPDATE))
                {
                    cmd.Update("reservation")
                        .Set("TotalPaid", 0)
                        .Set("LeftToPay", ReservationPrice)
                        .Set("ReservationType", Status)
                        .Where("ReservationId", reservationId)
                        .Execute();
                }
                Message = $"Total refund amount: {TotalPaid}";
                return true;
            }
            else
            {
                Message = "This reservation has already been refunded before.";
                return false;
            }
        }
    }
}
