using HospitalPatientRouting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalPatientRouting.Services
{
    public class BinarySnapshotService
    {
        public MemoryStream CreateSnapshot(IEnumerable<RoutedPatient> criticalPatients)
        {
            MemoryStream memoryStream = new();

            using BinaryWriter writer = new(memoryStream,Encoding.UTF8,leaveOpen: true);

            foreach (RoutedPatient patient in criticalPatients)
            {
                writer.Write(patient.PatientId);
                writer.Write(patient.Severity);
                writer.Write(patient.Department);
            }

            writer.Flush();

            memoryStream.Position = 0;

            return memoryStream;
        }

        public List<CriticalSnapshot> DecodeSnapshot(MemoryStream memoryStream)
        {
            List<CriticalSnapshot> snapshots = new();

            memoryStream.Position = 0;

            using BinaryReader reader = new(memoryStream,Encoding.UTF8,leaveOpen: true);

            while (memoryStream.Position < memoryStream.Length)
            {
                string patientId = reader.ReadString();
                int severity = reader.ReadInt32();
                string department = reader.ReadString();

                snapshots.Add(new CriticalSnapshot
                {
                    PatientId = patientId,
                    Severity = severity,
                    Department = department
                });
            }

            return snapshots;
        }

        public void Verify(IEnumerable<CriticalSnapshot> snapshots,IEnumerable<RoutedPatient> originalPatients)
        {
            List<RoutedPatient> original =originalPatients.ToList();

            List<CriticalSnapshot> decoded =snapshots.ToList();

            if (original.Count != decoded.Count)
            {
                throw new InvalidDataException("Binary snapshot verification failed: count mismatch.");
            }

            for (int i = 0; i < original.Count; i++)
            {
                RoutedPatient expected = original[i];
                CriticalSnapshot actual = decoded[i];

                if (expected.PatientId != actual.PatientId || expected.Severity != actual.Severity ||
                    expected.Department != actual.Department)
                {
                    throw new InvalidDataException($"Binary snapshot verification failed for patient :{expected.PatientId}.");
                }
            }
        }
    }

    public class CriticalSnapshot
    {
        public string PatientId { get; set; } = "";

        public int Severity { get; set; }

        public string Department { get; set; } = "";
    }
}
