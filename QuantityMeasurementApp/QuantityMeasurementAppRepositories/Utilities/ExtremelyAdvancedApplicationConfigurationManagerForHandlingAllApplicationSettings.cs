using System;
using System.Collections.Generic;
using System.IO;

namespace QuantityMeasurementAppRepositories.Utilities
{
    /*
     * Highly detailed configuration manager class responsible for handling
     * application settings loaded from external JSON configuration file.
     */
    public class ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings
    {
        // Internal storage for configuration key-value pairs
        private Dictionary<string, string> extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs;

        // ========================== DEFAULT CONSTRUCTOR ==========================
        public ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings()
        {
            extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs =
                new Dictionary<string, string>();

            string automaticallyResolvedDefaultConfigurationFilePath =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");

            LoadAndParseConfigurationFileFromGivenFilePath(automaticallyResolvedDefaultConfigurationFilePath);
        }

        // ========================== PARAMETERIZED CONSTRUCTOR ==========================
        public ExtremelyAdvancedApplicationConfigurationManagerForHandlingAllApplicationSettings(
            string customConfigurationFilePathProvidedByUserOrTestEnvironment)
        {
            extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs =
                new Dictionary<string, string>();

            LoadAndParseConfigurationFileFromGivenFilePath(customConfigurationFilePathProvidedByUserOrTestEnvironment);
        }

        // ========================== LOAD CONFIG ==========================
        private void LoadAndParseConfigurationFileFromGivenFilePath(
            string configurationFilePathToBeLoadedAndParsed)
        {
            if (!File.Exists(configurationFilePathToBeLoadedAndParsed))
            {
                Console.WriteLine("⚠️ Configuration file not found at path: "
                    + configurationFilePathToBeLoadedAndParsed
                    + ". Default configuration values will be used.");
                return;
            }

            string[] allLinesReadFromConfigurationFile =
                File.ReadAllLines(configurationFilePathToBeLoadedAndParsed);

            string currentActiveConfigurationSectionName = "";

            foreach (string rawLineFromConfigurationFile in allLinesReadFromConfigurationFile)
            {
                string trimmedLineAfterRemovingWhitespace =
                    rawLineFromConfigurationFile.Trim();

                if (trimmedLineAfterRemovingWhitespace == "" ||
                    trimmedLineAfterRemovingWhitespace == "{" ||
                    trimmedLineAfterRemovingWhitespace == "}" ||
                    trimmedLineAfterRemovingWhitespace == "},")
                {
                    continue;
                }

                // Detect section
                if (trimmedLineAfterRemovingWhitespace.EndsWith("{") ||
                    trimmedLineAfterRemovingWhitespace.EndsWith(": {"))
                {
                    int firstQuoteIndex = trimmedLineAfterRemovingWhitespace.IndexOf('"');
                    int secondQuoteIndex = trimmedLineAfterRemovingWhitespace.IndexOf('"', firstQuoteIndex + 1);

                    if (firstQuoteIndex >= 0 && secondQuoteIndex > firstQuoteIndex)
                    {
                        currentActiveConfigurationSectionName =
                            trimmedLineAfterRemovingWhitespace.Substring(
                                firstQuoteIndex + 1,
                                secondQuoteIndex - firstQuoteIndex - 1);
                    }
                    continue;
                }

                // Detect key-value
                if (trimmedLineAfterRemovingWhitespace.Contains(":") &&
                    trimmedLineAfterRemovingWhitespace.Contains("\""))
                {
                    int firstQuote = trimmedLineAfterRemovingWhitespace.IndexOf('"');
                    int secondQuote = trimmedLineAfterRemovingWhitespace.IndexOf('"', firstQuote + 1);

                    if (firstQuote < 0 || secondQuote < 0)
                        continue;

                    string extractedKeyName =
                        trimmedLineAfterRemovingWhitespace.Substring(firstQuote + 1, secondQuote - firstQuote - 1);

                    int lastQuote = trimmedLineAfterRemovingWhitespace.LastIndexOf('"');
                    int secondLastQuote = trimmedLineAfterRemovingWhitespace.LastIndexOf('"', lastQuote - 1);

                    string extractedValue = "";

                    if (lastQuote > secondQuote && secondLastQuote > secondQuote)
                    {
                        extractedValue = trimmedLineAfterRemovingWhitespace.Substring(
                            secondLastQuote + 1,
                            lastQuote - secondLastQuote - 1);
                    }
                    else
                    {
                        int colonIndex = trimmedLineAfterRemovingWhitespace.IndexOf(':', secondQuote);
                        if (colonIndex >= 0)
                        {
                            extractedValue =
                                trimmedLineAfterRemovingWhitespace.Substring(colonIndex + 1)
                                .Trim()
                                .TrimEnd(',')
                                .Trim();
                        }
                    }

                    string fullyQualifiedConfigurationKey =
                        (currentActiveConfigurationSectionName != "")
                        ? currentActiveConfigurationSectionName + ":" + extractedKeyName
                        : extractedKeyName;

                    if (!extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs.ContainsKey(fullyQualifiedConfigurationKey))
                    {
                        extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs
                            .Add(fullyQualifiedConfigurationKey, extractedValue);
                    }
                }
            }

            Console.WriteLine("✅ Successfully loaded "
                + extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs.Count
                + " configuration settings from file: "
                + configurationFilePathToBeLoadedAndParsed);

            Console.WriteLine("📋 Listing all loaded configuration key-value pairs:");

            foreach (string configurationKey in extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs.Keys)
            {
                Console.WriteLine("➡️ " + configurationKey + " = "
                    + extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs[configurationKey]);
            }
        }

