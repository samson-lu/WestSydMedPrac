using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add the following references
using System.Data;
using System.Data.SqlClient;

namespace WestSydMedPrac.Classes
{
    public sealed class Appointments : List<Appointment>
    {
        #region Constructors
        public Appointments(Patient patient)
        {
            try
            {
                SqlDataAccessLayer myDAL = new SqlDataAccessLayer();
                //usp_GetPatientAppointments
                SqlParameter[] parameter = { new SqlParameter("@Patient_ID", patient.Patient_ID) };
                DataTable dtAppoinments = myDAL.ExecuteStoredProc("usp_GetAppointDetailByPatientId", parameter);

                //Iterate through the results adding each appointment to this class's internal list
                foreach (DataRow appointmentRow in dtAppoinments.Rows)
                {
                    //Create a new instance of an Appoinment for each row
                    Appointment appointment = new Appointment(appointmentRow);

                    //Add the appointment to this class's list
                    this.Add(appointment);

                }

            }
            catch (Exception ex)
            {

                throw new Exception("Unable to retrieve Patient's appointments!", ex);
            }
        }

        public Appointments(Practitioner practitioner)
        {
            //TODO - Implement this when you build the Practitioner(s) and Practitioner classes.
            //try
            //{
            //    SqlDataAccessLayer myDAL = new SqlDataAccessLayer();
            //    SqlParameter[] parameter = { new SqlParameter("@Practitioner_ID", practitioner.Practitioner_ID) };

            //    DataTable dtAppoinments = myDAL.ExecuteStoredProc("usp_GetAppointmentsDetailsByPractitioner_ID", parameter);

            //    foreach (DataRow appointmentRow in dtAppoinments.Rows)
            //    {
            //        //Create a new instance of an Appointment for each row
            //        Appointment appointment = new Appointment(appointmentRow);

            //        //Add the appointment to the class's list
            //        this.Add(appointment);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw new Exception("Unable to retrieve Practitioner's appointments!", ex);
            //}


        }
        #endregion Constructors
    }
}
