﻿using System;
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

            //Sort the Patient List by LastName
            //http://www.codedigest.com/Articles/CSHARP/84_Sorting_in_Generic_List.aspx
            Comparison<Patient> compareLastName = new Comparison<Patient>(Patient.ComparePatientName);
            allPatients.Sort(compareLastName);

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
            //There's no point going anymore if there are no records in the list.
            if(lvPatients.Items.Count > 0)
            {
                //Set the .SelectedIndex to the first item in the list
                lvPatients.SelectedIndex = 0;
                //Scroll the list view up to the top if it's a long list.
                lvPatients.ScrollIntoView(lvPatients.SelectedItem);
            }
        }

        private void btnPatPreviousRecord_Click(object sender, RoutedEventArgs e)
        {
            //We want at least one record 
            if(lvPatients.SelectedIndex >= 1)
            {
                lvPatients.SelectedIndex = lvPatients.SelectedIndex - 1;
                lvPatients.ScrollIntoView(lvPatients.SelectedItem);
            }
        }

        private void btnPatNextRecord_Click(object sender, RoutedEventArgs e)
        {
            //TODO - Test this with no patients in the list
            if(lvPatients.SelectedIndex < lvPatients.Items.Count - 1)
            {
                lvPatients.SelectedIndex = lvPatients.SelectedIndex + 1;
                lvPatients.ScrollIntoView(lvPatients.SelectedItem);
            }
        }

        private void btnPatLastRecord_Click(object sender, RoutedEventArgs e)
        {
            //Move to the last Item in the list
            if(lvPatients.Items.Count > 0)
            {
                lvPatients.SelectedIndex = lvPatients.Items.Count - 1;
                lvPatients.ScrollIntoView(lvPatients.SelectedItem);
            }
        }

        private void cboGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboPatState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddNewPatient_Click(object sender, RoutedEventArgs e)
        {
            //Toggle the button between "Add New" and "Save"
            if(btnAddNewPatient.Content.ToString() == "Add New")
            {
                //Toggle the button to "Save"
                btnAddNewPatient.Content = "Save";
                //Deselect the ListView item
                lvPatients.SelectedIndex = -1;
                //Disable the ListView 
                lvPatients.IsEnabled = false;
                //Clear the controls 
                //TODO - Revisit this using a recursive method call that check the children type.
                //ClearPatientTabControls();


                //Toggle the other buttons also
                btnCancelPatient.IsEnabled = true;
                btnUpdatePatient.IsEnabled = false;
                btnDeletePatient.IsEnabled = false;

            }
            else
            {
                //Do the save
                //Check if the control validate (i.e. that we have data in the controls)
                if (ValidatePatientControls())
                {
                    //Do the save
                    Patient newPatient = new Patient();
                    AssignPropertiesToPatient(newPatient);
                    
                    if(newPatient.Insert() == 1)
                    {
                        MessageBox.Show("New Patient's details has been successfully saved!", "Details Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                        //Refresh the data in the ListView
                        RefreshPatientList();
                        //Move to the last item in the ListView
                        lvPatients.SelectedIndex = lvPatients.Items.Count - 1;
                        lvPatients.ScrollIntoView(lvPatients.SelectedItem);
                    }
                }

                //Toggle the button back to "Add New"
                btnAddNewPatient.Content = "Add New";
                btnCancelPatient.IsEnabled = false;
                btnUpdatePatient.IsEnabled = true;
                btnDeletePatient.IsEnabled = true;
                lvPatients.IsEnabled = true;
            }
        }

        private void RefreshPatientList()
        {
            //Get the latest view of the Patients from the Database.
            allPatients = null;
            allPatients = new Patients();
            LoadPatientListView();
        }

        private void AssignPropertiesToPatient(Patient newPatient)
        {
            //'newPatient' is a class. i.e it is a "reference type". Don't forget that when we pass a reference type as an argument to a method, what we're actually doing is creating a pointer to the same object that's passed. So, if we modify the object in the method that it's passed to, we're modifying the original.

            //Note: That the Patient_ID is not derived from the user interface
            //but rather is 'autogenerated' by the database each time a new patient record
            //is inserted to the database.
            newPatient.Gender = cboGender.Text;
            newPatient.DateOfBirth = (DateTime)dtpPatDateOfBirth.SelectedDate;
            newPatient.FirstName = txtPatFirstName.Text;
            newPatient.LastName = txtPatLastName.Text;
            newPatient.Street = txtPatStreet.Text;
            newPatient.Suburb = txtPatSuburb.Text;
            newPatient.State = cboPatState.Text;
            newPatient.PostCode = txtPatPostCode.Text;
            newPatient.HomePhone = txtPatHomePhone.Text;
            newPatient.Mobile = txtPatMobilePhone.Text;
            newPatient.MedicareNumber = txtPatMedicareNumber.Text;
            newPatient.PatientNotes = txtPatNotes.Text;
        }

        private bool ValidatePatientControls()
        {
            if(cboGender.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Gender!", "Patient Gender?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (dtpPatDateOfBirth.SelectedDate == null)
            {
                MessageBox.Show("Please select a Date Of Birth!", "Patient Date Of Birth?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatFirstName.Text == string.Empty || txtPatFirstName.Text == null)
            {
                MessageBox.Show("Please enter a First Name!", "First Name?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatLastName.Text == string.Empty || txtPatLastName.Text == null)
            {
                MessageBox.Show("Please enter a Last Name!", "Last Name?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatStreet.Text == string.Empty || txtPatStreet.Text == null)
            {
                MessageBox.Show("Please enter Street address details!", "Street Number & Name?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatSuburb.Text == string.Empty || txtPatSuburb.Text == null)
            {
                MessageBox.Show("Please enter a Suburb Name!", "Suburb Name?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (cboPatState.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a State!", "State?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatPostCode.Text == string.Empty || txtPatPostCode.Text == null)
            {
                MessageBox.Show("Please enter a Post Code!", "Post Code?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatHomePhone.Text == string.Empty || txtPatHomePhone.Text == null)
            {
                MessageBox.Show("Please enter a Home Phone number!", "Home Phone Number?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatMobilePhone.Text == string.Empty || txtPatMobilePhone.Text == null)
            {
                MessageBox.Show("Please enter a Mobile Phone Number!", "Mobile Phone Number?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else if (txtPatMedicareNumber.Text == string.Empty || txtPatMedicareNumber.Text == null)
            {
                MessageBox.Show("Please enter a Medicare Number!", "Medicare Number?", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void ClearPatientTabControls()
        {
            txtPatient_ID.Clear();
            cboGender.SelectedIndex = -1;
            dtpPatDateOfBirth.SelectedDate = null;
            txtPatFirstName.Clear();
            txtPatLastName.Clear();
            txtPatStreet.Clear();
            txtPatSuburb.Clear();
            cboPatState.SelectedIndex = -1;
            txtPatPostCode.Clear();
            txtPatHomePhone.Clear();
            txtPatMobilePhone.Clear();
            txtPatMedicareNumber.Clear();
            txtPatNotes.Clear();
            
        }

        private void btnUpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            //Check that a Patient is selected first
            if(lvPatients.SelectedItem != null)
            {
                //Show the user a message that they're about to do an update, ask them if they really want to proceed.
                string message = "The Patient's details will be Updated! \n Do you with to continue?";
                string caption = "Update Patient?";
                if (MessageBox.Show(message,caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    //Grab the selected Patient from the list view control.
                    Patient selectedPatient;
                    selectedPatient = (Patient)lvPatients.SelectedItem;
                    //Get the value of the selected index (i.e. take note of which patient we're on in the ListView).
                    //so that we can return to the same row after we've done the update.
                    int selectedIndex = lvPatients.SelectedIndex;
                    AssignPropertiesToPatient(selectedPatient);

                    try
                    {
                        //If they say yes.
                        //then do update.
                        if(selectedPatient.Update() == 1)
                        {
                            MessageBox.Show("Patient's details successfully updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            RefreshPatientList();
                            //Move the focus back to the patient that was just updated
                            lvPatients.SelectedIndex = selectedIndex;
                            lvPatients.ScrollIntoView(lvPatients.SelectedIndex);
                        }
                    }
                    catch (Exception ex)
                    {

                        message = $"Something went wrong! \n The Patient's details were NOT updated. \n {ex.Message}";
                        MessageBox.Show(message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    } 
                }
            }
            else
            {
                MessageBox.Show("Please select a Patient to be updated first.", "Select a Patient", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {
            //Check that a Patient is selected first
            if(lvPatients.SelectedItem != null)
            {
                //Ask the user if they're really wanat to do the delete.
                string message = $"Are you sure you want to Delete this Patient? \nThe Patient's details and all Appointments will be permanently deleted!";
                string caption = "Delete Patient?";

                //If they say YES, then 'Try' and do the delete.
                if(MessageBox.Show(message,caption, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Do the delete
                    Patient selectedPatient;
                    selectedPatient = (Patient)lvPatients.SelectedItem;
                    try
                    {
                        if(selectedPatient.Delete() == 1)
                        {
                            //Delete Suceeded
                            MessageBox.Show("Patient Successfully deleted!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                            RefreshPatientList();
                            //Go to the first item in the list
                            lvPatients.SelectedIndex = 0;
                            lvPatients.ScrollIntoView(lvPatients.SelectedIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        message = $"Soemthing went wrong! \nThe Patient's details were NOT deleted! \n{ex.Message}";
                        MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                //Show them a message indicating success. 

                //Or show them a message indicating failure.
            }
            else
            {
                MessageBox.Show("Please select a Patient to be deleted first.");
            }

        }

        private void btnCancelPatient_Click(object sender, RoutedEventArgs e)
        {
            btnCancelPatient.IsEnabled = false;
            btnAddNewPatient.Content = "Add New";
            lvPatients.IsEnabled = true;
            btnUpdatePatient.IsEnabled = true;
            btnDeletePatient.IsEnabled = true;
            ClearPatientTabControls();
            LoadPatientListView();
            lvPatients.SelectedIndex = 0;
            lvPatients.ScrollIntoView(lvPatients.SelectedIndex);
        }

        private void btnAppointmentsPatient_Click(object sender, RoutedEventArgs e)
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
