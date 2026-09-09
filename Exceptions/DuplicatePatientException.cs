using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Exceptions
{
    public class DuplicatePatientException:PatientException
    {
        public DuplicatePatientException(string id):base($"Id already exist: {id}") { }
    }
}
