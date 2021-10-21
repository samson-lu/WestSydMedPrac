using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WestSydMedPrac.Classes
{
        public sealed class Practitioner : Person
        {
            private DataTable _dtPractitioner;

            public int Practitioner_ID { get; set; }

            public string FullNameAndType
            {
                get
                {
                    return $"{this.LastName},{this.FirstName}{this.PractnrTypeName_Ref}";
                }
            }

            public string PractnrTypeName_Ref { get; set; }

            public string RegistrationNumber { get; set; }
            public IEnumerable<Appointment> Appointments { get; set; }
            public bool Monday { get; set; }
            public bool Tuesday { get; set; }
            public bool Wednesday { get; set; }
            public bool Thursday { get; set; }
            public bool Friday { get; set; }
            public bool Saturday { get; set; }
            public bool Sunday { get; set; }
            //Constructors
            public Practitioner() : base()
            {
                //default constructor, does nothing
            }

            public Practitioner(int practitioner_ID) : base()
            {
                //Get the Practitioner's details from the database
                SqlDataAccessLayer myDal = new SqlDataAccessLayer();

                try
                {
                    //Set up the parameter array for the Stored Proc to accept Practitioner_ID
                    SqlParameter[] parameters = { new SqlParameter("@Practitioner_ID", practitioner_ID) };

                    //The following line calls the method on our DAL that actually does the work
                    this._dtPractitioner = myDal.ExecuteStoredProc("usp_GetPractitioner", parameters);

                    //First check if the Datatable has any rows
                    if (_dtPractitioner != null && _dtPractitioner.Rows.Count > 0)
                    {
                        //Map the Practitioners detail to this class's properties by passing the first row of the table which has the Practitioner's detail in it
                        LoadPractitionerProperties(_dtPractitioner.Rows[0]);
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Unable to retrieve Practitioner details!", ex);
                }
                finally
                {
                    //Clean up
                    this._dtPractitioner.Dispose();
                    this._dtPractitioner = null;
                    myDal = null;
                }

            }

            //Instantiates a Practitioner object from a DataRow: Used by the Practitioner Collection Class
            public Practitioner(DataRow practitionerRow) : base()
            {
                LoadPractitionerProperties(practitionerRow);
            }

            //Public Methods
            public static int ComparePractitionerName(Practitioner p1, Practitioner p2)
            {
                return p1.LastName.CompareTo(p2.LastName);
            }

            //Private Methods
            private void LoadPractitionerProperties(DataRow dataRow)
            {
                this.Practitioner_ID = (int)dataRow["Practitioner_ID"];
                this.PractnrTypeName_Ref = dataRow["PractnrTypeName_Ref"].ToString();
                this.FirstName = dataRow["FirstName"].ToString();
                this.LastName = dataRow["LastName"].ToString();
                this.Street = dataRow["Street"].ToString();
                this.Suburb = dataRow["Suburb"].ToString();
                this.State = dataRow["State"].ToString();
                this.PostCode = dataRow["PostCode"].ToString();
                this.HomePhone = dataRow["HomePhone"].ToString();
                this.Mobile = dataRow["Mobile"].ToString();
                this.RegistrationNumber = dataRow["RegistrationNumber"].ToString();
                this.Monday = (bool)dataRow["Monday"];
                this.Tuesday = (bool)dataRow["Tuesday"];
                this.Wednesday = (bool)dataRow["Wednesday"];
                this.Thursday = (bool)dataRow["Thursday"];
                this.Friday = (bool)dataRow["Friday"];
                this.Saturday = (bool)dataRow["Saturday"];
                this.Sunday = (bool)dataRow["Sunday"];

                //Get the Practitioner's Appointments and assign the appointment property
                Appointments appointments = new Appointments(this);
                this.Appointments = appointments;
            }

            //public data methods
            public override int Get()
            {
                try
                {
                    SqlDataAccessLayer myDal = new SqlDataAccessLayer();

                    //Setup the parameter array for the stored proc to accept the Practitioner_ID
                    SqlParameter[] parameters = { new SqlParameter("@Practitioner_ID", this.Practitioner_ID) };

                    //The following line calls the method on our DAL that actually does the work
                    this._dtPractitioner = myDal.ExecuteStoredProc("usp_GetPractitioner", parameters);

                    //First check if the DataTable has any rows
                    if (_dtPractitioner != null && _dtPractitioner.Rows.Count > 0)
                    {
                        //Map the Practitioner's details to this class's properties by passing the first row of
                        //the table which has the Practitioner's details in it.
                        LoadPractitionerProperties(_dtPractitioner.Rows[0]);
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {

                    throw new Exception("Unable to retrieve Practitioner's detail!", ex);
                }
            }

            public override int Update()
            {
                try
                {
                    SqlDataAccessLayer myDal = new SqlDataAccessLayer();
                    SqlParameter[] parameters =
                    {
                    new SqlParameter("@Practitioner_ID", this.Practitioner_ID),
                    new SqlParameter("@PractnrTypeName_Ref", this.PractnrTypeName_Ref),
                    new SqlParameter("@FirstName", this.FirstName),
                    new SqlParameter("@LastName", this.LastName),
                    new SqlParameter("@Street", this.Street),
                    new SqlParameter("@Suburb", this.Suburb),
                    new SqlParameter("@State", this.State),
                    new SqlParameter("@PostCode", this.PostCode),
                    new SqlParameter("@HomePhone", this.HomePhone),
                    new SqlParameter("@Mobile", this.Mobile),
                    new SqlParameter("@RegistrationNumber", this.RegistrationNumber),
                    new SqlParameter("@Monday", this.Monday),
                    new SqlParameter("@Tuesday", this.Tuesday),
                    new SqlParameter("@Wednesday", this.Wednesday),
                    new SqlParameter("@Thursday", this.Thursday),
                    new SqlParameter("@Friday", this.Friday),
                    new SqlParameter("@Saturday", this.Saturday),
                    new SqlParameter("@Sunday", this.Sunday)
                };

                    //convert date value to bit type
                    parameters[11].SqlDbType = SqlDbType.Bit;
                    parameters[12].SqlDbType = SqlDbType.Bit;
                    parameters[13].SqlDbType = SqlDbType.Bit;
                    parameters[14].SqlDbType = SqlDbType.Bit;
                    parameters[15].SqlDbType = SqlDbType.Bit;
                    parameters[16].SqlDbType = SqlDbType.Bit;
                    parameters[17].SqlDbType = SqlDbType.Bit;




                    //Define a variable for the value to be returned by this method
                    int rowsAffected = myDal.ExecuteNonQuerySP("usp_UpdatePractioner", parameters);
                    return rowsAffected;
                }
                catch (Exception ex)
                {

                    throw new Exception("The Practitioner's details could not be updated!", ex);
                }
            }

            public override int Delete()
            {
                try
                {
                    SqlDataAccessLayer myDal = new SqlDataAccessLayer();
                    SqlParameter[] parameters =
                    {
                    new SqlParameter ("@Practitioner_ID", this.Practitioner_ID)
                };

                    int rowsAffected = myDal.ExecuteNonQuerySP("usp_DeletePractitioner", parameters);
                    return rowsAffected;
                }
                catch (Exception ex)
                {

                    throw new Exception("The Practitioner details could not be Deleted!", ex);
                }
            }

            public override int Insert()
            {
                try
                {
                    SqlDataAccessLayer myDal = new SqlDataAccessLayer();
                    SqlParameter[] parameters =
                    {
                    new SqlParameter("@PractnrTypeName_Ref", this.PractnrTypeName_Ref),
                    new SqlParameter("@FirstName", this.FirstName),
                    new SqlParameter("@LastName", this.LastName),
                    new SqlParameter("@Street", this.Street),
                    new SqlParameter("@Suburb", this.Suburb),
                    new SqlParameter("@State", this.State),
                    new SqlParameter("@PostCode", this.PostCode),
                    new SqlParameter("@HomePhone", this.HomePhone),
                    new SqlParameter("@Mobile", this.Mobile),
                    new SqlParameter("@RegistrationNumber", this.RegistrationNumber),
                    new SqlParameter("@Monday", this.Monday),
                    new SqlParameter("@Tuesday", this.Tuesday),
                    new SqlParameter("@Wednesday", this.Wednesday),
                    new SqlParameter("@Thursday", this.Thursday),
                    new SqlParameter("@Friday", this.Friday),
                    new SqlParameter("@Saturday", this.Saturday),
                    new SqlParameter("@Sunday", this.Sunday)
                };


                    //convert date value to bit type
                    parameters[10].SqlDbType = SqlDbType.Bit;
                    parameters[11].SqlDbType = SqlDbType.Bit;
                    parameters[12].SqlDbType = SqlDbType.Bit;
                    parameters[13].SqlDbType = SqlDbType.Bit;
                    parameters[14].SqlDbType = SqlDbType.Bit;
                    parameters[15].SqlDbType = SqlDbType.Bit;
                    parameters[16].SqlDbType = SqlDbType.Bit;


                    int rowsAffected = myDal.ExecuteNonQuerySP("usp_InsertPractitioner", parameters);
                    return rowsAffected;

                }
                catch (Exception ex)
                {

                    throw new Exception("The Practitioner could not be added!", ex);
                }
            }

            public void GetAppointments()
            {
                try
                {
                    //Get all of this Practitioner's appointment
                    Appointments myAppointments = new Appointments(this);
                    //Assign the appointments to this Practitioner's enumerated list of appointments
                    this.Appointments = myAppointments;
                }
                catch (Exception ex)
                {

                    throw new Exception("Unable to retrieve the Practitioner's Appointments!", ex);
                }
            }
        }
    
}
