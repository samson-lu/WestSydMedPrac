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
    //The 'sealed' access modifier prevents other classes from inheriting this class.
    public sealed class Patient : Person
    {
        #region Private Field Variables

        private DataTable _dtPatient;

        #endregion Private Field Variables

        #region Properties
        public int Patient_ID { get; set; }
        public string FullNameAndDOB 
        {
            get
            {
                //String Interpolation is used to concatenate and embed strings and string variables
                //into a single string.
                //Syntax: $"The string literal {anEmbeddedVariable} and the rest.."
                return $"{this.LastName}, {this.FirstName} {this.DateOfBirth.ToShortDateString()}";
            }

        }

        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string MedicareNumber { get; set; }
        public string PatientNotes { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }


        #endregion Properties

        #region Constructors
        /// <summary>
        /// Default Constructor: Instantiates a 'new' empty patient object.
        /// </summary>

        public Patient() : base() //This is a call to the base class constructor (which we haven't defined)br
        {
            //Does nothing.
        } 

        public Patient(int patient_ID) : base()
        {
            //Get the Patient's detail from the database.
            SqlDataAccessLayer myDal = new SqlDataAccessLayer();
            try
            {
                
                //Set up the parameter array for the Stored Procedure to accept the Patient_ID
                SqlParameter[] parameters = { new SqlParameter("@Patient_ID", patient_ID) };

                //The following line calls the method on our DAL that actually does the work.
                this._dtPatient = myDal.ExecuteStoredProc("usp_GetPatient", parameters);

                //First check if the Datatable has any rows
                if(_dtPatient != null && _dtPatient.Rows.Count > 0)
                {
                    //Map the patient's details to this class's properties by passing the first row
                    //of the table which has the Patient's details in it.
                    LoadPatientProperties(_dtPatient.Rows[0]);
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Unable to retrieve Patient details!", ex);
            }
            finally
            {
                //Clean up 
                this._dtPatient.Dispose();
                this._dtPatient = null;
                myDal = null;
            }
        }




        #endregion Constructors


        #region Private Methods

        private void LoadPatientProperties(DataRow dataRow)
        {
            this.Patient_ID = (int)dataRow["Patient_ID"];
            this.Gender = dataRow["Gender"].ToString();
            this.DateOfBirth = (DateTime)dataRow["DateOfBirth"];
            this.FirstName = dataRow["FirstName"].ToString();
            this.LastName = dataRow["LastName"].ToString();
            this.Street = dataRow["Street"].ToString();
            this.Suburb = dataRow["Suburb"].ToString();
            this.State = dataRow["State"].ToString();
            this.PostCode = dataRow["PostCode"].ToString();
            this.HomePhone = dataRow["HomePhone"].ToString();
            this.Mobile = dataRow["Mobile"].ToString();
            this.MedicareNumber = dataRow["MedicareNumber"].ToString();
            this.PatientNotes = dataRow["Notes"].ToString();

            //Get the patient's Appointments and assign the appointment
            //property
            Appointments appointments = new Appointments(this);
            this.Appointments = appointments;

        }



        #endregion Private Methods

        #region Public Data Methods
        public override int Get()
        {
            try
            {
                SqlDataAccessLayer myDal = new SqlDataAccessLayer();

                //Set up the parameter array for the Stored Procedure to accept the Patient_ID
                SqlParameter[] parameters = { new SqlParameter("@Patient_ID", this.Patient_ID) };

                //The following line calls the method on our DAL that actually does the work.
                this._dtPatient = myDal.ExecuteStoredProc("usp_GetPatient", parameters);

                //First check if the Datatable has any rows
                if (_dtPatient != null && _dtPatient.Rows.Count > 0)
                {
                    //Map the patient's details to this class's properties by passing the first row
                    //of the table which has the Patient's details in it.
                    LoadPatientProperties(_dtPatient.Rows[0]);
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Unable to retrieve the Patient's details!", ex);
            }
        }

        public override int Update() 
        {
            try
            {
                SqlDataAccessLayer myDal = new SqlDataAccessLayer();
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Patient_ID", this.Patient_ID),
                    new SqlParameter("@Gender", this.Gender),
                    new SqlParameter("@DateOfBirth", this.DateOfBirth),
                    new SqlParameter("@FirstName", this.FirstName),
                    new SqlParameter("@LastName", this.LastName),
                    new SqlParameter("@Street", this.Street),
                    new SqlParameter("@Suburb", this.Suburb),
                    new SqlParameter("@State",this.State),
                    new SqlParameter("@PostCode", this.PostCode),
                    new SqlParameter("@HomePhone", this.HomePhone),
                    new SqlParameter("@Mobile", this.Mobile),
                    new SqlParameter("@MedicareNumber", this.MedicareNumber),
                    new SqlParameter("@Notes", this.PatientNotes)
                };

                //Have to explicity convert date value for DateOfBirth (2eth parameter) to SqlDbType.Date
                parameters[2].SqlDbType = SqlDbType.Date;

                //Define a variable for the value to be returned by this method.
                int rowsAffected = myDal.ExecuteNonQuerySP("usp_UpdatePatient", parameters);
                return rowsAffected;
            }
            catch (Exception ex)
            {

                throw new Exception("The Patient's details could not be updated!", ex);
            }
        }

        public override int Delete()
        {
            try
            {
                SqlDataAccessLayer myDAL = new SqlDataAccessLayer();
                //CLASS TASK
                //Write the code to delete a partien from the database.
                //Use 'usp_DeletePatient' stored procedure. That stored procedure only take 1 parameter
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Patient_ID",this.Patient_ID)
                };

                int rowsAffected = myDAL.ExecuteNonQuerySP("usp_DeletePatient", parameters);
                return rowsAffected;

            }
            catch (Exception ex)
            {

                throw new Exception("The Patient's details could not be deleted!", ex);
            }
        }

        public override int Insert()
        {
            try
            {
                SqlDataAccessLayer myDal = new SqlDataAccessLayer();
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Patient_ID", this.Patient_ID),
                    new SqlParameter("@Gender", this.Gender),
                    new SqlParameter("@DateOfBirth", this.DateOfBirth),
                    new SqlParameter("@FirstName", this.FirstName),
                    new SqlParameter("@LastName", this.LastName),
                    new SqlParameter("@Street", this.Street),
                    new SqlParameter("@Suburb", this.Suburb),
                    new SqlParameter("@State",this.State),
                    new SqlParameter("@PostCode", this.PostCode),
                    new SqlParameter("@HomePhone", this.HomePhone),
                    new SqlParameter("@Mobile", this.Mobile),
                    new SqlParameter("@MedicareNumber", this.MedicareNumber),
                    new SqlParameter("@Notes", this.PatientNotes)
                };

                //Have to explicity convert date value for DateOfBirth (2eth parameter) to SqlDbType.Date
                parameters[2].SqlDbType = SqlDbType.Date;

                //Define a variable for the value to be returned by this method.
                int rowsAffected = myDal.ExecuteNonQuerySP("usp_InsertPatient", parameters);
                return rowsAffected;
            }
            catch (Exception ex)
            {

                throw new Exception("The Patient could not be added!", ex);
            }
        }

        public void GetAppointments()
        {
            try
            {
                //Get all of this patients appointments
                Appointments myAppointments = new Appointments(this);
                //Assign the appointments to this Patient's enumerated list of appointments
                this.Appointments = myAppointments;
            }
            catch (Exception ex)
            {

                throw new Exception("Unable to retrieve the Patient's Appointments!", ex);
            }
        }
        #endregion Public Data Methods
    }
}
