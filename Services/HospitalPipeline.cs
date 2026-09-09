using HospitalPatientRouting.Exceptions;
using HospitalPatientRouting.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace HospitalPatientRouting.Services
{
    public class HospitalPipeline
    {
        private readonly CsvPatientReader _csvReader;
        private readonly PatientValidator _validator;
        private readonly PatientRouter _router;
        private readonly BinarySnapshotService _binaryService;

        public HospitalPipeline(
            CsvPatientReader csvReader,
            PatientValidator validator,
            PatientRouter router,
            BinarySnapshotService binaryService)
        {
            _csvReader = csvReader;
            _validator = validator;
            _router = router;
            _binaryService = binaryService;
        }

        public void Process(
            string csvPath,
            string routedPatientsPath,
            string criticalAlertsPath)
        {
            List<RoutedPatient> routedPatients = new();

            List<RoutedPatient> criticalPatients = new();

            foreach (Patient patient in _csvReader.ReadPatients(csvPath))
            {
                try
                {
                    _validator.Validate(patient);

                    RoutedPatient routedPatient =_router.Route(patient);

                    routedPatients.Add(routedPatient);

                    if (routedPatient.IsCritical)
                    {
                        criticalPatients.Add(routedPatient);
                    }

                    Console.WriteLine($"Processed: {patient.PatientId}");
                }
                catch (PatientException ex) 
                {
                    Console.WriteLine($"Rejected {patient.PatientId}: {ex.Message}");
                }
            }

            using MemoryStream snapshot =_binaryService.CreateSnapshot(criticalPatients);
            List<CriticalSnapshot> decodedSnapshots =_binaryService.DecodeSnapshot(snapshot);
            _binaryService.Verify(decodedSnapshots,criticalPatients);

            WriteRoutedPatients(routedPatientsPath,routedPatients); 
            WriteCriticalAlerts(criticalAlertsPath,decodedSnapshots);

            Console.WriteLine();
            Console.WriteLine("Processing completed.");
            Console.WriteLine( $"Valid patients: {routedPatients.Count}");
            Console.WriteLine($"Critical patients: {criticalPatients.Count}");
        }
        
        private static void WriteRoutedPatients(string filePath,List<RoutedPatient> patients)
        {
            string? directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using FileStream fileStream = new(
                filePath,
                FileMode.Create,
                FileAccess.Write);

            using StreamWriter writer = new(fileStream);

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(patients, options);
            writer.Write(json);
        }

        private static void WriteCriticalAlerts(string filePath,List<CriticalSnapshot> snapshots)
        {
            string? directory =Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using FileStream fileStream = new(filePath,FileMode.Create,FileAccess.Write);

            using BufferedStream bufferedStream =
                new(fileStream);

            using StreamWriter writer =
                new(bufferedStream);

            foreach (CriticalSnapshot snapshot in snapshots)
            {
                writer.WriteLine(
                    $"CRITICAL ALERT | " +
                    $"PatientId={snapshot.PatientId} | " +
                    $"Severity={snapshot.Severity} | " +
                    $"Department={snapshot.Department}");
            }
        }
    }
}


<<<<<<< HEAD

=======
//}
>>>>>>> bd65a12335b349dcd674d36fe6aa9e5a9188196b
