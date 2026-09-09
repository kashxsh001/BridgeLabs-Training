using HospitalPatientRouting.Exceptions;
using HospitalPatientRouting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Services
{
    public class PatientRouter
    {
        private readonly Dictionary<string, string> _routingMap;

        public PatientRouter(RoutingConfiguration configuration)
        {
            _routingMap = configuration.SymptomRouting
                .Where(x =>
                    !string.IsNullOrWhiteSpace(x.Symptom) &&
                    !string.IsNullOrWhiteSpace(x.Department))
                .ToDictionary(
                    x => x.Symptom,
                    x => x.Department,
                    StringComparer.OrdinalIgnoreCase);
        }

        public RoutedPatient Route(Patient patient)
        {
            if (!_routingMap.TryGetValue(
                    patient.Symptom,
                    out string? department))
            {
                throw new UnmappedSymptomException(
                    patient.Symptom);
            }

            return new RoutedPatient
            {
                PatientId = patient.PatientId,
                Name = patient.Name,
                Age = patient.Age,
                Symptom = patient.Symptom,
                Severity = patient.Severity,
                Department = department,
                IsCritical = patient.Severity >= 8
            };
        }
    }
}
