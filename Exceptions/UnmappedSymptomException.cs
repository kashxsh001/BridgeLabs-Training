using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Exceptions
{
    internal class UnmappedSymptomException : PatientException
    {
        public UnmappedSymptomException(string symptom) : base($"No depatment id exist for symptom :{symptom}")
        {
        }
    }
}
