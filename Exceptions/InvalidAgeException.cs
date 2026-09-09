using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Exceptions
{
    public class InvalidAgeException :PatientException
    {
        public InvalidAgeException(int age): base($"Invalid {age}. Age must be between 1 and 120.")
        { }
    }
}
