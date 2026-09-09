using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Exceptions
{
    public class UnmappedSymptomException : PatientException
    {
        public UnmappedSymptomException(string symptom) : base($"No depatment id exist for symptom :{symptom}")
        {
        }
    }
}
