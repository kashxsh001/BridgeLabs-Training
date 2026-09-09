using HospitalPatientRouting.Services;

namespace HospitalPatientRouting
{
    public class Class1
    {
        public static void Main()
        {
            string baseDirectory =AppContext.BaseDirectory;

            string csvPath =Path.Combine(baseDirectory,"Data","patients.csv");

            string departmentsPath =Path.Combine(baseDirectory,"Data","departments.json");

            string outputDirectory =Path.Combine(baseDirectory,"Output");

            string routedPatientsPath =Path.Combine(outputDirectory,"routed_patients.json");

            string criticalAlertsPath =Path.Combine(outputDirectory,"critical_alerts.txt");

            try
            {
                RoutingConfigurationLoader configLoader =new();

                var configuration =configLoader.Load(departmentsPath);

                CsvPatientReader csvReader =new();

                PatientValidator validator =new();

                PatientRouter router =new(configuration);

                BinarySnapshotService binaryService =new();

                HospitalPipeline pipeline =new(
                        csvReader,
                        validator,
                        router,
                        binaryService);

               
                pipeline.Process(
                    csvPath,
                    routedPatientsPath,
                    criticalAlertsPath);
            }
            catch (IOException ex)
            {
                Console.WriteLine(
                    $"File I/O error: {ex.Message}");
            }
            catch (System.Text.Json.JsonException ex)
            {
                Console.WriteLine(
                    $"JSON configuration error: {ex.Message}");
            }
        }
    }
}
