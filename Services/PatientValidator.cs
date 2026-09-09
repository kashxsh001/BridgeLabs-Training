using System;
using System.Collections.Generic;
using System.Text;
using HospitalPatientRouting.Models;
using HospitalPatientRouting.Exceptions;

namespace HospitalPatientRouting.Services
{

    public class PatientValidator
    {
        private readonly HashSet<string> _patientIds =new(StringComparer.OrdinalIgnoreCase);

        public void Validate(Patient patient)
        {
            ValidatePatientId(patient.PatientId);

            ValidateAge(patient.Age);

            ValidateSeverity(patient.Severity);

            ValidateDuplicatePatientId(patient.PatientId);

            _patientIds.Add(patient.PatientId);
        }

        private static void ValidatePatientId(string patientId)
        {
            if (string.IsNullOrWhiteSpace(patientId))
            {
                throw new PatientException("PatientId cannot be empty.");
            }
        }

        private static void ValidateAge(int age)
        {
            if (age < 1 || age > 120)
            {
                throw new InvalidAgeException(age);
            }
        }

        private static void ValidateSeverity(int severity)
        {
            if (severity < 1 || severity > 10)
            {
                throw new InvalidSeverityException(severity);
            }
        }

        private void ValidateDuplicatePatientId(string patientId)
        {
            if (_patientIds.Contains(patientId))
            {
                throw new DuplicatePatientException(patientId);
            }
        }
    }
}
