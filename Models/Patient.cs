using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Models
{
    public class Patient
    {
        public string PatientId { get; set; } = "";
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Symptom { get; set; } = "";
        public int Severity { get; set; }
    }
}