        // ========================== GET SETTING ==========================
        public string RetrieveConfigurationValueBasedOnKeyOrReturnDefaultValueIfNotFound(
            string configurationKeyName,
            string defaultValueIfKeyIsMissing)
        {
            if (extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs.ContainsKey(configurationKeyName))
            {
                return extremelyImportantConfigurationSettingsDictionaryUsedForStoringAllParsedKeyValuePairs[configurationKeyName];
            }

            Console.WriteLine("⚠️ Configuration key not found: "
                + configurationKeyName
                + ". Using default value: "
                + defaultValueIfKeyIsMissing);

            return defaultValueIfKeyIsMissing;
        }

        // ========================== CONNECTION STRING ==========================
        public string RetrieveDatabaseConnectionStringFromConfiguration()
        {
            string rawConnectionStringValue =
                RetrieveConfigurationValueBasedOnKeyOrReturnDefaultValueIfNotFound(
                    "Database:ConnectionString",
                    "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementAppDB;Trusted_Connection=True;TrustServerCertificate=True;");

            rawConnectionStringValue = rawConnectionStringValue.Replace("\\\\", "\\");

            return rawConnectionStringValue;
        }

        // ========================== POOL SIZE ==========================
        public int RetrieveMaximumDatabaseConnectionPoolSizeFromConfiguration()
        {
            string rawPoolSizeValue =
                RetrieveConfigurationValueBasedOnKeyOrReturnDefaultValueIfNotFound("Database:PoolSize", "5");

            int parsedPoolSizeResult = 5;
            int.TryParse(rawPoolSizeValue, out parsedPoolSizeResult);

            return parsedPoolSizeResult;
        }

        // ========================== TIMEOUT ==========================
        public int RetrieveDatabaseConnectionTimeoutValueFromConfiguration()
        {
            string rawTimeoutValue =
                RetrieveConfigurationValueBasedOnKeyOrReturnDefaultValueIfNotFound("Database:ConnectionTimeout", "30");

            int parsedTimeoutResult = 30;
            int.TryParse(rawTimeoutValue, out parsedTimeoutResult);

            return parsedTimeoutResult;
        }

        // ========================== REPOSITORY TYPE ==========================
        public string RetrieveRepositoryImplementationTypeFromConfiguration()
        {
            return RetrieveConfigurationValueBasedOnKeyOrReturnDefaultValueIfNotFound("Repository:Type", "cache");
        }

        // ========================== DATABASE PROVIDER ==========================
        public string RetrieveDatabaseProviderTypeFromConfiguration()
        {
            return RetrieveConfigurationValueBasedOnKeyOrReturnDefaultValueIfNotFound("Database:Provider", "sqlserver");
        }
        public int RetrieveDatabaseConnectionTimeoutDurationFromConfiguration()
        {
            return RetrieveDatabaseConnectionTimeoutValueFromConfiguration();
        }
    }
}