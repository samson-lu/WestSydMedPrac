using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WestSydMedPrac.Classes
{
    public sealed class Practitioners : List<Practitioner>
    {
        //Constructors
        public Practitioners()
        {
            //Get all Practitioner
            GetAllPractitioners();
        }

        //Public methods
        public void Refresh()
        {
            //Clear the Practitioner objects from within this classes internal list
            //and go back to the database and get the latest of list of all patients
            this.Clear();
            GetAllPractitioners();
        }

        //private methods
        private void GetAllPractitioners()
        {
            //Get all Practitioners
            SqlDataAccessLayer myDal = new SqlDataAccessLayer();

            DataTable practitionerTable = myDal.ExecuteStoredProc("usp_GetAllPractitioners");

            foreach (DataRow practitionerRow in practitionerTable.Rows)
            {
                Practitioner aPractitioner = new Practitioner(practitionerRow);
                this.Add(aPractitioner);
            }
        }
    }
}

