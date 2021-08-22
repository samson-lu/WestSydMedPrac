using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Add the following References
using System.Data.SqlClient; //Connected library of classes that DO care which plateform we're connecting to.
                             //So SqlClient is specific to Microsoft SQL Server
using System.Data; //Disconnected Library of classes that don't care which 
                   //platform (i.e proprietry database server) we're connecting 
                   //e.g. DataTable, DataSet
using System.Configuration; //Contains library of classes that allow us to read the configuration file i.e App.Config file
using WestSydMedPrac.Classes;

namespace WestSydMedPrac
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() //Constructor: Same name as the class. Has no return type and can be overloaded.
        {
            InitializeComponent();
        }

        //    }
        //}


        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            //string cnnStr = ConfigurationManager.ConnectionStrings["cnnStrWSMP"].ConnectionString;
            //SqlConnection myCnn = new SqlConnection(cnnStr);

            try
            {
                //myCnn.Open();
                //MessageBox.Show("Connected Successfully");
                //SqlCommand myCmd = new SqlCommand("usp_GetAllPatients", myCnn);
                //myCmd.CommandType = CommandType.StoredProcedure;
                //SqlDataReader drPatients = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
                //DataTable dtPatients = new DataTable();
                //dtPatients.Load(drPatients);

                //string patientDetails = string.Empty;
                //foreach (DataRow drPatient in dtPatients.Rows)
                //{
                //    patientDetails += drPatient["Patient_ID"] + " " + drPatient["FirstName"] + "\n";

                //}
                Patient myExistingPatient = new Patient(5);
                MessageBox.Show($" {myExistingPatient.Patient_ID} \n First Name: {myExistingPatient.FirstName}, Last Name: {myExistingPatient.LastName}");

                myExistingPatient.LastName = "Panagopoulous";
                if(myExistingPatient.UpdatePatient() == 1)
                {
                    MessageBox.Show("Update worked");
                }

              
            }
            catch (SqlException sqlEx)//Catch the more specific exceptions that we anticipate
            {
                MessageBox.Show("SQL Exception Occured" + sqlEx.Message);
            }
            catch (Exception ex)//Catch everything else that we have not anticipated
            {
                MessageBox.Show("General Exception Occurred" + ex.Message);
            }
            finally //Regardless of whether we have an exception occurs or not, do the following
            {
                //if the connection is still open, then close it.
                //if (myCnn.State == ConnectionState.Open)
                //{
                //    myCnn.Close();
                //}
            }
        }
    }
}
