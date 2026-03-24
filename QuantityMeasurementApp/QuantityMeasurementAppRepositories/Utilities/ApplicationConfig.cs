using System;
using System.Collections.Generic;
using System.IO;

namespace QuantityMeasurementAppRepositories.Utilities
{
    /*
     * ============================================================================================
     * CLASS: ApplicationConfig
     * 
     * This class is responsible for reading configuration values from an external JSON file
     * (appsettings.json) and providing easy access to those values throughout the application.
     * 
     * Purpose:
     * --------------------------------------------------------------------------------------------
     * - Centralized configuration management
     * - Avoid hardcoding values like connection strings, pool sizes, etc.
     * - Improve flexibility and maintainability of the application
     * 
     * Key Features:
     * --------------------------------------------------------------------------------------------
     * - Reads configuration file manually (line-by-line parsing)
     * - Supports section-based keys (e.g., "Database:ConnectionString")
     * - Provides default values if keys are missing
     * - Designed to work without external libraries (lightweight implementation)
     * 
     * NOTE:
     * --------------------------------------------------------------------------------------------
     * This is a simplified custom parser and not a full JSON parser like System.Text.Json.
     * It assumes a predictable structure of the appsettings.json file.
     * ============================================================================================
     */
    public class ApplicationConfig
    {
        /*
         * ========================================================================================
         * INTERNAL DATA STORAGE
         * 
         * A dictionary is used to store configuration values as key-value pairs.
         * Example:
         * - Key   : "Database:ConnectionString"
         * - Value : "Server=localhost..."
         * ========================================================================================
         */
        private Dictionary<string, string> settings;

        /*
         * ========================================================================================
         * DEFAULT CONSTRUCTOR
         * 
         * - Initializes the dictionary
         * - Automatically looks for "appsettings.json" in the application's base directory
         * ========================================================================================
         */
        public ApplicationConfig()
        {
            settings = new Dictionary<string, string>();

            // Construct path to config file located next to the executable
            string defaultPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "appsettings.json");

            LoadConfig(defaultPath);
        }

        /*
         * ========================================================================================
         * PARAMETERIZED CONSTRUCTOR
         * 
         * - Allows loading configuration from a custom file path
         * - Mainly used in unit testing or special environments
         * ========================================================================================
         */
        public ApplicationConfig(string configFilePath)
        {
            settings = new Dictionary<string, string>();
            LoadConfig(configFilePath);
        }

        /*
         * ========================================================================================
         * METHOD: LoadConfig (PRIVATE)
         * 
         * Reads the configuration file line by line and extracts key-value pairs.
         * 
         * Behavior:
         * ----------------------------------------------------------------------------------------
         * - Skips unnecessary lines (empty lines, braces)
         * - Detects sections (e.g., "Database": { ... })
         * - Extracts keys and values manually using string operations
         * - Supports both quoted and non-quoted values
         * 
         * Limitations:
         * ----------------------------------------------------------------------------------------
         * - Not a full JSON parser
         * - Assumes a simple and consistent JSON format
         * ========================================================================================
         */
        private void LoadConfig(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("[ApplicationConfig] Warning: config file not found: " + filePath + ". Using defaults.");
                return;
            }

            // Read all lines from the configuration file
            string[] lines = File.ReadAllLines(filePath);
            string currentSection = "";

