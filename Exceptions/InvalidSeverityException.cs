using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Exceptions
{
    internal class InvalidSeverityException : PatientException
    {
        public InvalidSeverityException(int severity) : base($"Invalid Severity {severity}. Severity must be between 1 to 10.")
        {
        }
    }
}
