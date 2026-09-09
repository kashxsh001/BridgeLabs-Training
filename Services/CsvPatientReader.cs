using CsvHelper;
using CsvHelper.Configuration;
using HospitalPatientRouting.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HospitalPatientRouting.Services
{
    public class CsvPatientReader
    {
        public IEnumerable<Patient> ReadPatients(string filePath)
        {
            using FileStream fileStream = new(filePath,FileMode.Open,FileAccess.Read,FileShare.Read);

            using StreamReader streamReader = new(fileStream);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = context =>
                {
                    Console.WriteLine($"Bad CSV data: {context.RawRecord}");
                }
            };

            using CsvReader csvReader = new(streamReader,config);

            while (csvReader.Read())
            {
                Patient? patient;

                try
                {
                    patient = csvReader.GetRecord<Patient>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Rejected CSV record: {ex.Message}");

                    continue;
                }

                if (patient != null)
                {
                    yield return patient;
                }
            }
        }
    }
}

