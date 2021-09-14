using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WestSydMedPrac.Classes
{
    public sealed class Patients : List<Patient>
    {
        #region Constructors
        public Patients()
        {
            //Get all Patients
            GetAllPatients();

        }
        #endregion Constructors

        #region Public Methods
        public void Refresh()
        {
            //Clear the Patient objects from within this classes internal list
            //and go back to the database and get the latest list of all patients.
            this.Clear();

            GetAllPatients();

        }


        #endregion Public Methods


        #region Private Methods
        private void GetAllPatients()
        {
            //Get all the patients
            SqlDataAccessLayer myDAL = new SqlDataAccessLayer();

            DataTable patientTable = myDAL.ExecuteStoredProc("usp_GetAllPatients");

            foreach (DataRow patientRow in patientTable.Rows)
            {
                Patient aPatient = new Patient(patientRow);
                this.Add(aPatient);
            }
        }
        #endregion Private Methods
    }
}
