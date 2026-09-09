using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Exceptions
{
    public class PatientException : Exception
    {
      
            public PatientException(string message)
                : base(message)
            {
            }

            public PatientException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        

    }
}
