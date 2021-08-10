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
                this._dtPatient = myDal.ExecutesStoredProc("usp.GetPatient", parameters);

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
            //Appointments appointments = new Appointments(this);
            //this.Appointments = appointments;

        }



        #endregion Private Methods

        #region Public Data Methods

        #endregion Public Data Methods
    }
}
