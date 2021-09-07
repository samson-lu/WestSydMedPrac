using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add the following reference
using System.Data;
using System.Data.SqlClient;


namespace WestSydMedPrac.Classes
{
    //The 'sealed' access modifier prevents other classes from inheriting this class.
    public sealed class Appointment
    {
        //#regions allow us to organise our code. They're collapsable.
        #region Private Field Variables

        #endregion Private Field Variables

        #region Properties
        //We will implement the properties for this class as 'auto-properties'
        //i.e. we're not going to define (or use) private field variables to hold the 
        //value of the properties. But rather, the use of 'auto-properties' creates a 
        //hidden system variable that is not accessible to us (developer) directly.
        public int Practitioner_ID { get; set; }
        public string PractitionerType { get; set; }
        public string PractitionerFirstName { get; set; }
        public string PractitionerLastName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public int Patient_ID { get; set; }
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Appointment Default Constructor - Instantiates an empty Appointment Object.
        /// </summary>
        public Appointment()
        {
            //Default Constructor - no implementation
        }

        /// <summary>
        /// Overloaded Appointment Constructor - Creates an Appointment object with the supplied date, time, patient, and practitioner.
        /// </summary>
        /// <param name="appointmentDate">DateTime: The date of the desired appointment.</param>
        /// <param name="appointmentTime">TimeSpan: The time of the desired appointment.</param>
        /// <param name="patient_id">int: The ID of the patient for whom the appointment is being made.</param>
        /// <param name="practitioner_id">int: The ID of the practitioner with whom the appointment is being made.</param>
        public Appointment(DateTime appointmentDate, TimeSpan appointmentTime, int patient_id, int practitioner_id)
        {
            this.AppointmentDate = appointmentDate;
            this.AppointmentTime = appointmentTime;
            this.Patient_ID = patient_id;
            this.Practitioner_ID = practitioner_id;
        }

        /// <summary>
        /// Overloaded Appointment Constructor - Creates an Appointment from the values passed in a DataRow (from an existing appointment). This method is used by the appointment(s) class (a list class).
        /// </summary>
        /// <param name="appointmentRow"></param>
        public Appointment(DataRow appointmentRow)
        {
            //Assign the Appointment detail to this class's properties
            this.Practitioner_ID = (int)appointmentRow["Practitioner_ID"];
            this.PractitionerType = (string)appointmentRow["PractnrTypeName_Ref"];
            this.PractitionerFirstName = (string)appointmentRow["FirstName"];
            this.PractitionerLastName = (string)appointmentRow["LastName"];
            this.AppointmentDate = (DateTime)appointmentRow["AppointmentDate"];
            this.AppointmentTime = (TimeSpan)appointmentRow["AppointmentTime_Ref"];
            this.Patient_ID = (int)appointmentRow["Patient_ID"];
        }
        #endregion Constructors

        #region Private Methods

        #endregion Private Methods

        #region Public Data Methods
            public int MakeAppointment()
        {
            try
            {
                //Create an instance of the DAL
                SqlDataAccessLayer myDal = new SqlDataAccessLayer();
                //Declare and initialise an array parameters to pass to the DAL.ExecuteNonQuerySP
                SqlParameter[] parameters =
                {
                    new SqlParameter ("@Pracitioner_ID", this.Practitioner_ID),
                    new SqlParameter ("@AppointmentDate", this.AppointmentDate.ToShortDateString()),
                    new SqlParameter ("@AppointmentTime", this.AppointmentTime),
                    new SqlParameter ("@Patient_ID", this.Patient_ID)
                };

                //Explicitly convert the Date and Time values to .SqlDbType.Date etc
                parameters[1].SqlDbType = SqlDbType.Date;
                parameters[2].SqlDbType = SqlDbType.Time;
                //Assign the result of the call to the ExecuteNonQuerySP() to the return variable
                int rowsAffected = myDal.ExecuteNonQuerySP("usp_CreateAppointment", parameters);
                return rowsAffected;
            }
            catch (Exception ex)
            {

                throw new Exception("Unable to create new appointment!", ex);
            }
        }

            public int CancelAppointment()
        {
            try
            {
                //Create an instance of the DAL
                SqlDataAccessLayer myDal = new SqlDataAccessLayer();
                //Declare and initialise an array parameters to pass to the DAL.ExecuteNonQuerySP
                SqlParameter[] parameters =
                {
                    new SqlParameter ("@Pracitioner_ID", this.Practitioner_ID),
                    new SqlParameter ("@AppointmentDate", this.AppointmentDate.ToShortDateString()),
                    new SqlParameter ("@AppointmentTime", this.AppointmentTime),
                    new SqlParameter ("@Patient_ID", this.Patient_ID)
                };

                //Explicitly convert the Date and Time values to .SqlDbType.Date etc
                parameters[1].SqlDbType = SqlDbType.Date;
                parameters[2].SqlDbType = SqlDbType.Time;
                //Assign the result of the call to the ExecuteNonQuerySP() to the return variable
                int rowsAffected = myDal.ExecuteNonQuerySP("usp_CancelAppointment", parameters);
                return rowsAffected;
            }
            catch (Exception ex)
            {

                throw new Exception("Unable to cancel appointment!", ex);
            }
        }
        #endregion Public Data Methods



    }
}
