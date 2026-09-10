# Hospital Patient Routing & Critical Alert Pipeline

A C#/.NET console application that processes patient records from CSV, validates patient data, routes patients to the appropriate hospital department, and generates alerts for critical patients.

## Features

- Read CSV using `FileStream`, `StreamReader`, and CsvHelper
- Load department routing configuration from JSON
- Validate patient age, severity, Patient ID, and symptoms
- Detect duplicate Patient IDs
- Route patients based on symptoms
- Identify critical patients (`Severity >= 8`)
- Use `MemoryStream`, `BinaryWriter`, and `BinaryReader` for critical patient snapshots
- Verify binary data before generating alerts
- Write critical alerts using `BufferedStream`
- Handle invalid records using custom exceptions
- NUnit unit testing

## Project Flow

```text
patients.csv
     ↓
CsvPatientReader
     ↓
PatientValidator
     ↓
PatientRouter
     ↓
Routed Patient
     ↓
Critical Patient?
   ↓ Yes
Binary Snapshot
     ↓
Decode & Verify
     ↓
Critical Alert

Valid Patients → routed_patients.json
Invalid Records → Rejected without stopping the pipeline
