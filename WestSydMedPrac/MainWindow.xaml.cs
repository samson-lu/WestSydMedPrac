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

        //Get all the Patients from the Database
        Patients allPatients = new Patients();

        /// <summary>
        /// MainWindow for the Western Sydne Medical Practice uses a Tab control
        /// to manipulate Practitioner, Patient, and Appointment data.
        /// </summary>
        public MainWindow() //Constructor: Same name as the class. Has no return type and can be overloaded.
        {
            InitializeComponent();

            //Load the Patient's tab ListView
            LoadPatientListView();
        }

        private void LoadPatientListView()
        {
            //Bind the Patient's Collection to the ListView Control
            lvPatients.ItemsSource = allPatients;
        }

        private void tabctrlMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lvPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPatFirstRecord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPatPreviousRecord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPatNextRecord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPatLastRecord_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cboGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboPatState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }




    //  //string cnnStr = ConfigurationManager.ConnectionStrings["cnnStrWSMP"].ConnectionString;
    //  //SqlConnection myCnn = new SqlConnection(cnnStr);

    //  //try
    //  {
    //      //myCnn.Open();
    //      //MessageBox.Show("Connected Successfully");
    //      //SqlCommand myCmd = new SqlCommand("usp_GetAllPatients", myCnn);
    //      //myCmd.CommandType = CommandType.StoredProcedure;
    //      //SqlDataReader drPatients = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
    //      //DataTable dtPatients = new DataTable();
    //      //dtPatients.Load(drPatients);

    //      //string patientDetails = string.Empty;
    //      //foreach (DataRow drPatient in dtPatients.Rows)
    //      //{
    //      //    patientDetails += drPatient["Patient_ID"] + " " + drPatient["FirstName"] + "\n";

    //      //}
    //      //Patient myExistingPatient = new Patient(7);
    //      //MessageBox.Show($" {myExistingPatient.Patient_ID} \n First Name: {myExistingPatient.FirstName}, Last Name: {myExistingPatient.LastName}");

    //      //myExistingPatient.LastName = "Weinhouse";
    //      //if(myExistingPatient.Update() == 1)
    //      //{
    //      //    MessageBox.Show("Update worked");
    //      //}

    //      //if(MessageBox.Show("Are you sure you want to permanently DELETE this Patient?","DELETE?", MessageBoxButton.YesNo) == MessageBoxResult.Yes) 
    //      //{
    //      //    if(myExistingPatient.Delete() == 1)
    //      //    {
    //      //        MessageBox.Show("Patient DELETED FOR EVER!!");
    //      //    }
    //      //}

    //      //********************************************************************************************************************************************************************
    //      txtBlkErrorMessage.Text = string.Empty;

    //      Patient gottenPatient = new Patient();
    //      //Check that the User enters an integer.
    //      if(int.TryParse(txtPatient_ID.Text, out int patientID))
    //      {
    //          gottenPatient.Patient_ID = patientID;
    //          if (gottenPatient.Get() != 1)
    //          {
    //              txtBlkErrorMessage.Text = $"There are no patients with Patient ID {gottenPatient.Patient_ID}! Please enter a different Patient ID.";
    //          }
    //          else
    //          {
    //              MessageBox.Show(gottenPatient.Patient_ID.ToString() + " " + gottenPatient.FirstName + " " + gottenPatient.LastName);

    //              //The 'foreach' is designed by Microsoft to iterate objects that implement IEnumerable and IEnumerator interfaces.

    //              string appoinmentList = string.Empty;
    //              foreach (Appointment anAppointment in gottenPatient.Appointments)
    //              {
    //                  appoinmentList += $"Pracitioner ID: {anAppointment.Practitioner_ID} Practitioner: {anAppointment.PractitionerFirstName} {anAppointment.PractitionerLastName} Date: {anAppointment.AppointmentDate} Time: {anAppointment.AppointmentTime} Patient ID: {anAppointment.Patient_ID} \n";
    //              }

    //              MessageBox.Show(appoinmentList);

    //          }
    //      }
    //      else
    //      {
    //          txtBlkErrorMessage.Text = "You have entered an invalid Patient_ID. Please enter a number.";
    //      }



    //  }
    //  catch (SqlException sqlEx)//Catch the more specific exceptions that we anticipate
    //  {
    //      MessageBox.Show("SQL Exception Occured " + sqlEx.Message);
    //  }
    //  catch (Exception ex)//Catch everything else that we have not anticipated
    //  {
    //      MessageBox.Show("General Exception Occurred " + ex.Message);
    //  }
    //  finally //Regardless of whether we have an exception occurs or not, do the following
    //  {
    //      //if the connection is still open, then close it.
    //      //if (myCnn.State == ConnectionState.Open)
    //      //{
    //      //    myCnn.Close();
    //      //}
    //  }

    ////***************************************************************************************************************************************************************************
}
