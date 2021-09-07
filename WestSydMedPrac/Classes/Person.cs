using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestSydMedPrac.Classes
{
    //'abstract' modifiers prevents us from instantiating an object from this class.
    public abstract class Person
    {
        #region Private Field Variables
        // Field variables (also referred to as member variables are used to 
        //hold the property data of the class.) This is the part of the mechanism that implements
        //the OOP concept of encapsulation. Encapsulation is the notion that the data of an object
        //should be protected from unvalidated access. Access to the protected data must occur through a
        //validation checkpoint where the values being read from (get) or written to (set) the variables
        //can be check. We can do this in the 'accessors' (getters and setters methods)
        private int _personID; //This will hold the ID of the person
        private string _homePhone;
        private string _firstName;
        private string _lastName;
        private string _street;
        private string _suburb;
        private string _mobile;
        private string _state;
        private string _postCode;
        #endregion Private Field Variables

        #region Properties

        //public string Gender { get; set; }

        public int PersonID
        {
            get
            {
                return _personID;
            }
            set
            {
                _personID = value;
            }
        }


        
        //The 'virtual' keyword is used to modify a method, property, or event declaration
        //so that it can be overriden in the derived class. So the following properties
        //can be overridden by any class that inherits them.
        public virtual string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        

        public virtual string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        

        public virtual string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        

        public virtual string Suburb
        {
            get { return _suburb; }
            set { _suburb = value; }
        }

        

        public virtual string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        

        public virtual string State
        {
            get { return _state; }
            set { _state = value; }
        }

        

        public virtual string PostCode
        {
            get { return _postCode; }
            set { _postCode = value; }
        }

        

        public virtual string HomePhone
        {
            get { return _homePhone; }
            set { _homePhone = value; }
        }


        #endregion Properties

        #region Constructors

        #endregion Constructors

        #region Private Methods

        #endregion Private Methods

        #region Public Data Methods
        public virtual int Get() //This is a 'get' the patient from the DB.
        {
            throw new System.NotImplementedException();
        }

        public virtual int Update()
        {
            throw new System.NotImplementedException();
        }

        public virtual int Delete()
        {
            throw new System.NotImplementedException();
        }

        public virtual int Insert()
        {
            throw new System.NotImplementedException();
        }
        #endregion Public Data Methods
    }
}
