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
            string? directory =Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using FileStream fileStream = new(filePath,FileMode.Create,FileAccess.Write);

            JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            using Utf8JsonWriter jsonWriter =new(fileStream);

            JsonSerializer.Serialize(jsonWriter,patients,options);

            jsonWriter.Flush();
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



//using HospitalPatientRouting.Services;

//namespace HospitalPatientRouting;

//public class Program
//{
//public static void Main()
//{
//    string baseDirectory =
//        AppContext.BaseDirectory;

//    string csvPath =
//        Path.Combine(
//            baseDirectory,
//            "Data",
//            "patients.csv");

//    string departmentsPath =
//        Path.Combine(
//            baseDirectory,
//            "Data",
//            "departments.json");

//    string outputDirectory =
//        Path.Combine(
//            baseDirectory,
//            "Output");

//    string routedPatientsPath =
//        Path.Combine(
//            outputDirectory,
//            "routed_patients.json");

//    string criticalAlertsPath =
//        Path.Combine(
//            outputDirectory,
//            "critical_alerts.txt");

//    try
//    {
//        // Load JSON configuration
//        RoutingConfigurationLoader configLoader =
//            new();

//        var configuration =
//            configLoader.Load(departmentsPath);

//        // Create services
//        CsvPatientReader csvReader =
//            new();

//        PatientValidator validator =
//            new();

//        PatientRouter router =
//            new(configuration);

//        BinarySnapshotService binaryService =
//            new();

//        HospitalPipeline pipeline =
//            new(
//                csvReader,
//                validator,
//                router,
//                binaryService);

//        // Start processing
//        pipeline.Process(
//            csvPath,
//            routedPatientsPath,
//            criticalAlertsPath);
//    }
//    catch (IOException ex)
//    {
//        Console.WriteLine(
//            $"File I/O error: {ex.Message}");
//    }
//    catch (System.Text.Json.JsonException ex)
//    {
//        Console.WriteLine(
//            $"JSON configuration error: {ex.Message}");
//    }
//}
//}


//using HospitalPatientRouting.Exceptions;
//using HospitalPatientRouting.Models;
//using HospitalPatientRouting.Services;
//using NUnit.Framework;

//namespace HospitalPatientRouting.Tests;

//[TestFixture]
//public class PatientRoutingTests
//{
//    private PatientRouter _router = null!;
//    private PatientValidator _validator = null!;
//    private BinarySnapshotService _binaryService = null!;

//    [SetUp]
//    public void Setup()
//    {
//        RoutingConfiguration configuration =
//            new()
//            {
//                SymptomRouting =
//                [
//                    new SymptomRouting
//                    {
//                        Symptom = "chest_pain",
//                        Department = "Cardiology"
//                    },

//                    new SymptomRouting
//                    {
//                        Symptom = "fever",
//                        Department = "General Medicine"
//                    },

//                    new SymptomRouting
//                    {
//                        Symptom = "headache",
//                        Department = "Neurology"
//                    },

//                    new SymptomRouting
//                    {
//                        Symptom = "fracture",
//                        Department = "Orthopedics"
//                    }
//                ]
//            };

//        _router = new PatientRouter(configuration);

//        _validator = new PatientValidator();

//        _binaryService = new BinarySnapshotService();
//    }

//    // ---------------------------------------------------------
//    // 1. Known symptom routing
//    // ---------------------------------------------------------

//    [Test]
//    public void Route_KnownSymptom_AssignsCorrectDepartment()
//    {
//        Patient patient = new()
//        {
//            PatientId = "P1",
//            Name = "Rohan",
//            Age = 34,
//            Symptom = "chest_pain",
//            Severity = 9
//        };

//        RoutedPatient result =
//            _router.Route(patient);

//        Assert.That(
//            result.Department,
//            Is.EqualTo("Cardiology"));
//    }

//    // ---------------------------------------------------------
//    // 2. Severity >= 8
//    // ---------------------------------------------------------

//    [Test]
//    public void Route_SeverityEightOrAbove_MarksPatientCritical()
//    {
//        Patient patient = new()
//        {
//            PatientId = "P5",
//            Name = "Manoj",
//            Age = 60,
//            Symptom = "fracture",
//            Severity = 8
//        };

//        RoutedPatient result =
//            _router.Route(patient);

//        Assert.That(
//            result.IsCritical,
//            Is.True);
//    }

//    // ---------------------------------------------------------
//    // 3. Invalid age
//    // ---------------------------------------------------------

//    [Test]
//    public void Route_InvalidAge_ThrowsInvalidAgeException()
//    {
//        Patient patient = new()
//        {
//            PatientId = "P3",
//            Name = "Arjun",
//            Age = 150,
//            Symptom = "headache",
//            Severity = 3
//        };

