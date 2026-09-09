using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Exceptions
{
    internal class DuplicatePatientException:PatientException
    {
        public DuplicatePatientException(string id):base($"Id already exist: {id}") { }
    }
}
