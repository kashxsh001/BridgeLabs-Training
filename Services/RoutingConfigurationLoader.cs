using System;
using System.Collections.Generic;
using System.Text;
using HospitalPatientRouting.Models;
using System.Text.Json;

namespace HospitalPatientRouting.Services
{
  public class RoutingConfigurationLoader
{
    public RoutingConfiguration Load(string filePath)
   {
        using FileStream fileStream = new(
            filePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read);

            RoutingConfiguration? configuration =
                JsonSerializer.Deserialize<RoutingConfiguration>(
                    fileStream,
                    new JsonSerializerOptions
                    {
                       PropertyNameCaseInsensitive = true
                    });

            if (configuration == null)
            {
                throw new InvalidDataException(
                    "Routing configuration is empty or invalid.");
            }

            return configuration;
        }
    }

}
