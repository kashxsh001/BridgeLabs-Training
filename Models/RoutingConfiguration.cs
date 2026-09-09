using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Models
{
    public class RoutingConfiguration
    {
        public List<SymptomRouting> SymptomRouting { get; set; } = new();
    }

    public class SymptomRouting
    {
        public string Symptom { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;
    }
}