//        Assert.Throws<InvalidAgeException>(
//            () => _validator.Validate(patient));
//    }

//    // ---------------------------------------------------------
//    // 4. Invalid severity
//    // ---------------------------------------------------------

//    [Test]
//    public void Route_InvalidSeverity_ThrowsInvalidSeverityException()
//    {
//        Patient patient = new()
//        {
//            PatientId = "P1",
//            Name = "Rohan",
//            Age = 34,
//            Symptom = "chest_pain",
//            Severity = 11
//        };

//        Assert.Throws<InvalidSeverityException>(
//            () => _validator.Validate(patient));
//    }

//    // ---------------------------------------------------------
//    // 5. Unknown symptom
//    // ---------------------------------------------------------

//    [Test]
//    public void Route_UnknownSymptom_ThrowsUnmappedSymptomException()
//    {
//        Patient patient = new()
//        {
//            PatientId = "P4",
//            Name = "Kavya",
//            Age = 45,
//            Symptom = "unknown_symptom",
//            Severity = 7
//        };

//        Assert.Throws<UnmappedSymptomException>(
//            () => _router.Route(patient));
//    }

//    // ---------------------------------------------------------
//    // 6. Duplicate PatientId
//    // ---------------------------------------------------------

//    [Test]
//    public void Route_DuplicatePatientId_ThrowsDuplicatePatientException()
//    {
//        Patient firstPatient = new()
//        {
//            PatientId = "P1",
//            Name = "Rohan",
//            Age = 34,
//            Symptom = "chest_pain",
//            Severity = 9
//        };

//        Patient secondPatient = new()
//        {
//            PatientId = "P1",
//            Name = "Another Patient",
//            Age = 40,
//            Symptom = "fever",
//            Severity = 5
//        };

//        _validator.Validate(firstPatient);

//        Assert.Throws<DuplicatePatientException>(
//            () => _validator.Validate(secondPatient));
//    }

//    // ---------------------------------------------------------
//    // 7. Binary encode/decode
//    // ---------------------------------------------------------

//    [Test]
//    public void BinarySnapshot_EncodeThenDecode_ReturnsOriginalData()
//    {
//        List<RoutedPatient> patients =
//        [
//            new RoutedPatient
//            {
//                PatientId = "P1",
//                Name = "Rohan",
//                Age = 34,
//                Symptom = "chest_pain",
//                Severity = 9,
//                Department = "Cardiology",
//                IsCritical = true
//            },

//            new RoutedPatient
//            {
//                PatientId = "P5",
//                Name = "Manoj",
//                Age = 60,
//                Symptom = "fracture",
//                Severity = 8,
//                Department = "Orthopedics",
//                IsCritical = true
//            }
//        ];

//        using MemoryStream snapshot =
//            _binaryService.CreateSnapshot(patients);

//        List<CriticalSnapshot> decoded =
//            _binaryService.DecodeSnapshot(snapshot);

//        _binaryService.Verify(
//            decoded,
//            patients);

//        Assert.That(decoded.Count, Is.EqualTo(2));

//        Assert.That(
//            decoded[0].PatientId,
//            Is.EqualTo("P1"));

//        Assert.That(
//            decoded[0].Severity,
//            Is.EqualTo(9));

//        Assert.That(
//            decoded[0].Department,
//            Is.EqualTo("Cardiology"));

//        Assert.That(
//            decoded[1].PatientId,
//            Is.EqualTo("P5"));

//        Assert.That(
//            decoded[1].Severity,
//            Is.EqualTo(8));

//        Assert.That(
//            decoded[1].Department,
//            Is.EqualTo("Orthopedics"));
//    }

//    // ---------------------------------------------------------
//    // 8. Empty/header-only CSV
//    // ---------------------------------------------------------

//    [Test]
//    public void CsvReader_HeaderOnlyCsv_ReturnsNoPatients()
//    {
//        string tempFile =
//            Path.GetTempFileName();

//        try
//        {
//            File.WriteAllText(
//                tempFile,
//                "PatientId,Name,Age,Symptom,Severity\n");

//            CsvPatientReader reader =
//                new();

//            List<Patient> patients =
//                reader.ReadPatients(tempFile).ToList();

//            Assert.That(
//                patients,
//                Is.Empty);
//        }
//        finally
//        {
//            File.Delete(tempFile);
//        }
//    }

//    // ---------------------------------------------------------
//    // 9. Boundary validation
//    // ---------------------------------------------------------

//    [Test]
//    public void Validate_BoundaryAgeAndSeverity_AreAccepted()
//    {
//        Patient patient = new()
//        {
//            PatientId = "P100",
//            Name = "Boundary",
//            Age = 120,
//            Symptom = "fever",
//            Severity = 10
//        };

//        Assert.DoesNotThrow(
//            () => _validator.Validate(patient));
//    }
//}