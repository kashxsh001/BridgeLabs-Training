using HospitalPatientRouting.Exceptions;
using HospitalPatientRouting.Models;
using HospitalPatientRouting.Services;
using NUnit.Framework;

namespace HospitalPatientRouting.Tests;
public class HospitalPatientRoutingTests
{
    private PatientRouter _router = null!;
    private PatientValidator _validator = null!;
    private BinarySnapshotService _binaryService = null!;

    [SetUp]
    public void Setup()
    {
        RoutingConfiguration configuration =
            new()
            {
                SymptomRouting =
                [
                    new SymptomRouting
                    {
                        Symptom = "chest_pain",
                        Department = "Cardiology"
                    },

                    new SymptomRouting
                    {
                        Symptom = "fever",
                        Department = "General Medicine"
                    },

                    new SymptomRouting
                    {
                        Symptom = "headache",
                        Department = "Neurology"
                    },

                    new SymptomRouting
                    {
                        Symptom = "fracture",
                        Department = "Orthopedics"
                    }
                ]
            };

        _router = new PatientRouter(configuration);

        _validator = new PatientValidator();

        _binaryService = new BinarySnapshotService();
    }

    [Test]
    public void Route_KnownSymptom_AssignsCorrectDepartment()
    {
        Patient patient = new()
        {
            PatientId = "P1",
            Name = "Rohan",
            Age = 34,
            Symptom = "chest_pain",
            Severity = 9
        };

        RoutedPatient result =
            _router.Route(patient);

        Assert.That(
            result.Department,
            Is.EqualTo("Cardiology"));
    }


    [Test]
    public void Route_SeverityEightOrAbove_MarksPatientCritical()
    {
        Patient patient = new()
        {
            PatientId = "P5",
            Name = "Manoj",
            Age = 60,
            Symptom = "fracture",
            Severity = 8
        };

        RoutedPatient result =
            _router.Route(patient);

        Assert.That(
            result.IsCritical,
            Is.True);
    }



    [Test]
    public void Route_InvalidAge_ThrowsInvalidAgeException()
    {
        Patient patient = new()
        {
            PatientId = "P3",
            Name = "Arjun",
            Age = 150,
            Symptom = "headache",
            Severity = 3
        };

        Assert.Throws<InvalidAgeException>(
            () => _validator.Validate(patient));
    }

    [Test]
    public void Route_InvalidSeverity_ThrowsInvalidSeverityException()
    {
        Patient patient = new()
        {
            PatientId = "P1",
            Name = "Rohan",
            Age = 34,
            Symptom = "chest_pain",
            Severity = 11
        };

        Assert.Throws<InvalidSeverityException>(
            () => _validator.Validate(patient));
    }


    [Test]
    public void Route_UnknownSymptom_ThrowsUnmappedSymptomException()
    {
        Patient patient = new()
        {
            PatientId = "P4",
            Name = "Kavya",
            Age = 45,
            Symptom = "unknown_symptom",
            Severity = 7
        };

        Assert.Throws<UnmappedSymptomException>(
            () => _router.Route(patient));
    }


    [Test]
    public void Route_DuplicatePatientId_ThrowsDuplicatePatientException()
    {
        Patient firstPatient = new()
        {
            PatientId = "P1",
            Name = "Rohan",
            Age = 34,
            Symptom = "chest_pain",
            Severity = 9
        };

        Patient secondPatient = new()
        {
            PatientId = "P1",
            Name = "Another Patient",
            Age = 40,
            Symptom = "fever",
            Severity = 5
        };

        _validator.Validate(firstPatient);

        Assert.Throws<DuplicatePatientException>(
            () => _validator.Validate(secondPatient));
    }


    [Test]
    public void BinarySnapshot_EncodeThenDecode_ReturnsOriginalData()
    {
        List<RoutedPatient> patients =
        [
            new RoutedPatient
            {
                PatientId = "P1",
                Name = "Rohan",
                Age = 34,
                Symptom = "chest_pain",
                Severity = 9,
                Department = "Cardiology",
                IsCritical = true
            },

            new RoutedPatient
            {
                PatientId = "P5",
                Name = "Manoj",
                Age = 60,
                Symptom = "fracture",
                Severity = 8,
                Department = "Orthopedics",
                IsCritical = true
            }
        ];

        using MemoryStream snapshot =
            _binaryService.CreateSnapshot(patients);

        List<CriticalSnapshot> decoded =
            _binaryService.DecodeSnapshot(snapshot);

        _binaryService.Verify(
            decoded,
            patients);

        Assert.That(decoded.Count, Is.EqualTo(2));

        Assert.That(
            decoded[0].PatientId,
            Is.EqualTo("P1"));

        Assert.That(
            decoded[0].Severity,
            Is.EqualTo(9));

        Assert.That(
            decoded[0].Department,
            Is.EqualTo("Cardiology"));

        Assert.That(
            decoded[1].PatientId,
            Is.EqualTo("P5"));

        Assert.That(
            decoded[1].Severity,
            Is.EqualTo(8));

        Assert.That(
            decoded[1].Department,
            Is.EqualTo("Orthopedics"));
    }


    [Test]
    public void CsvReader_HeaderOnlyCsv_ReturnsNoPatients()
    {
        string tempFile =
            Path.GetTempFileName();

        try
        {
            File.WriteAllText(
                tempFile,
                "PatientId,Name,Age,Symptom,Severity\n");

            CsvPatientReader reader =
                new();

            List<Patient> patients =
                reader.ReadPatients(tempFile).ToList();

            Assert.That(
                patients,
                Is.Empty);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }


    [Test]
    public void Validate_BoundaryAgeAndSeverity_AreAccepted()
    {
        Patient patient = new()
        {
            PatientId = "P100",
            Name = "Boundary",
            Age = 120,
            Symptom = "fever",
            Severity = 10
        };

        Assert.DoesNotThrow(
            () => _validator.Validate(patient));
    }
}