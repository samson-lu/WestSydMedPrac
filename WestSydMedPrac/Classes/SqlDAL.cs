using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Add the following references
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;

namespace WestSydMedPrac.Classes
{
    public class SqlDataAccessLayer
    {
        #region Private Properties

        private string _connString;

        #endregion Private Properties

        #region Constructors

        public SqlDataAccessLayer()
        {
            //Get the connection string from the App.Config file.
            _connString = ConfigurationManager.ConnectionStrings["cnnStrWSMP"].ConnectionString;
        }

        #endregion Constructors

        #region Methods

        public DataTable ExecuteStoredProc(string SPName, SqlParameter[] parameters)
        {
            //Create a connection object
            SqlConnection conn = new SqlConnection(_connString);

            //Createa command object
            SqlCommand cmd = new SqlCommand(SPName, conn);

            

            cmd.CommandType = CommandType.StoredProcedure;

            FillParameter(cmd, parameters);

            cmd.Connection.Open();

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            DataTable dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }

        private void FillParameter(SqlCommand cmd, SqlParameter[] parameters)
        {
            //TODO - Explain the passing of Reference Types and Value Types to Methods... 
            //for (int i = 0; i < parameters.Length; i++)
            //{
            //    cmd.Parameters.Add(parameters[i]);
            //}

            foreach (SqlParameter parameter in parameters)
            {
                cmd.Parameters.Add(parameter);
            }
        }

        //'internal' access modifier means that the method is 'public' only to other code in 
        //this assembly.
        internal int ExecuteNonQuerySP(string SPName, SqlParameter[] parameters)
        {
            //Create a connection object
            SqlConnection conn = new SqlConnection(_connString);

            //Createa command object
            SqlCommand cmd = new SqlCommand(SPName, conn);



            cmd.CommandType = CommandType.StoredProcedure;
            

            FillParameter(cmd, parameters);

            cmd.Connection.Open();

            //Execute the Stored Procedure
            int _ = cmd.ExecuteNonQuery();
            //C# from version 7.0 implemented a thing called a discard, which where the variable itself
            //isn't actually required

            Debug.Print($"The db connection is  { cmd.Connection.State.ToString()}");
            if(cmd.Connection.State == ConnectionState.Open)
            {
                cmd.Connection.Close();
            }

            //Return the result to the calling code
            return _;

        }

        #endregion Methods
    }
}
