using System;
using System.Collections.Generic;
using System.IO;

namespace Examen_Modulo_Practico_2
{
    public class DatasheetLink
    {
        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }

    public class ComponentInfo
    {
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Section { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string CommonUse { get; set; } = string.Empty;

        public string KeyData { get; set; } = string.Empty;

        public string Tip { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;

        public string TechnicalDetails { get; set; } = string.Empty;

        public string Pins { get; set; } = string.Empty;

        public string NominalValues { get; set; } = string.Empty;

        public string Identification { get; set; } = string.Empty;

        public string SafetyNotes { get; set; } = string.Empty;

        public string ElectricalCharacteristics { get; set; } = string.Empty;

        public string PackageFormats { get; set; } = string.Empty;

        public string TypicalCircuit { get; set; } = string.Empty;

        public string CommunicationProtocol { get; set; } = string.Empty;

        public string DesignRecommendations { get; set; } = string.Empty;

        public string FailureSymptoms { get; set; } = string.Empty;

        public string DatasheetOverview { get; set; } = string.Empty;

        public string OperatingNotes { get; set; } = string.Empty;

        public string SelectionGuide { get; set; } = string.Empty;

        public string LaboratoryChecklist { get; set; } = string.Empty;

        public string CommonUseDetails { get; set; } = string.Empty;

        public string NominalValuesDetails { get; set; } = string.Empty;

        public string PinsDetails { get; set; } = string.Empty;

        public string PackageFormatsDetails { get; set; } = string.Empty;

        public string CommunicationProtocolDetails { get; set; } = string.Empty;

        public string SafetyNotesDetails { get; set; } = string.Empty;

        public string DesignRecommendationsDetails { get; set; } = string.Empty;

        public string FailureSymptomsDetails { get; set; } = string.Empty;

        public string TechnicalDetailsExtended { get; set; } = string.Empty;

        public string ElectricalCharacteristicsExtended { get; set; } = string.Empty;

        public string TypicalCircuitExtended { get; set; } = string.Empty;

        public string IdentificationExtended { get; set; } = string.Empty;

        public List<DatasheetLink> ExtraDatasheets { get; set; } = new List<DatasheetLink>();

        public string SimulationImagePath { get; set; } = string.Empty;

        public string DatasheetImage => !string.IsNullOrWhiteSpace(SimulationImagePath) ? SimulationImagePath : ImagePath;

        public static string ResolveSimulationImagePath(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
                return string.Empty;

            var extensions = new[] { ".png", ".svg", ".jpg", ".jpeg" };
            foreach (var ext in extensions)
            {
                var path = basePath.EndsWith(ext, StringComparison.OrdinalIgnoreCase) ? basePath : basePath + ext;
                if (File.Exists(path))
                    return path;
            }

            // If none found, return the original basePath (caller may handle fallback)
            return basePath;
        }

    }
}