            foreach (string rawLine in lines)
            {
                string line = rawLine.Trim();

                // Ignore empty lines and structural JSON symbols
                if (line == "" || line == "{" || line == "}" || line == "},")
                {
                    continue;
                }

                /*
                 * Detect section headers like:
                 * "Database": {
                 * and extract the section name
                 */
                if (line.EndsWith("{") || line.EndsWith(": {"))
                {
                    int fq = line.IndexOf('"');
                    int sq = line.IndexOf('"', fq + 1);
                    if (fq >= 0 && sq > fq)
                    {
                        currentSection = line.Substring(fq + 1, sq - fq - 1);
                    }
                    continue;
                }

                /*
                 * Detect key-value pairs and extract both key and value
                 */
                if (line.Contains(":") && line.Contains("\""))
                {
                    // Extract key
                    int firstQuote = line.IndexOf('"');
                    int secondQuote = line.IndexOf('"', firstQuote + 1);

                    if (firstQuote < 0 || secondQuote < 0)
                    {
                        continue;
                    }

                    string keyPart = line.Substring(
                        firstQuote + 1, secondQuote - firstQuote - 1);

                    // Extract value
                    int lastQuote = line.LastIndexOf('"');
                    int secondLastQuote = line.LastIndexOf('"', lastQuote - 1);

                    string valuePart = "";

                    if (lastQuote > secondQuote && secondLastQuote > secondQuote)
                    {
                        // Case: value is inside quotes
                        valuePart = line.Substring(secondLastQuote + 1, lastQuote - secondLastQuote - 1);
                    }
                    else
                    {
                        // Case: value is not quoted (numbers, etc.)
                        int colonIdx = line.IndexOf(':', secondQuote);
                        if (colonIdx >= 0)
                        {
                            valuePart = line.Substring(colonIdx + 1).Trim();
                            valuePart = valuePart.TrimEnd(',').Trim();
                        }
                    }

                    // Build full key including section
                    string fullKey = (currentSection != "") ? currentSection + ":" + keyPart : keyPart;

                    if (!settings.ContainsKey(fullKey))
                    {
                        settings.Add(fullKey, valuePart);
                    }
                }
            }

            // Logging loaded configuration
            Console.WriteLine("[ApplicationConfig] Loaded For Loading " + settings.Count + " settings from " + filePath);

            Console.WriteLine("[ApplicationConfig] Settings loaded:");
            foreach (string key in settings.Keys)
            {
                Console.WriteLine("  " + key + " = " + settings[key]);
            }
        }

        /*
         * ========================================================================================
         * METHOD: GetSetting
         * 
         * Returns the value for a given key, or a default value if the key does not exist.
         * ========================================================================================
         */
        public string GetSetting(string key, string defaultValue)
        {
            if (settings.ContainsKey(key))
            {
                return settings[key];
            }
            Console.WriteLine("[ApplicationConfig] Key not found: " + key + ". Using default: " + defaultValue);
            return defaultValue;
        }

        /*
         * ========================================================================================
         * METHOD: GetConnectionString
         * 
         * Retrieves the database connection string from configuration.
         * 
         * Special Handling:
         * - Replaces double backslashes (\\) with single (\)
         * - This is required because JSON escapes backslashes
         * ========================================================================================
         */
        public string GetConnectionString()
        {
            string cs = GetSetting("Database:ConnectionString",
                "Server=localhost\\SQLEXPRESS;Database=QuantityMeasurementAppDB;" +
                "Trusted_Connection=True;TrustServerCertificate=True;");

            // Fix escaped backslashes
            cs = cs.Replace("\\\\", "\\");

            return cs;
        }

        /*
         * ========================================================================================
         * METHOD: GetMaxPoolSize
         * 
         * Retrieves maximum connection pool size from configuration.
         * Converts string value to integer safely.
         * ========================================================================================
         */
        public int GetMaxPoolSize()
        {
            string val = GetSetting("Database:PoolSize", "5");
            int result = 5;
            int.TryParse(val, out result);
            return result;
        }

        /*
         * ========================================================================================
         * METHOD: GetConnectionTimeout
         * 
         * Retrieves connection timeout (in seconds) from configuration.
         * ========================================================================================
         */
        public int GetConnectionTimeout()
        {
            string val = GetSetting("Database:ConnectionTimeout", "30");
            int result = 30;
            int.TryParse(val, out result);
            return result;
        }

        /*
         * ========================================================================================
         * METHOD: GetRepositoryType
         * 
         * Returns which repository implementation should be used:
         * - "cache"    → In-memory repository
         * - "database" → Database-backed repository
         * ========================================================================================
         */
        public string GetRepositoryType()
        {
            return GetSetting("Repository:Type", "cache");
        }

        /*
         * ========================================================================================
         * METHOD: GetDatabaseProvider
         * 
         * Returns the database provider type (e.g., "sqlserver")
         * ========================================================================================
         */
        public string GetDatabaseProvider()
        {
            return GetSetting("Database:Provider", "sqlserver");
        }
    }
}