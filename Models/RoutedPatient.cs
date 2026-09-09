using CsvHelper;
using CsvHelper.Configuration;
using HospitalPatientRouting.Exceptions;
using HospitalPatientRouting.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace HospitalPatientRouting.Models
{
    public class RoutedPatient
    {
        public string PatientId { get; set; } = "";
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Symptom { get; set; } = "";
        public int Severity { get; set; }
        public bool IsCritical {  get; set; }
        public string Department { get; set; } = "";

    }
}
